import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserService } from 'src/app/Services/user.service';
import { IUser } from 'shared';
import { TokenService } from 'src/app/Services/token.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
})
export class UsersComponent implements OnInit, OnDestroy {
  constructor(private userService: UserService, private tokenService: TokenService) {}

  public users: IUser[] = [];
  public currentUser: IUser = this.tokenService.GetPayload();

  private userServiceSubRef: Subscription | undefined;

  ngOnInit(): void {
    this.GetUsers();
  }

  ngOnDestroy() {
    this.userServiceSubRef?.unsubscribe();
  }

  private GetUsers(): void {
    this.userServiceSubRef = this.userService.getAllUsers().subscribe((data: any) => {
      this.users = data.users;
    });
  }
}
