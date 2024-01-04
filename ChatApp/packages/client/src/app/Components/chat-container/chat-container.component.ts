import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import {
  ChatMessageData,
  EventMessage,
  InitMessagesResponse,
  IUser,
  RoomData,
  UserData,
} from 'shared';
import { ChatInputProps } from './chat-input/chat-input.component';
import { TokenService } from '../../Services/token.service';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../Services/user.service';
import { DataService } from '../../Services/data.service';
import { filter, Subscription } from 'rxjs';
import { SocketEvent } from '../../Lib/SocketEvent';
import { transformToChatMessageData } from './transformUtils';
import { IReceiveMessage } from '../../Interfaces/IMessage';

@Component({
  selector: 'app-chat-container',
  templateUrl: './chat-container.component.html',
  styleUrls: ['./chat-container.component.scss'],
})
export class ChatContainerComponent implements OnInit, OnDestroy {
  private dataServiceSubRef: Subscription | undefined;
  receiverId: string | undefined = undefined;
  @Input() isDeleteEnabled = false;
  @Input() currentRoom?: RoomData;

  currentUser: IUser | undefined = undefined;
  messages: ChatMessageData[] = [];
  isTyping = false;
  typingUsers: string[] = [];

  constructor(
    private tokenService: TokenService,
    private route: ActivatedRoute,
    private userService: UserService,
    private dataService: DataService
  ) {}

  ngOnInit() {
    this.currentUser = this.tokenService.GetPayload();
    this.subscribeToMessageEvents();
    // console.log('Getting init messages for room:', this.currentRoom?.id);
    this.dataService.sendMessage({
      type: SocketEvent.Chat.GET_INIT_MESSAGES,
      roomId: this.currentRoom?.id,
    });
  }

  ngOnDestroy(): void {
    this.dataServiceSubRef?.unsubscribe();
  }

  private handleInitMsgEvent(res: any) {
    const response = res as EventMessage<InitMessagesResponse>;
    this.messages = response.data.messages.map((msg: IReceiveMessage) =>
      transformToChatMessageData(msg, this.currentUser!, this.currentRoom!)
    );
  }

  private handleChatMsgEvent(res: any) {
    if (this.currentRoom && this.currentUser) {
      this.messages.push(
        transformToChatMessageData(
          res.message as IReceiveMessage,
          this.currentUser,
          this.currentRoom
        )
      );
    }
  }

  private handleStartTypingEvent(res: any) {
    const isMeTyping = res.data.username === this.currentUser?.username;
    const isMeIncluded = this.typingUsers.includes(res.data.username);
    if (isMeTyping || isMeIncluded) return;
    this.isTyping = true;
    this.typingUsers.push(res.data.username);
  }

  private handleStopTypingEvent(res: any) {
    const isMeTyping = res.data.username === this.currentUser?.username;
    if (isMeTyping) return;
    this.typingUsers = this.typingUsers.filter((user) => user !== res.data.username);
    if (!this.typingUsers.length) this.isTyping = false;
  }
  private subscribeToMessageEvents(): void {
    this.dataServiceSubRef = this.dataService.messages$
      .pipe(filter((res: any) => res.groupId === this.currentRoom?.id))
      .subscribe((res: any) => {
        switch (res.type) {
          case SocketEvent.Chat.RETURN_INIT_MESSAGES:
            this.handleInitMsgEvent(res);
            break;
          case SocketEvent.Chat.MESSAGE_RETURN_TEXT:
          case SocketEvent.Chat.MESSAGE_RETURN_IMAGE:
          case SocketEvent.Chat.MESSAGE_RETURN_AUDIO:
          case SocketEvent.Chat.MESSAGE_RETURN_GIF:
            this.handleChatMsgEvent(res);
            break;
          case SocketEvent.Chat.USER_STARTED_TYPING:
            this.handleStartTypingEvent(res);
            break;
          case SocketEvent.Chat.USER_STOPPED_TYPING:
            this.handleStopTypingEvent(res);
            break;
          default:
            break;
        }
      });
  }

  isPrivateRoom(): boolean {
    return Boolean(this.currentRoom?.roomType === 'private');
  }

  isRoomValid(): boolean {
    return Boolean(this.currentUser && this.currentRoom);
  }

  getChatInputProps(): ChatInputProps {
    return {
      // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
      currentUser: this.currentUser!,
      // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
      currentGroup: this.currentRoom!,
      receiverId: this.receiverId,
    };
  }

  getMemberListName(member: UserData): string {
    return member.id === this.currentUser?._id ? `${member.name} (You)` : member.name;
  }

  openGroupSettings() {
    console.log('toggle settings');
  }

  getTypingString() {
    if (this.isTyping) {
      const postfix = this.typingUsers.length > 1 ? 'are' : 'is';
      return `${this.typingUsers.join(', ')} ${postfix} typing...`;
    }
    return null;
  }

  isMe(member: UserData): boolean {
    return member.id === this.currentUser?._id;
  }
}
