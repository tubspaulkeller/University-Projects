import { Injectable } from '@angular/core';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class UploadService {
  private reader: any = new FileReader();
  private selectedFile: any;

  constructor(private dataService: DataService) {}

  public uploadFile(
    type: string,
    file: File,
    groupName: string,
    user: string,
    receiver: string,
    ianaTimezone: string,
    timestamp: string,
    groupId?: string
  ): void {
    this.reader.readAsDataURL(file);
    this.reader.onload = () => {
      this.selectedFile = this.reader.result;
      this.dataService.sendMessage({
        type: type,
        groupName: groupName,
        groupId: groupId,
        sender: user,
        receiver: receiver,
        message: '',
        gif: '',
        file: this.selectedFile,
        ianaTimezone: ianaTimezone,
        timestamp: timestamp,
      });
    };
  }
}
