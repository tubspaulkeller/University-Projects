import { App } from './app';
import { middleware } from './middleware/Middleware';
import { authRoutes } from './routes/AuthRoute';
import { userRoutes } from './routes/UserRoute';
import { imageRoutes } from './routes/ImageRoute';
import { config } from '../config';

const app = new App(
  parseInt(config.port),
  middleware,
  [authRoutes, userRoutes, imageRoutes],
  config.api
);
app.listen();

// Websockets
app.createWebsockets();

// connect mongoDB
app.mongoDB(config.db_url);
