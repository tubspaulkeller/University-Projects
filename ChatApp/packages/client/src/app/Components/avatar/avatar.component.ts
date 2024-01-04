import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserService } from '../../Services/user.service';
import { Router } from '@angular/router';
import { TokenService } from '../../Services/token.service';
import { DataService } from 'src/app/Services/data.service';
import { Subscription } from 'rxjs';
import { SocketEvent } from '../../Lib/SocketEvent';

@Component({
  selector: 'app-avatar',
  templateUrl: './avatar.component.html',
  styleUrls: ['./avatar.component.scss'],
})
export class AvatarComponent implements OnInit, OnDestroy {
  private userId = '';
  private userName = '';
  private imageURL = '';
  private dataServiceSubRef: Subscription | undefined;
  private userServiceSubRef: Subscription | undefined;

  constructor(
    private router: Router,
    private userService: UserService,
    private tokenService: TokenService,
    private dataService: DataService
  ) {}

  ngOnInit(): void {
    this.userId = this.tokenService.GetPayload()._id;
    this.getUser();
    this.dataServiceSubRef = this.dataService.messages$.subscribe((res: any) => {
      if (res.type === SocketEvent.User.PROFILE_REFRESHED) {
        this.getUser();
      }
    });
  }

  ngOnDestroy(): void {
    this.dataServiceSubRef?.unsubscribe();
    this.userServiceSubRef?.unsubscribe();
  }

  private getUser(): void {
    if (this.userServiceSubRef) this.userServiceSubRef.unsubscribe();
    this.userServiceSubRef = this.userService.getUserById(this.userId).subscribe({
      next: (user) => {
        this.imageURL = `https://res.cloudinary.com/dmnkxrxes/image/upload/v${user.user.pictureVersion}/${user.user.pictureId}`;
        this.userName = user.user.username;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  getImageURL(): string {
    return this.imageURL;
  }

  getUserName(): string {
    return this.userName;
  }

  logout(): void {
    this.tokenService.DeleteToken();
    this.router.navigate(['/auth']);
  }
}
