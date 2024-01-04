import { Component, Input } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { IUser } from 'shared';
import { UploadService } from 'src/app/Services/upload.service';
import { environment } from 'src/environments/environment';
import { SocketEvent } from '../../../Lib/SocketEvent';

@Component({
  selector: 'app-image-message',
  templateUrl: './image-message.component.html',
  styleUrls: ['./image-message.component.scss'],
})
export class ImageMessageComponent {
  @Input() user!: IUser;
  @Input() groupName!: string;
  @Input() receiver_id!: string;
  @Input() roomId?: string;

  public imgFileName = '';
  public selectedFile: any | File;
  public uploader: FileUploader = new FileUploader({
    url: environment.img_url,
    disableMultipart: true,
  });

  constructor(private uploadService: UploadService) {}

  public OnFileSelected(event: any): void {
    const file: File = event[0];
    this.uploadService.uploadFile(
      SocketEvent.Chat.SEND_MESSAGE_IMAGE,
      file,
      this.groupName,
      this.user._id,
      this.receiver_id,
      this.roomId!,
      Intl.DateTimeFormat().resolvedOptions().timeZone,
      new Date().toISOString()
    );
    this.selectedFile = null;
    this.imgFileName = '';
  }
}
