import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IUser } from 'shared';
import { IReceiveMessage } from 'src/app/Interfaces/IMessage';
import { TokenService } from 'src/app/Services/token.service';
import { UserService } from 'src/app/Services/user.service';
import { DataService } from '../../Services/data.service';
import { Subscription } from 'rxjs';
import { SocketEvent } from '../../Lib/SocketEvent';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
})
export class ChatComponent implements OnInit, OnDestroy {
  public user!: IUser;
  public receiver_id!: string;
  public sender!: any;
  public receiver!: any;
  public receivers: IUser[] = [];
  public messageArr: IReceiveMessage[] = [];
  public typing = false;

  @Input() roomId?: string;

  private dataServiceSubRef: Subscription | undefined;

  constructor(
    private tokenService: TokenService,
    private route: ActivatedRoute,
    private userService: UserService,
    private dataService: DataService
  ) {}

  ngOnInit(): void {
    this.user = this.tokenService.GetPayload();
    this.receiver_id = '';
    this.subscribeToMessageEvents();
    console.log('roomId', this.roomId);
    this.dataService.sendMessage({
      type: SocketEvent.Chat.GET_INIT_MESSAGES,
      roomId: this.roomId,
    });
  }

  ngOnDestroy(): void {
    this.dataServiceSubRef?.unsubscribe();
  }

  private subscribeToMessageEvents(): void {
    console.log('subscribing to messages in ChatComponent');
    this.dataServiceSubRef = this.dataService.messages$?.subscribe((res: any) => {
      const data = res;
      // console.log('type', data.type);
      switch (data.type) {
        // Get Data from DB
        case SocketEvent.Chat.RETURN_INIT_MESSAGES:
          // console.log('All messages', data);
          if (data.message) {
            this.messageArr = data.message.message;
          }
          break;
        // Get Data from Server
        case SocketEvent.Chat.MESSAGE_RETURN_TEXT:
          console.log('message', data);
          this.messageArr.push(data.message);
          break;
        case SocketEvent.Chat.MESSAGE_RETURN_IMAGE:
          this.messageArr.push(data.message);
          break;
        case SocketEvent.Chat.MESSAGE_RETURN_GIF:
          this.messageArr.push(data.message);
          break;
        case SocketEvent.Chat.MESSAGE_RETURN_AUDIO:
          this.messageArr.push(data.message);
          break;
        case SocketEvent.Chat.USER_STARTED_TYPING:
          this.emitTyping(data);
          break;
        case SocketEvent.Chat.USER_STOPPED_TYPING:
          this.emitTyping(data);
          break;
        default:
          break;
      }
    });
  }

  private emitTyping(data: any): void {
    if (data.message.type === 'isTyping') {
      this.userService.getUserById(data.message.sender).subscribe((typer) => {
        this.sender = typer.user;
        if (this.sender._id !== this.user._id) {
          this.typing = true;
        }
      });
    }
    if (data.message.type === 'stopTyping') {
      this.typing = false;
    }
  }
}
