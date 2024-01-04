import { Component, OnDestroy, OnInit } from '@angular/core';
import { DataService } from 'src/app/Services/data.service';
import { Subscription } from 'rxjs';
import { SocketEvent } from '../../Lib/SocketEvent';
import { EventMessage, InitRoomDataRequest, RoomData, RoomIdData } from 'shared';

@Component({
  selector: 'app-main-room',
  templateUrl: './main-room.component.html',
  styleUrls: ['./main-room.component.scss'],
})
export class MainRoomComponent implements OnInit, OnDestroy {
  currentRoom: RoomData | undefined;
  private subcriptionRef: Subscription | undefined;

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    this.subcriptionRef = this.dataService.messages$.subscribe((res: any) => {
      if (res.type === SocketEvent.Group.RETURN_MAIN_ID) {
        const { data } = res as EventMessage<RoomIdData>;
        const request: EventMessage<InitRoomDataRequest> = {
          type: SocketEvent.Group.GET_INIT_DATA,
          data: {
            roomId: data.id,
            roomType: 'main',
          },
        };
        this.dataService.sendMessage(request);
      }
      if (res.type === SocketEvent.Group.RETURN_INIT_DATA) {
        const { data } = res as EventMessage<RoomData>;
        this.currentRoom = data;
      }
    });
    this.dataService.sendMessage({
      type: SocketEvent.Group.GET_MAIN_ID,
    });
  }

  ngOnDestroy() {
    this.subcriptionRef?.unsubscribe();
  }
}
