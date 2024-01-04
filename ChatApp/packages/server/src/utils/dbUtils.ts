import { Types } from 'mongoose';
import { Room, RoomModel } from '../models/Room';

export async function addUserToPublicRoom(userId: Types.ObjectId, roomName: string): Promise<void> {
  const room = await RoomModel.findOne({ name: roomName });
  if (room && !room.groupRoomparticipants.includes(userId)) {
    room.groupRoomparticipants.push(userId);
    await room.save();
    return;
  }
  const newRoom = new RoomModel();
  newRoom.name = roomName;
  newRoom.groupRoomparticipants.push(userId);
  await newRoom.save();
  return;
}

export async function getRoomById(roomId: string): Promise<Room | undefined> {
  const room = await RoomModel.findById(roomId);
  return room === null ? undefined : room;
}
