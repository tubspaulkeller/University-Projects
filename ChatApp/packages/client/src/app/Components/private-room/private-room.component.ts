import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from 'src/app/Services/data.service';
import { TokenService } from 'src/app/Services/token.service';
import { Subscription } from 'rxjs';
import { SocketEvent } from '../../Lib/SocketEvent';
import { EventMessage, InitRoomDataRequest, IUser, RoomData } from 'shared';

@Component({
  selector: 'app-private-room',
  templateUrl: './private-room.component.html',
  styleUrls: ['./private-room.component.scss'],
})
export class PrivateRoomComponent implements OnInit {
  chatPartnerName: string | null = null;
  chartPartnerId: string | null = null;
  currentUser: IUser | undefined = undefined;

  currentRoomId?: string;

  currentRoom: RoomData | undefined;

  private dataServiceSubRef: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private dataService: DataService,
    private tokenService: TokenService
  ) {}

  ngOnInit(): void {
    this.currentUser = this.tokenService.GetPayload();
    this.chatPartnerName = this.route.snapshot.paramMap.get('name');
    this.chartPartnerId = this.route.snapshot.paramMap.get('id');

    this.dataServiceSubRef = this.dataService.messages$.subscribe((res: any) => {
      const data = res;
      if (data.type === SocketEvent.Group.RETURN_PRIVATE_ID) {
        this.currentRoomId = data.id;
        // Just trigger creating a new room
        if (this.currentRoomId && this.currentUser) {
          const request: EventMessage<InitRoomDataRequest> = {
            type: SocketEvent.Group.GET_INIT_DATA,
            data: {
              roomId: this.currentRoomId,
              roomType: 'private',
              sender: this.currentUser._id,
            },
          };
          this.dataService.sendMessage(request);
        }
      }

      if (res.type === SocketEvent.Group.RETURN_INIT_DATA) {
        const { data } = res as EventMessage<RoomData>;
        this.currentRoom = data;
      }
    });

    if (this.currentUser) {
      this.dataService.sendMessage({
        type: SocketEvent.Group.GET_OR_CREATE_PRIVATE,
        sender: this.currentUser._id,
        receiver: this.chartPartnerId,
      });
    }
  }
}
