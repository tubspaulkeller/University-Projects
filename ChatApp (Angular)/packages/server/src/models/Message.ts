import mongoose, { Schema } from 'mongoose';

interface ChatMessage {
  room: mongoose.Schema.Types.ObjectId;
  message: Message[];
}

export type Message = {
  type: string | null;
  groupName: string;
  groupId: string;
  sender: string;
  receiver: string | null;
  message: string | null;
  gif: string | null;
  imgId: string | null;
  imgVersion: string | null;
  recordingurl: string | null;
  ianaTimezone: string;
  timestamp: string;
  file: File | null;
};

const MessageSchema: Schema = new Schema<ChatMessage>({
  room: { type: mongoose.Schema.Types.ObjectId, ref: 'Room', default: null },
  message: [
    {
      sender: { type: String },
      receiver: { type: String, default: '' },
      message: { type: String, default: '' },
      gif: { type: String, default: '' },
      imgId: { type: String, default: '' },
      imgVersion: { type: String, default: '' },
      recordingurl: { type: String, default: '' },
      ianaTimezone: { type: String, required: true },
      timestamp: { type: String, required: true },
      createdAt: { type: Date, default: Date.now() },
    },
  ],
});

const MessageModel = mongoose.model<ChatMessage>('ChatMessage', MessageSchema);
export { MessageModel };
