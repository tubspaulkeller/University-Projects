import { SocketEvent } from '../websockets/types';
import { config } from '../../config';
import { getRoomById } from './dbUtils';
import User from '../models/User';
import { Room } from '../models/Room';
import { EventMessage, InitRoomDataRequest, IUser, RoomData, RoomType, UserData } from 'shared';

function getRoomType(room: Room): RoomType | undefined {
  const isPrivate = room.privateRoomparticipants.length > 0;
  const isPublic = room.groupRoomparticipants.length > 0;
  const isMain = room.name === 'main';
  if (isMain) return 'main';
  if (isPrivate) return 'private';
  if (isPublic) return 'public';
  return undefined;
}

function assembleUserData(user: IUser): UserData {
  const { _id, username, pictureId, pictureVersion } = user;
  return {
    id: _id,
    name: username,
    imgSrc: `https://res.cloudinary.com/${config.cloudinary_name}/image/upload/v${pictureVersion}/${pictureId}`,
  };
}

async function mapGroupMembersToDTOs(members: IUser[]): Promise<UserData[]> {
  const tmp = [];
  for (const member of members) {
    const mongoUser = await User.findById(member._id);
    const UserData = mongoUser ? assembleUserData(mongoUser) : undefined;
    if (UserData) tmp.push(UserData);
  }
  return tmp;
}

async function assembleChatPartnerData(
  pMembers: any[],
  msgSenderId: string | undefined
): Promise<UserData | undefined> {
  const obj = pMembers[0]; // we always have an array of length 1, idk why
  if (!obj || !msgSenderId) return undefined;
  // we always have sender / receiver in private room, but sender is not always sender of the msg...
  const { sender, receiver } = obj;
  // chatPartner is the other user in the private room
  const chatPartnerId = msgSenderId === String(sender._id) ? receiver._id : sender._id;
  const chatPartner = await User.findById(chatPartnerId);
  return chatPartner ? assembleUserData(chatPartner) : undefined;
}

export async function assembleInitRoomDataMessage(
  data: InitRoomDataRequest
): Promise<EventMessage<RoomData> | undefined> {
  const { roomId, sender } = data;
  const room = await getRoomById(roomId);
  if (!room) return undefined;
  const roomType = getRoomType(room);
  if (!roomType) return undefined;
  const members = await mapGroupMembersToDTOs(room.groupRoomparticipants);
  const chatPartner = await assembleChatPartnerData(room.privateRoomparticipants, sender);
  return {
    type: SocketEvent.Group.RETURN_INIT_DATA,
    data: {
      id: roomId,
      name: room.name,
      roomType: roomType,
      members: members.length ? members : undefined,
      chatPartner: chatPartner,
    },
  };
}
