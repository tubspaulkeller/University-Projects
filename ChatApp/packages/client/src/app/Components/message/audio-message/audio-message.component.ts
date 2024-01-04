import { Component, Input } from '@angular/core';
import { IUser } from 'shared';
import { UploadService } from 'src/app/Services/upload.service';
import RecordRTC from 'recordrtc';
import { SocketEvent } from '../../../Lib/SocketEvent';

@Component({
  selector: 'app-audio-message',
  templateUrl: './audio-message.component.html',
  styleUrls: ['./audio-message.component.scss'],
})
export class AudioMessageComponent {
  @Input() user!: IUser;
  @Input() groupName!: string;
  @Input() receiver_id!: string;
  @Input() roomId?: string;

  public isRecording = false;
  public record: any;
  private error: any;
  public selectedFile: any | File;
  public options = {
    mimeType: 'audio/wav',
    numberOfAudioChannels: 1,
  };

  constructor(private uploadService: UploadService) {}

  public startRecording() {
    this.isRecording = true;
    const mediaConstraints = {
      video: false,
      audio: true,
    };
    navigator.mediaDevices
      .getUserMedia(mediaConstraints)
      .then(this.successCallback.bind(this), this.errorCallback.bind(this));
  }

  private successCallback(stream: any) {
    //Start Actuall Recording
    const StereoAudioRecorder = RecordRTC.StereoAudioRecorder;
    this.record = new StereoAudioRecorder(stream);
    this.record.record();
  }

  private errorCallback(error: any): void {
    this.error = 'Can not play audio in your browser';
  }

  public stopRecording(): void {
    this.isRecording = false;
    this.record.stop(this.processRecording.bind(this));
  }

  private processRecording(blob: File): void {
    this.uploadService.uploadFile(
      SocketEvent.Chat.SEND_MESSAGE_AUDIO,
      blob,
      this.groupName,
      this.user._id,
      this.receiver_id,
      this.roomId!,
      Intl.DateTimeFormat().resolvedOptions().timeZone,
      new Date().toISOString()
    );
    this.selectedFile = null;
  }
}
