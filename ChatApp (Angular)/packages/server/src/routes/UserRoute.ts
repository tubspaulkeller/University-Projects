import express from 'express';

export const userRoutes = express.Router();

// import UserController
import { UsersControler } from '../controllers/Controller.Module';

userRoutes.get('/users', (req, res) => {
  UsersControler.GetAllUsers(req, res);
});
userRoutes.get('/user/:id', (req, res) => {
  UsersControler.GetUser(req, res);
});
