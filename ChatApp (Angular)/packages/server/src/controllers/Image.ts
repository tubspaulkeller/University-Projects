import { Request, Response } from 'express';
import User from '../models/User';
// eslint-disable-next-line @typescript-eslint/no-var-requires
const cloudinary = require('cloudinary');
cloudinary.config({
  cloud_name: 'dmnkxrxes',
  api_key: '981856872212126',
  api_secret: 'LEoeMfjxvJRzL9HQPATUceAVzvY',
});

export class Image {
  public async uploadImage(req: Request, res: Response) {
    const userId: any = req.body.user;
    cloudinary.uploader.upload(req.body.img, async (result: any) => {
      await User.updateOne(
        {
          _id: userId,
        },
        {
          $push: {
            images: {
              imgId: result.public_id,
              imgVersion: result.version,
            },
          },
        }
      )
        .then(() => res.status(200).json({ message: 'Image uploaded successfully' }))
        .catch((err) => res.status(500).json({ message: 'Error uploading image' }));
    });
  }

  public async SetProfile(req: Request, res: Response) {
    const userId: any = req.body.user;

    await User.updateOne(
      {
        _id: userId,
      },
      {
        pictureId: req.params.imgId,
        pictureVersion: req.params.imgVersion,
      }
    )
      .then(() => res.status(200).json({ message: 'Profile picture set' }))
      .catch((err) => res.status(500).json({ message: 'Error setting profile picture' }));
  }
}
