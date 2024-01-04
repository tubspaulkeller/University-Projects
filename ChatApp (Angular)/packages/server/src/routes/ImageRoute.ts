//express imports
import express = require('express');
export const imageRoutes = express.Router();

//importing controllers
import { ImageController } from '../controllers/Controller.Module';

imageRoutes.post('/set-profile-image/:imgId/:imgVersion', (req, res) => {
  ImageController.SetProfile(req, res);
});
imageRoutes.post('/upload-image', (req, res) => {
  ImageController.uploadImage(req, res);
});
