import { Component, Input } from '@angular/core';
import { IUser } from 'shared';
import { environment } from 'src/environments/environment';
import { DataService } from '../../../Services/data.service';
import { SocketEvent } from '../../../Lib/SocketEvent';

@Component({
  selector: 'app-text-message',
  templateUrl: './text-message.component.html',
  styleUrls: ['./text-message.component.scss'],
})
export class TextMessageComponent {
  @Input() user!: IUser;
  @Input() groupName!: string;
  @Input() receiver_id!: string;
  @Input() roomId?: string;

  public text = '';
  public isEmojiPickerVisible = false;
  public gifs: any[] = [];
  public gif = '';
  public typingMessage: any;

  constructor(private dataService: DataService) {}

  public SendMessage(): void {
    console.log('sending message');
    this.dataService.sendMessage({
      type: SocketEvent.Chat.SEND_MESSAGE_TEXT,
      groupName: this.groupName,
      groupId: this.roomId,
      sender: this.user._id,
      receiver: this.receiver_id,
      message: this.text,
      gif: this.gif,
      file: null,
    });
    this.text = '';
  }

  public async searchGif(): Promise<void> {
    if (this.text.startsWith('@gif ')) {
      const search = this.text.replace('@gif ', '').replace(' ', '+');
      const url = `http://api.giphy.com/v1/gifs/search?api_key=${environment.gif_key}&q=${search}&limit=10`;
      const response = await fetch(url);
      const data = await response.json();
      this.gifs = data.data;
    }
  }

  public sendGif(gif: any): void {
    this.gif = gif.images.original.url;
    this.text = '';
    this.dataService.sendMessage({
      type: SocketEvent.Chat.SEND_MESSAGE_GIF,
      groupName: this.groupName,
      groupId: this.roomId,
      sender: this.user._id,
      receiver: this.receiver_id,
      message: this.text,
      gif: this.gif,
      file: null,
    });
    this.text = '';
    this.gif = '';
    this.gifs = [];
  }

  public addEmoji(event: any): void {
    this.text = `${this.text}${event.emoji.native}`;
    this.isEmojiPickerVisible = false;
  }

  public IsTyping(): void {
    this.pingServer(SocketEvent.Chat.START_TYPING);
    if (this.typingMessage) {
      clearTimeout(this.typingMessage);
    }
    this.typingMessage = setTimeout(() => {
      this.pingServer(SocketEvent.Chat.STOP_TYPING);
    }, 500);
  }

  private pingServer(type: string): void {
    this.dataService.sendMessage({
      type: type,
      groupName: this.groupName,
      sender: this.user._id,
      receiver: this.receiver_id,
    });
  }
}
