import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { environment } from 'src/environments/environment';
import { FileUploader } from 'ng2-file-upload';

// TODO Add zod for validation
@Component({
  selector: 'app-add-group-dialog',
  templateUrl: './add-group-dialog.component.html',
  styleUrls: ['./add-group-dialog.component.scss'],
})
export class AddGroupDialogComponent {
  public imgFileName = '';
  public selectedFile: any | File;
  private reader: any = new FileReader();
  public uploader: FileUploader = new FileUploader({
    url: environment.img_url,
    disableMultipart: true,
  });

  constructor(
    public dialogRef: MatDialogRef<AddGroupDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { name: string; file: File }
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  public OnFileSelected(event: any): void {
    const file: File = event[0];
    this.reader.readAsDataURL(file);
    this.reader.onload = () => {
      this.selectedFile = this.reader.result;
      this.data.file = this.selectedFile;
    };

    this.selectedFile = null;
    this.imgFileName = '';
  }
}
