import { Component, OnDestroy, OnInit } from '@angular/core';
import { IUser } from 'shared';
import { DataService } from 'src/app/Services/data.service';
import { TokenService } from 'src/app/Services/token.service';
import { Subscription } from 'rxjs';
import { SocketEvent } from '../../Lib/SocketEvent';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss'],
})
export class GroupsComponent implements OnInit, OnDestroy {
  public user!: IUser;
  public groups: any[] = [];
  private dataServiceSubRef: Subscription | undefined;

  constructor(private tokenService: TokenService, private dataService: DataService) {}

  ngOnInit(): void {
    this.user = this.tokenService.GetPayload();
    this.dataServiceSubRef = this.dataService.messages$.subscribe((res: any) => {
      if (res.type === SocketEvent.Group.RETURN_ALL_GROUPS) {
        this.groups = res.groups;
        console.log(this.groups);
      }
    });
    this.dataService.sendMessage({ type: SocketEvent.Group.GET_ALL_GROUPS });
  }

  ngOnDestroy() {
    this.dataServiceSubRef?.unsubscribe();
  }
}
