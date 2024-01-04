export type RoomType = 'private' | 'public' | 'main';

export interface RoomData {
  id: string;
  name: string;
  members?: UserData[]; // set when in main or public room
  chatPartner?: UserData; // set when in private room
  roomType: RoomType;
}

export interface UserData {
  id: string;
  name: string;
  imgSrc: string;
}

export interface RoomIdData {
  id: string;
}

export interface InitRoomDataRequest {
  roomId: string;
  roomType: RoomType;
  sender?: string; // undefined / null if requesting for main room
}

export interface InitMessagesResponse {
  messages: any[];
}

export type ChatMessageType = 'text' | 'image' | 'audio' | 'gif';

export interface ChatMessageData {
  type: ChatMessageType;
  isCurrentUser?: boolean;
  sender: UserData;
  timestamp: string; // will be UTC
  ianaTimezone: string;
  content: MessageContent;
}

export type MessageContent =
  | { imgId: string; imgVersion: string }
  | { gifUrl: string }
  | { recordingUrl: string }
  | string
  | null;

export interface IsTypingRequest {
  roomId: string;
  username: string;
}

export interface IsTypingResponse {
  username: string;
}
