import User from '../models/User';
import { Request, Response } from 'express';
import { Helpers } from '../utils/helpers';
import Joi from 'joi';
import jwt from 'jsonwebtoken';
import { config } from '../../config';
import { addUserToPublicRoom } from '../utils/dbUtils';
import { Types } from 'mongoose';

// eslint-disable-next-line @typescript-eslint/no-var-requires
const bcrypt = require('bcryptjs');

export class Auth {
  public async CreateUser(req: Request, res: Response) {
    const schema = Joi.object().keys({
      username: Joi.string().min(3).max(20).required(),
      email: Joi.string().email().required(),
      password: Joi.string().required(),
    });

    const { error } = schema.validate(req.body);
    if (error && error.details) {
      return res.status(400).json({ msg: error.details[0].message });
    }

    const userEmail = await User.findOne({ email: req.body.email });
    if (userEmail) {
      return res.status(400).json({ message: 'The email already exists' });
    }

    const userName = await User.findOne({ username: Helpers.firstUpper(req.body.username) });
    if (userName) {
      return res.status(400).json({ message: 'The username already exists' });
    }

    const body = {
      username: Helpers.firstUpper(req.body.username),
      email: Helpers.lowerCase(req.body.email),
      password: bcrypt.hashSync(req.body.password, 12),
    };

    try {
      // creating user
      const user = await User.create(body);
      // gen jwt token and set on auth header
      if (config.secret) {
        const token = jwt.sign({ data: user }, config.secret, {
          expiresIn: '24h',
        });
        res.cookie('auth', token);
        // Add user to main room
        const userId = new Types.ObjectId(user._id);
        await addUserToPublicRoom(userId, 'main');
        return res.send({ msg: 'User created successfully', user, token });
      } else {
        throw new Error('Secret key not found');
      }
    } catch (e) {
      return res.status(400).json({ error: e });
    }
  }

  public async LoginUser(req: Request, res: Response) {
    if (!req.body.username || !req.body.password) {
      return res.status(500).json({ message: 'No empty fields allowed' });
    }

    await User.findOne({ username: Helpers.firstUpper(req.body.username) })
      .then((user) => {
        if (!user) {
          return res.status(400).json({ message: 'Username not found' });
        }

        return bcrypt.compare(req.body.password, user.password).then((result: any) => {
          if (!result) {
            return res.status(500).json({ message: 'Password is incorrect' });
          }
          if (config.secret) {
            const token = jwt.sign({ data: user }, config.secret, {
              expiresIn: '24h',
            });
            res.cookie('auth', token);
            return res.status(200).json({ message: 'Login successful', user, token });
          }
          throw new Error('Secret key not found');
        });
      })
      .catch((err) => {
        return res.status(500).json({ message: err.message });
      });
  }
}
