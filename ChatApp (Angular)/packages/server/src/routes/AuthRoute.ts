//express imports
import express = require('express');
export const authRoutes = express.Router();

//importing controllers
import { AuthController } from '../controllers/Controller.Module';

authRoutes.post('/register', (req, res) => {
  AuthController.CreateUser(req, res);
});
authRoutes.post('/login', (req, res) => {
  AuthController.LoginUser(req, res);
});
