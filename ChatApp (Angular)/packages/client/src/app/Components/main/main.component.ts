import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TokenService } from '../../Services/token.service';
import { DataService } from '../../Services/data.service';
import { IGroup } from '../../Interfaces/IGroup';
import { Subscription } from 'rxjs';
import { SocketEvent } from '../../Lib/SocketEvent';

type MainPage = 'Home' | 'Users' | 'Groups' | 'Profilepicture';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnDestroy, OnInit {
  isExpanded = false;

  currentPage: MainPage | string;

  groups: IGroup[] = [];
  selectedGroup: IGroup | undefined;

  private dataServiceSubRef: Subscription | undefined;

  setCurrentPage(page: MainPage | string) {
    this.currentPage = page;
    const selectedGroup = this.groups.find((group) => group.name === page);
    if (selectedGroup) this.selectedGroup = selectedGroup;
  }

  groupHasImage(group?: IGroup): boolean {
    return Boolean(group?.imgSrc);
  }

  constructor(
    private router: Router,
    private tokenService: TokenService,
    private dataService: DataService
  ) {
    this.currentPage = 'Home';
  }

  ngOnInit() {
    this.dataServiceSubRef = this.dataService.messages$.subscribe((res: any) => {
      if (res.type === SocketEvent.Group.RETURN_ALL_GROUPS) {
        this.groups = (res.groups as IGroup[]).sort((a, b) => {
          return a.name.localeCompare(b.name);
        });
      }
    });
    this.dataService.sendMessage({ type: SocketEvent.Group.GET_ALL_GROUPS });
  }

  ngOnDestroy(): void {
    this.dataServiceSubRef?.unsubscribe();
    this.dataService.closeConnection();
  }
}
