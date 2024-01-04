import { Component, OnDestroy, OnInit } from '@angular/core';
import { ImageService } from 'src/app/Services/image.service';
import { TokenService } from 'src/app/Services/token.service';
import { UserService } from 'src/app/Services/user.service';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { DataService } from 'src/app/Services/data.service';
import { Subscription } from 'rxjs';
import { SocketEvent } from '../../Lib/SocketEvent';

@Component({
  selector: 'app-profile-picture',
  templateUrl: './profile-picture.component.html',
  styleUrls: ['./profile-picture.component.scss'],
})
export class ProfilePictureComponent implements OnInit, OnDestroy {
  public uploader: FileUploader = new FileUploader({
    url: environment.img_url,
    disableMultipart: true,
  });

  public user: any;
  public images: any = [];

  private dataServiceSubRef: Subscription | undefined;
  private userServiceSubRef: Subscription | undefined;
  private imageServiceSubRef: Subscription | undefined;

  constructor(
    private tokenService: TokenService,
    private imageService: ImageService,
    private userService: UserService,
    private dataService: DataService
  ) {}

  ngOnInit(): void {
    this.user = this.tokenService.GetPayload();
    this.getUser();
    this.dataServiceSubRef = this.dataService.messages$.subscribe((res: any) => {
      if (res.type === SocketEvent.User.PROFILE_REFRESHED) {
        this.getUser();
      }
    });
  }

  ngOnDestroy() {
    this.dataServiceSubRef?.unsubscribe();
    this.userServiceSubRef?.unsubscribe();
    this.imageServiceSubRef?.unsubscribe();
  }

  private getUser(): void {
    if (this.userServiceSubRef) this.userServiceSubRef.unsubscribe();
    this.userServiceSubRef = this.userService.getUserById(this.user._id).subscribe({
      next: (data) => {
        this.images = data.user.images.reverse();
      },
      error: (err) => console.log(err),
    });
  }

  public OnFileSelected(event: any): void {
    const file: File = event[0];
    const reader = new FileReader();

    reader.readAsDataURL(file);
    reader.onload = () => {
      this.Upload(reader.result);
    };
  }

  public Upload(file: any): void {
    if (file) {
      if (this.imageServiceSubRef) this.imageServiceSubRef.unsubscribe();
      this.imageServiceSubRef = this.imageService.AddImage(this.user._id, file).subscribe({
        next: (data) => {
          file = null;
          this.dataService.sendMessage({ type: SocketEvent.User.REFRESH_PROFILE });
        },
        error: (err) => console.log(err),
      });
    }
  }

  public SetProfileImage(image: any) {
    this.imageService.SetProfileImg(this.user._id, image.imgId, image.imgVersion).subscribe({
      next: (data) => {
        this.dataService.sendMessage({ type: SocketEvent.User.REFRESH_PROFILE });
      },
      error: (err) => console.log(err),
    });
  }
}
