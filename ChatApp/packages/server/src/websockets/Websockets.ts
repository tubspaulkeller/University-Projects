import * as WebSocket from 'ws';
// eslint-disable-next-line @typescript-eslint/ban-ts-comment
// @ts-ignore
import { v4 as uuidv4 } from 'uuid';
import { Message, MessageModel } from '../models/Message';
import { Room, RoomModel } from '../models/Room';
import { config } from '../../config';
import { SocketEvent } from './types';
import { assembleInitRoomDataMessage } from '../utils/dataTransformUtils';
import {
  EventMessage,
  InitMessagesResponse,
  InitRoomDataRequest,
  IsTypingRequest,
  IsTypingResponse,
  RoomIdData,
  SocketEventType,
} from 'shared';
import { Types } from 'mongoose';

// eslint-disable-next-line @typescript-eslint/no-var-requires
const cloudinary = require('cloudinary');
cloudinary.config({
  cloud_name: config.cloudinary_name,
  api_key: config.cloudinary_api_key,
  api_secret: config.cloudinary_api_secret,
});

export class WebSockets {
  private wss!: WebSocket.Server;

  // has user uuid -> websocket assignments
  userMap;
  // has group id -> user uuid assignments - what users are in which group
  connectionMap;

  constructor(websocketserver: WebSocket.Server) {
    this.wss = websocketserver;
    this.connect();
    this.userMap = new Map<string, WebSocket>();
    this.connectionMap = new Map<string, string[]>();
    // TODO when are users added to MainRoom??
  }

  private connect(): void {
    this.wss.on('connection', (ws: WebSocket) => {
      console.log('connected to Websocket');
      const uuid: any = uuidv4();
      this.userMap.set(uuid, ws);
      this.onMessageHandler(ws, uuid);
      ws.onclose = () => {
        // remove from userConnections
        this.cleanUpConnection(uuid);
        console.log('disconnected from Websocket');
      };
    });
  }

  private cleanUpConnection(uuid: string): void {
    // remove user from all groups
    for (const groupId of Array.from(this.connectionMap.keys())) {
      const connections = this.connectionMap.get(groupId);
      const index = connections?.indexOf(uuid);
      if (index && index > -1) {
        connections?.splice(index, 1);
      }
    }
    // remove from userMap
    this.userMap.delete(uuid);
  }

  private onMessageHandler(ws: WebSocket, uuid: string): void {
    ws.on('message', (message: any) => {
      const data = JSON.parse(message);
      console.log(data.type, uuid);
      switch (data.type) {
        case SocketEvent.Group.GET_MAIN_ID:
          this.getMainChannelId(ws, uuid);
          break;

        case SocketEvent.Group.GET_INIT_DATA:
          this.handleInitRoomDataRequest(uuid, data, ws);
          break;

        case SocketEvent.Chat.GET_INIT_MESSAGES:
          this.getInitMessages(data.roomId, ws);
          break;

        case SocketEvent.Group.GET_ALL_GROUPS:
          this.getGroups(ws);
          break;

        case SocketEvent.Group.ADD_GROUP:
          this.addGroup(data, ws);
          break;

        case SocketEvent.Group.GET_OR_CREATE_PRIVATE:
          this.getOrCreatePrivateRoom(data.sender, data.receiver, ws);
          break;

        case SocketEvent.Chat.SEND_MESSAGE_TEXT:
          this.sendTextMessage(data);
          break;

        case SocketEvent.Chat.SEND_MESSAGE_GIF:
          this.sendGifMessage(data);
          break;
        case SocketEvent.Chat.SEND_MESSAGE_AUDIO:
          this.sendRecMessage(data);
          break;

        case SocketEvent.Chat.SEND_MESSAGE_IMAGE:
          this.sendImgMessage(data);
          break;

        case SocketEvent.Chat.START_TYPING:
          this.sendStartOrStopTypingEvent(data, SocketEvent.Chat.USER_STARTED_TYPING);
          break;

        case SocketEvent.Chat.STOP_TYPING:
          this.sendStartOrStopTypingEvent(data, SocketEvent.Chat.USER_STOPPED_TYPING);
          break;

        case SocketEvent.User.REFRESH_PROFILE:
          ws.send(JSON.stringify({ type: SocketEvent.User.PROFILE_REFRESHED }));
          break;

        default:
          console.log('default', data);
          break;
      }
    });
  }

  private async handleInitRoomDataRequest(
    uuid: string,
    eventMsg: EventMessage<InitRoomDataRequest>,
    ws: WebSocket
  ): Promise<void> {
    const { roomType } = eventMsg.data;
    // Add user to db group if he's not there, for private rooms this is already done at room creation
    if (roomType !== 'private')
      await this.addUserToDbGroup(eventMsg.data?.sender, eventMsg.data.roomId);
    // Add user to group if not already in group
    await this.addUserSocketToGroup(uuid, eventMsg.data.roomId);
    // Send init data to user
    await this.getInitRoomData(eventMsg.data, ws);
  }

