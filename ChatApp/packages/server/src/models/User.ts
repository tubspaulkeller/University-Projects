import mongoose, { Schema } from 'mongoose';
import { IUser } from '../../../shared/src/types/User';

const UserSchema: Schema = new Schema<IUser>({
  username: { type: String },
  email: { type: String },
  password: { type: String },
  pictureVersion: { type: String, default: '1664097849' },
  pictureId: { type: String, default: 'avatar_ubkxcf.jpg' },
  images: [
    {
      imgId: { type: String, default: '' },
      imgVersion: { type: String, default: '' },
    },
  ],
});

const User = mongoose.model<IUser>('User', UserSchema);
export default User;
