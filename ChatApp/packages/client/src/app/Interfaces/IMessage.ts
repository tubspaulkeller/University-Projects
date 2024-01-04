export interface IReceiveMessage {
  type: string;
  sender: string;
  receiver: string;
  message: string | null;
  gif: string | null;
  imgId: string | null;
  imgVersion: string | null;
  recordingurl: string | null;
  ianaTimezone: string;
  timestamp: string;
  createdAt: Date;
}

export interface ISendMessage {
  type: string; // eventType
  groupName: string | undefined;
  groupId: string | undefined;
  sender: string;
  receiver: string | null | undefined;
  message: string | null | undefined;
  gif: string | null | undefined;
  ianaTimezone: string;
  timestamp: string;
  file: File | null | undefined;
}