  private async addUserToDbGroup(userId: string | null | undefined, roomId: string): Promise<void> {
    if (!userId) return;
    const room = await this.getRoomById(roomId);
    if (!room) return;
    const roomUsers = room.groupRoomparticipants;
    if (!roomUsers.includes(new Types.ObjectId(userId))) {
      roomUsers.push(userId);
      await room.save();
    }
  }

  private async addUserSocketToGroup(uuid: string, groupId: string): Promise<void> {
    const room = await this.getRoomById(groupId);
    if (!room) return;
    const roomId = room._id.toString();
    const doesRoomExist = this.connectionMap.has(roomId);
    if (doesRoomExist) {
      const connections = this.connectionMap.get(roomId);
      if (!connections?.includes(uuid)) {
        connections?.push(uuid);
      } else console.log('already found user in room', connections);
    } else {
      this.connectionMap.set(roomId, [uuid]);
    }
  }

  private sendToAllUsersInGroup(groupId: string, message: any, eventType: string) {
    const connections = this.connectionMap.get(groupId);
    console.log('connections', connections);
    if (connections) {
      connections.forEach((connection) => {
        console.log('sending', eventType, 'to user', connection);
        const ws = this.userMap.get(connection);
        console.log('found socket for user');
        if (ws) ws.send(JSON.stringify({ type: eventType, message, groupId }));
      });
    }
  }

  private async getInitRoomData(data: InitRoomDataRequest, ws: WebSocket): Promise<void> {
    const response = await assembleInitRoomDataMessage(data);
    ws.send(JSON.stringify(response));
  }

  private async getPrivateRoom(id1: string, id2: string) {
    return RoomModel.findOne({
      $or: [
        {
          $and: [
            { 'privateRoomparticipants.sender': id1 },
            { 'privateRoomparticipants.receiver': id2 },
          ],
        },
        {
          $and: [
            { 'privateRoomparticipants.sender': id2 },
            { 'privateRoomparticipants.receiver': id1 },
          ],
        },
      ],
    });
  }

  private async getOrCreatePrivateRoom(id1: string, id2: string, ws: WebSocket) {
    const room = await this.getPrivateRoom(id1, id2);
    let roomId = room?._id;
    if (!room) {
      const newRoom = new RoomModel();
      newRoom.name = 'private';
      newRoom.privateRoomparticipants.push({
        sender: id1,
        receiver: id2,
      });
      roomId = (await newRoom.save())._id;
    }
    const id = String(roomId);

    ws.send(JSON.stringify({ type: SocketEvent.Group.RETURN_PRIVATE_ID, id }));
  }

  private async getMainChannelId(ws: WebSocket, uuid: any) {
    const room = await RoomModel.findOne({ name: 'main' });
    console.log('Trying to find main channel...found', String(room?._id));
    let roomId = room?._id;
    if (!room) {
      const newRoom = new RoomModel();
      newRoom.name = 'main';
      roomId = (await newRoom.save())._id;
    }
    const response: EventMessage<RoomIdData> = {
      type: SocketEvent.Group.RETURN_MAIN_ID,
      data: { id: String(roomId) },
    };
    ws.send(JSON.stringify(response));
  }

  private async getRoomById(roomId: string) {
    return RoomModel.findById(roomId);
  }

  private async getGroups(ws: WebSocket): Promise<void> {
    await RoomModel.find({
      $and: [{ name: { $ne: 'private' } }, { name: { $ne: 'main' } }],
    }).then((rooms) => {
      if (rooms) {
        ws.send(JSON.stringify({ type: SocketEvent.Group.RETURN_ALL_GROUPS, groups: rooms }));
      }
    });
  }

  private async uploadGroupImage(file: any): Promise<string> {
    return cloudinary.uploader
      .upload(file, async () => {
        console.log('room pic uploaded');
      })
      .then(async (res: any) => {
        return await `https://res.cloudinary.com/${config.cloudinary_name}/image/upload/v${res.version}/${res.public_id}`;
      });
  }

  private async addGroup(message: Message, ws: WebSocket): Promise<void> {
    let groupImg: string;
    if (message.file) {
      groupImg = await this.uploadGroupImage(message.file);
    } else {
      groupImg = '';
    }
    const room = await RoomModel.findOne({ name: message.groupName });
    if (room) {
      if (!(await RoomModel.findOne({ groupRoomparticipants: message.sender }))) {
        await room?.updateOne({
          $push: { groupRoomparticipants: message.sender },
        });
      }
      this.getGroups(ws);
    } else {
      const newRoom = new RoomModel();
      newRoom.name = message.groupName;
      newRoom.groupRoomparticipants.push(message.sender);
      newRoom.imgSrc = groupImg;
      await newRoom.save();
    }

    this.getGroups(ws);
  }

