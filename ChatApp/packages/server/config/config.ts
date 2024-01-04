import * as dotenv from 'dotenv';

dotenv.config();

export const config = {
  db_url: process.env.MONGO_URL || 'ERROR_MONGO_URL',
  port: process.env.PORT || '3000',
  secret: process.env.SECRET || 'ERROR_SECRET',
  cloudinary: process.env.CLOUDINARY_URL || 'ERROR_CLOUDINARY_URL',
  api: process.env.API_URL || 'ERROR_API_URL',
  cloudinary_name: process.env.CLOUDINARY_NAME || 'ERROR_CLOUDINARY_NAME',
  cloudinary_api_key: process.env.CLOUDINARY_API_KEY || 'ERROR_CLOUDINARY_API_KEY',
  cloudinary_api_secret: process.env.CLOUDINARY_API_SECRET || 'ERROR_CLOUDINARY_API_SECRET',
};
