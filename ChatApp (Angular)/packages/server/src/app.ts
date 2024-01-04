import express, { Application } from 'express';
import * as WebSocket from 'ws';
import mongoose = require('mongoose');
import { WebSockets } from './websockets/Websockets';

export class App {
  private app: Application = express();
  private wsServer: WebSocket.Server = new WebSocket.Server({ port: 8080 });

  constructor(
    private port: number,
    middleware: Array<any>,
    routes: Array<express.Router>,
    private apiPath: string
  ) {
    this.middleware(middleware);
    this.routes(routes);
  }

  private middleware(mware: any[]) {
    mware.forEach((m) => {
      this.app.use(m);
    });
  }

  public addMiddleWare(middleWare: any) {
    this.app.use(middleWare);
  }

  private routes(routes: Array<express.Router>) {
    routes.forEach((r) => {
      this.app.use(`${this.apiPath}`, r);
    });
  }

  public listen(): void {
    const server = this.app.listen(this.port, () => {
      console.log(`Server listening at http://localhost:${this.port}`);
    });
    server.on('upgrade', (request, socket, head) => {
      this.wsServer.handleUpgrade(request, socket, head, (ws) => {
        this.wsServer.emit('connection', ws, request);
      });
    });
  }

  public createWebsockets(): void {
    new WebSockets(this.wsServer);
  }

  public mongoDB(uri: string) {
    const connect = () => {
      mongoose
        .connect(uri, {
          useNewUrlParser: true,
          useUnifiedTopology: true,
        } as mongoose.ConnectOptions)
        .then(() => {
          return console.info(`Database successfully connected`);
        })
        .catch((error) => {
          console.log('DATABASE CONNECTION FAILED \n', error);
          return process.exit(1);
        });
    };
    connect();

    mongoose.connection.on('database disconnected', connect);
  }
}
