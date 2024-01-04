# MWD22-23 Project - Real-Time Chat Application

## Introduction

This project was created in the scope of the `Modern Web Development` module at Kiel University of Applied Sciences.
Main motivation was to explore real-time communication over websockets, angular for frontend development and some monorepo tooling (turbo).


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