  private async getInitMessages(roomId: string, ws: WebSocket): Promise<void> {
    const msgRes = await MessageModel.findOne({ room: roomId }).select('message');
    if (msgRes) {
      const res: EventMessage<InitMessagesResponse> = {
        type: SocketEvent.Chat.RETURN_INIT_MESSAGES,
        groupId: roomId,
        data: {
          messages: msgRes.message,
        },
      };
      ws.send(JSON.stringify(res));
    }
  }

  private async sendTextMessage(message: Message): Promise<void> {
    const room = await this.getRoomById(message.groupId);
    if (message.message && room) {
      this.saveMessage(room, message);
      this.sendToAllUsersInGroup(
        room._id.toString(),
        message,
        SocketEvent.Chat.MESSAGE_RETURN_TEXT
      );
    }
  }

  private async sendGifMessage(message: Message): Promise<void> {
    const room = await this.getRoomById(message.groupId);
    if (message.gif && room) {
      this.saveMessage(room, message);
      this.sendToAllUsersInGroup(room._id.toString(), message, SocketEvent.Chat.MESSAGE_RETURN_GIF);
    }
  }

  private async sendRecMessage(message: Message): Promise<void> {
    const room = await this.getRoomById(message.groupId);
    if (room && message.file) {
      cloudinary.v2.uploader
        .upload(message.file, { resource_type: 'video' }, async () => {
          console.log('uploaded');
        })
        .then((res: any) => {
          message.recordingurl = res.url;
          this.saveMessage(room, message);
          this.sendToAllUsersInGroup(
            room._id.toString(),
            message,
            SocketEvent.Chat.MESSAGE_RETURN_AUDIO
          );
        });
    }
  }

  private async sendImgMessage(message: Message): Promise<void> {
    console.log('ImageMsg', message);
    const room = await this.getRoomById(message.groupId);

    if (room && message.file) {
      cloudinary.uploader
        .upload(message.file, async () => {
          console.log('uploaded');
        })
        .then((res: any) => {
          console.log('Successfully uploaded');
          message.imgId = res.public_id;
          message.imgVersion = res.version;
          this.saveMessage(room, message);
          this.sendToAllUsersInGroup(
            room._id.toString(),
            message,
            SocketEvent.Chat.MESSAGE_RETURN_IMAGE
          );
        });
    }
  }

  private sendStartOrStopTypingEvent(message: any, event: SocketEventType) {
    const eventMsg = message as EventMessage<IsTypingRequest>;
    const res: EventMessage<IsTypingResponse> = {
      type: event,
      groupId: eventMsg.groupId,
      data: {
        username: eventMsg.data.username,
      },
    };

    this.sendEventMessageToGroup(res, eventMsg.data.roomId);
  }

  private sendEventMessageToGroup(msg: EventMessage<any>, roomId: string) {
    const connections = this.connectionMap.get(roomId);
    if (connections) {
      connections.forEach((connection) => {
        const ws = this.userMap.get(connection);
        if (ws) ws.send(JSON.stringify(msg));
      });
    }
  }

  private async saveMessage(room: Room, message: Message): Promise<void> {
    const messages = await MessageModel.findOne({ room: room._id });
    if (!messages) console.log('no messages found');
    try {
      if (messages) {
        console.log('Room with message found');
        await MessageModel.updateOne(
          { room: room._id },
          {
            $push: {
              message: {
                type: message.type,
                groupName: message.groupName,
                sender: message.sender,
                receiver: message.receiver,
                message: message.message,
                gif: message.gif,
                imgId: message.imgId,
                imgVersion: message.imgVersion,
                recordingurl: message.recordingurl,
                file: null,
                ianaTimezone: message.ianaTimezone,
                timestamp: message.timestamp,
              },
            },
          }
        );
      } else {
        console.log('Room without any messages');
        const newMessage = new MessageModel();
        newMessage.room = room._id;
        newMessage.message.push({
          type: message.type,
          groupName: message.groupName,
          groupId: message.groupId,
          sender: message.sender,
          receiver: message.receiver,
          message: message.message,
          gif: message.gif,
          imgId: message.imgId,
          imgVersion: message.imgVersion,
          recordingurl: message.recordingurl,
          ianaTimezone: message.ianaTimezone,
          timestamp: message.timestamp,
          file: null,
        });
        await newMessage.save();
      }
    } catch (e) {
      console.log(e);
    }
  }
}
