import mongoose, { Schema } from 'mongoose';

interface IRoom {
  name: string;
  privateRoomparticipants: any[];
  groupRoomparticipants: any[];
  imgSrc: string;
}

export type Room = {
  _id: any;
  name: string;
  privateRoomparticipants: any[];
  groupRoomparticipants: any[];
  imgSrc: string;
};

const RoomSchema: Schema = new Schema<IRoom>({
  name: { type: String, default: '' },
  privateRoomparticipants: [
    {
      sender: { type: mongoose.Schema.Types.ObjectId, ref: 'User', default: '' },
      receiver: { type: mongoose.Schema.Types.ObjectId, ref: 'User', default: '' },
    },
  ],
  groupRoomparticipants: [{ type: mongoose.Schema.Types.ObjectId, ref: 'User', default: null }],
  imgSrc: { type: String, default: '' },
});

const RoomModel = mongoose.model<IRoom>('Room', RoomSchema);
export { RoomModel };
