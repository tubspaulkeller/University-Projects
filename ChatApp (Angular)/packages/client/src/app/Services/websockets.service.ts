import { Injectable } from '@angular/core';
import e from 'express';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ISendMessage } from '../Interfaces/IMessage';
@Injectable({
  providedIn: 'root',
})
export class WebsocketsService {
  websocket!: WebSocket;
  public connect() {
    this.websocket = new WebSocket(environment.websocket_url);
    this.websocket.onopen = (event) => {
      //console.log('Connected to server');
    };
  }

  public disconnect() {
    this.websocket.onclose = (event) => {
      console.log('Disconnected from server');
    };
  }

  public send(message: any) {
    this.websocket.send(JSON.stringify(message));
  }

  public sendChatMessage(message: ISendMessage): void {
    this.websocket.send(JSON.stringify(message));
  }

  public pingServer(message: any): void {
    this.websocket.send(JSON.stringify(message));
  }

  public listen(event: string): Observable<unknown> {
    return new Observable((observer) => {
      this.websocket.onmessage = (event) => {
        observer.next(event.data);
      };
    });
  }
}
