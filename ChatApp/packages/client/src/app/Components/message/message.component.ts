import { Component, Input } from '@angular/core';
import { IReceiveMessage } from 'src/app/Interfaces/IMessage';
import { IUser } from 'shared';
import moment from 'moment';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss'],
})
export class MessageComponent {
  @Input() user!: IUser;
  @Input() receiver_id!: string;
  @Input() message!: IReceiveMessage;

  constructor(private domSanitizer: DomSanitizer) {}

  public TimeFormat(time: any): string {
    return moment(time).format('HH:mm');
  }

  public sanitize(url: any): SafeUrl {
    return this.domSanitizer.bypassSecurityTrustUrl(url);
  }
}
