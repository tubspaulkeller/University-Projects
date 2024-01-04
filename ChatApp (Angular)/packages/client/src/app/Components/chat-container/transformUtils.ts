import { IReceiveMessage } from '../../Interfaces/IMessage';
import {
  ChatMessageData,
  ChatMessageType,
  IUser,
  MessageContent,
  RoomData,
  UserData,
} from 'shared';
import { environment } from '../../../environments/environment';

/**
 * Transforms a message from the server with current room info to a message that can be displayed in the chat
 * @param message {IReceiveMessage} The message from the server
 * @param currentUser {IUser} The current user
 * @param currentRoom {ChatRoom} - The current room, used collect additional user data
 * @returns {ChatMessageData} - The transformed messages
 */
export function transformToChatMessageData(
  message: IReceiveMessage,
  currentUser: IUser,
  currentRoom: RoomData
): ChatMessageData {
  const senderData = getSenderUserData(message, currentRoom, currentUser)!;
  const messageType = getMessageType(message);
  return {
    ianaTimezone: message.ianaTimezone,
    type: messageType,
    isCurrentUser: Boolean(message.sender === currentUser._id),
    sender: senderData,
    timestamp: message.timestamp,
    content: getMessageContent(message, messageType),
  };
}

const getMessageType = (message: IReceiveMessage): ChatMessageType => {
  let messageType: ChatMessageType = 'text';
  if (message.recordingurl) messageType = 'audio';
  if (message.imgId && message.imgVersion) messageType = 'image';
  if (message.gif) messageType = 'gif';
  return messageType;
};

const getMessageContent = (message: IReceiveMessage, type: ChatMessageType): MessageContent => {
  switch (type) {
    case 'text':
      return message.message as string;
    case 'image':
      return { imgId: message.imgId as string, imgVersion: message.imgVersion as string };
    case 'audio':
      return { recordingUrl: message.recordingurl as string };
    case 'gif':
      return { gifUrl: message?.gif as string };
    default:
      return null;
  }
};

const getSenderUserData = (
  message: IReceiveMessage,
  currentRoom: RoomData,
  currentUser: IUser
): UserData => {
  const errorCase: UserData = { id: 'USER_NOT_FOUND', name: 'USER_NOT_FOUND', imgSrc: '' };
  let senderData: UserData = errorCase;
  if (currentRoom.roomType === 'private')
    senderData = getUserDataFromPrivateRoom(message, currentRoom, currentUser) || errorCase;
  if (currentRoom.roomType !== 'private')
    senderData = getUserFromPublicRoom(message, currentRoom) || errorCase;
  return senderData;
};

const getUserDataFromPrivateRoom = (
  message: IReceiveMessage,
  currentRoom: RoomData,
  currentUser: IUser
): UserData | undefined => {
  const { chatPartner } = currentRoom;
  if (!chatPartner) return undefined;
  if (message.sender === chatPartner.id) return chatPartner;
  if (message.sender === currentUser._id)
    return {
      id: currentUser._id,
      name: currentUser.username,
      imgSrc: `https://res.cloudinary.com/${environment.cloudinary_name}/image/upload/v${currentUser.images}/${currentUser.pictureId}`,
    };
  return undefined;
};

const getUserFromPublicRoom = (
  message: IReceiveMessage,
  currentRoom: RoomData
): UserData | undefined => {
  // sender is always part of the groupMembers
  const { members } = currentRoom;
  if (!members) return undefined;
  return members.find((member) => member.id === message.sender);
};
