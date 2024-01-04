import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ChatInputProps } from '../chat-input.component';
import { DataService } from '../../../../Services/data.service';
import RecordRTC from 'recordrtc';
import { SocketEvent } from '../../../../Lib/SocketEvent';
import { UploadService } from '../../../../Services/upload.service';

@Component({
  selector: 'app-record-audio',
  templateUrl: './record-audio.component.html',
  styleUrls: ['./record-audio.component.scss'],
})
export class RecordAudioComponent {
  isRecording = false;
  record: any;
  private error: any;
  public options = {
    mimeType: 'audio/wav',
    numberOfAudioChannels: 1,
  };

  constructor(
    @Inject(MAT_DIALOG_DATA) public inputProps: ChatInputProps,
    public dialogRef: MatDialogRef<RecordAudioComponent>,
    private dataService: DataService,
    private uploadService: UploadService
  ) {}

  startRecording() {
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
    this.dialogRef.close();
  }

  private processRecording(blob: File): void {
    this.uploadService.uploadFile(
      SocketEvent.Chat.SEND_MESSAGE_AUDIO,
      blob,
      this.inputProps.currentGroup.name,
      this.inputProps.currentUser._id,
      this.inputProps.receiverId ?? '',
      Intl.DateTimeFormat().resolvedOptions().timeZone,
      new Date().toISOString(),
      this.inputProps.currentGroup.id
    );
  }
}
