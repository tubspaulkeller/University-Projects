import { Injectable } from '@angular/core';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';
import { environment } from '../../environments/environment';
import { SocketEvent } from '../Lib/SocketEvent';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  // private socket$?: WebSocketSubject<unknown>;

  // private be$$ = new BehaviorSubject<any>(null);
  //
  // // expose be to outside
  // public be$ = this.be$$.asObservable();
  //
  // private socket$$: WebSocketSubject<unknown> = this.getNewWebSocket().pipe(
  //   (data ) => {
  //   }
  // );

  private socket$: WebSocketSubject<unknown> = this.getNewWebSocket();

  public sendMessage(msg: any) {
    this.socket$?.next(msg);
  }

  public messages$ = this.socket$?.asObservable();

  closeConnection() {
    this.socket$?.complete();
  }

  private getNewWebSocket() {
    return webSocket(environment.websocket_url);
  }

  public getGroupInfo(groupId: string) {
    this.sendMessage({
      type: SocketEvent.Group.GET_INIT_DATA,
      groupId: groupId,
    });
  }
}
