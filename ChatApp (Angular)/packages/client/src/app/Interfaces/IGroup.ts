export interface IGroup {
  _id: string;
  name: string;
  imgSrc?: string;
  groupRoomparticipants: string[];
  privateRoomparticipants: string[];
}
