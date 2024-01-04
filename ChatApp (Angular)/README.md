# MWD22-23 Project - Real-Time Chat Application

## Introduction

This project was created in the scope of the `Modern Web Development` module at Kiel University of Applied Sciences.
Main motivation was to explore real-time communication over websockets, angular for frontend development and some monorepo tooling (turbo).

## Screenshots 
<img width="400" alt="Screenshot 2024-01-04 at 20 08 42" src="https://github.com/tubspaulkeller/University-Projects/assets/102319452/bf48e15c-2337-47d6-8b1b-4e952006b88d">

Landingpage <br>
<img width="600" alt="Screenshot 2024-01-04 at 20 08 36" src="https://github.com/tubspaulkeller/University-Projects/assets/102319452/db488b8c-8175-400e-9a02-053f2039cd2c">

Group <br>
<img width="400" alt="Screenshot 2024-01-04 at 20 08 46" src="https://github.com/tubspaulkeller/University-Projects/assets/102319452/a8658c71-e506-4b27-bff1-bf6aa7f36a4a">
<img width="600" alt="Screenshot 2024-01-04 at 20 09 07" src="https://github.com/tubspaulkeller/University-Projects/assets/102319452/7b8f4916-383a-4493-8f73-3fbf04318627">

Private Chat <br>
<img width="600" alt="Screenshot 2024-01-04 at 20 09 13" src="https://github.com/tubspaulkeller/University-Projects/assets/102319452/fb7b4ffa-fd66-4507-b35c-c87e462f20df">




## Tech-Stack
### Frontend:
- Angular 
- TypeScript
- Angular Material
- RxJS (in particular RxJS WebSockets)

### Backend:
- NodeJS 
- Typescript
- Express
- Mongoose

### Tooling:
- [Turborepo](https://turbo.build/repo) 
- Yarn Workspaces (for shared packages/configurations)

## Using this project

Clone this repository and run the following commands:

```shell
cd MDW22-23
yarn install
```

### Set up environment variables

Create `.env` in `packages/server/`.

The variables to set can be found in github secrets & our discord server.

Variables:

```
PORT=<PORT>
MONGO_URL:<MONGO_URL>
CLOUDINARY_URL:<CLOUDINARY_URL>
API_URL:<API_URL>
SECRET:<SECRET>
CLOUDINARY_NAME:<CLOUDINARY_NAME>
CLOUDINARY_API_KEY:<CLOUDINARY_API_KEY>
CLOUDINARY_API_SECRET: <CLOUDINARY_API_SECRET>
```

### Build

To build all apps and packages, run the following command:

```shell
yarn build
```

To build individual packages run:

```shell
yarn <workspace> build
e.g. yarn client build
```
The full project needs to be build at least once, if you want to run `dev` command.
This is due to `shared` package needing to be transpiled into javascript.

### Develop

To develop all apps and packages, run the following command:

```shell
yarn dev
```

To develop individual packages run:

```shell
yarn <workspace> dev
e.g. yarn server dev
```

**Installing dependencies should only be done from the root folder of this project!**

To add new dependencies to a specific package run:

```shell
yarn <workspace> add (--flags) <package>
e.g. yarn server add lodash
```

More information can be found in the [turborepo documentaion](https://turborepo.org/docs/core-concepts/running-tasks).
