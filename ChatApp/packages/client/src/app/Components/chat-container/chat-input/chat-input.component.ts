import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { DataService } from '../../../Services/data.service';
import { EventMessage, IsTypingRequest, IUser, RoomData, SocketEventType } from 'shared';
import { SocketEvent } from '../../../Lib/SocketEvent';
import { MatDialog } from '@angular/material/dialog';
import { GifSearchComponent } from './gif-search/gif-search.component';
import { UploadService } from '../../../Services/upload.service';
import { FileUploader } from 'ng2-file-upload';
import { environment } from '../../../../environments/environment';
import { RecordAudioComponent } from './record-audio/record-audio.component';

export interface ChatInputProps {
  currentUser: IUser;
  currentGroup: RoomData;
  receiverId?: string;
}

@Component({
  selector: 'app-chat-input',
  templateUrl: './chat-input.component.html',
  styleUrls: ['./chat-input.component.scss'],
})
export class ChatInputComponent {
  constructor(
    public dialog: MatDialog,
    private dataService: DataService,
    private uploadService: UploadService
  ) {}

  @Input() inputProps?: ChatInputProps;
  message = '';

  showEmojiPicker = false;
  showGifPicker = false;
  activeUserIsTyping: any;

  @ViewChild('fileInput') fileInput?: ElementRef<HTMLInputElement>;
  // selectedFile?: File;

  public uploader: FileUploader = new FileUploader({
    url: environment.img_url,
    disableMultipart: true,
  });

  sendMessage(): void {
    if (this.inputProps) {
      const {
        currentUser,
        receiverId,
        currentGroup: { id, name },
      } = this.inputProps;
      this.dataService.sendMessage({
        type: SocketEvent.Chat.SEND_MESSAGE_TEXT,
        groupName: name,
        groupId: id,
        sender: currentUser._id,
        receiver: receiverId,
        message: this.message,
        gif: null,
        file: null,
        ianaTimezone: Intl.DateTimeFormat().resolvedOptions().timeZone,
        timestamp: new Date().toISOString(),
      });
      // reset input field
      this.message = '';
    }
  }

  selectImage(event: any): void {
    if (this.fileInput) this.fileInput.nativeElement.click();
  }

  onFileSelect(event: any): void {
    // this.selectedFile = event.target.files[0];
    const file: File = event[0];
    if (this.inputProps) {
      this.uploadService.uploadFile(
        SocketEvent.Chat.SEND_MESSAGE_IMAGE,
        file,
        this.inputProps.currentGroup.name,
        this.inputProps.currentUser._id,
        this.inputProps?.receiverId ?? '',
        Intl.DateTimeFormat().resolvedOptions().timeZone,
        new Date().toISOString(),
        this.inputProps.currentGroup.id
      );
      // this.selectedFile = undefined;
    }
  }

  recordAudio(): void {
    const recDialogRec = this.dialog.open(RecordAudioComponent, {
      enterAnimationDuration: '0ms',
      exitAnimationDuration: '0ms',
      data: this.inputProps,
    });

    recDialogRec.afterClosed().subscribe((result) => {
      console.log('The dialog was closed', result);
    });
    console.log('record audio');
  }

  toggleEmojiPicker(): void {
    // console.log('toggle emoji picker');
    const newToggleState = !this.showEmojiPicker;
    this.showEmojiPicker = newToggleState;
    if (newToggleState) {
      this.showGifPicker = false;
    }
  }

  addEmojiToMessage(event: any): void {
    this.message += event.emoji.native;
    this.showEmojiPicker = false;
  }

  openGifPicker(): void {
    const dialogRef = this.dialog.open(GifSearchComponent, {
      enterAnimationDuration: '0ms',
      exitAnimationDuration: '0ms',
      data: this.inputProps,
      width: '75%',
      height: '75%',
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed', result);
    });
  }

  doTyping() {
    if (this.inputProps) {
      const req = this.generateTypingEvent(SocketEvent.Chat.START_TYPING);
      this.dataService.sendMessage(req);
      if (this.activeUserIsTyping) {
        clearTimeout(this.activeUserIsTyping);
      }
      this.activeUserIsTyping = setTimeout(() => {
        const req = this.generateTypingEvent(SocketEvent.Chat.STOP_TYPING);
        this.dataService.sendMessage(req);
      }, 2000);
    }
  }

  generateTypingEvent(eventType: SocketEventType): EventMessage<IsTypingRequest> | undefined {
    if (this.inputProps) {
      return {
        type: eventType,
        groupId: this.inputProps.currentGroup.id,
        data: {
          roomId: this.inputProps.currentGroup.id,
          username: this.inputProps.currentUser.username,
        },
      };
    }
    return undefined;
  }
}
