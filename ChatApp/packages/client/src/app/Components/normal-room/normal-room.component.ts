import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from '../../Services/data.service';
import { Subscription } from 'rxjs';
import { SocketEvent } from '../../Lib/SocketEvent';
import { EventMessage, InitRoomDataRequest, IUser, RoomData } from 'shared';
import { TokenService } from '../../Services/token.service';

@Component({
  selector: 'app-normal-room',
  templateUrl: './normal-room.component.html',
  styleUrls: ['./normal-room.component.scss'],
})
export class NormalRoomComponent implements OnInit, OnDestroy {
  currentRoomId: string | null = null;
  currentRoom: RoomData | undefined;

  currentUser: IUser | undefined = undefined;
  private dataServiceSubRef: Subscription | undefined;

  ngOnDestroy() {
    this.dataServiceSubRef?.unsubscribe();
  }

  constructor(
    private route: ActivatedRoute,
    private dataService: DataService,
    private tokenService: TokenService
  ) {
    this.currentRoomId = this.route.snapshot.paramMap.get('id');
    this.currentUser = this.tokenService.GetPayload();
  }

  ngOnInit() {
    this.dataServiceSubRef = this.dataService.messages$.subscribe((res: any) => {
      if (res.type === SocketEvent.Group.RETURN_INIT_DATA) {
        const { data } = res as EventMessage<RoomData>;
        this.currentRoom = data;
        console.log('current room', this.currentRoom);
      }
    });

    if (this.currentRoomId && this.currentUser) {
      const initDataRequest: EventMessage<InitRoomDataRequest> = {
        type: SocketEvent.Group.GET_INIT_DATA,
        data: {
          roomId: this.currentRoomId,
          roomType: 'public',
          sender: this.currentUser._id,
        },
      };
      this.dataService.sendMessage(initDataRequest);
    }
  }
}
