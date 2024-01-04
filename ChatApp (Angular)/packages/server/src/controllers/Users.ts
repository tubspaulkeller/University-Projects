import { Request, Response } from 'express';
import User from '../models/User';

export class Users {
  public async GetUser(req: Request, res: Response) {
    const userId: any = req.params.id;
    await User.findOne({ _id: userId })
      .then((user) => {
        res.status(200).json({ message: 'User found', user });
      })
      .catch((err) => res.status(500).json({ message: 'Error occured' }));
  }

  public async GetAllUsers(req: Request, res: Response) {
    User.find({})
      .then((users) => {
        return res.status(200).json({ msg: 'All users', users });
      })
      .catch((err) => {
        return res.status(400).json({ msg: 'No users found', err });
      });
  }
}
