import { Component, Input } from '@angular/core';
import { ChatMessageData } from 'shared';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-message-container',
  templateUrl: './message-container.component.html',
  styleUrls: ['./message-container.component.scss'],
})
export class MessageContainerComponent {
  @Input() messages?: ChatMessageData[];

  constructor(private domSanitizer: DomSanitizer) {}

  formatTime(timestamp: string, ianaTimezone: string): string {
    const date = new Date(timestamp);
    return date.toLocaleString(Intl.NumberFormat().resolvedOptions().locale, {
      timeZone: ianaTimezone,
      weekday: 'short',
      hour: 'numeric',
      minute: 'numeric',
    });
  }

  buildImageUrl(message: ChatMessageData): string {
    const { imgId, imgVersion } = message.content as any;
    return `https://res.cloudinary.com/dmnkxrxes/image/upload/v${imgVersion}/${imgId}`;
  }

  getGifUrl(message: ChatMessageData): string {
    return (message.content as any).gifUrl as string;
  }

  getRecordingUrl(message: ChatMessageData): string {
    return (message.content as any).recordingUrl as string;
  }

  public sanitize(url: any): SafeUrl {
    return this.domSanitizer.bypassSecurityTrustUrl(url);
  }
}
