# Web-Socket Events

## Event-Model
```
{
  type: string,
  data: any
}
```

## Event-Types
### Group Events
#### Send by client
- `GROUP:GET_INIT_DATA` - Get initial data for a group
- `GROUP:GET_ALL_GROUPS` - Sent when a client wants to get the list of all groups.
- `GROUP:ADD_GROUP` - Sent when a client wants to add a group.
- `GROUP:DELETE_GROUP` - Sent when a client wants to delete a group. TBD
- `GROUP:JOIN_GROUP` - Sent when a client wants to join a group.
- `GROUP:LEAVE_GROUP` - Sent when a client wants to leave a group. TBD

#### Send by server
- `GROUP:USER_JOINED` - Sent when a user joins a group. TBD
- `GROUP:USER_LEFT` - Sent when a user leaves a group. TBD
- `GROUP:RETURN_ALL_GROUPS` - Sent when the server returns the list of all groups.
- `GROUP:GROUP_ADDED` - Sent when the server adds a group.
- `GROUP:GROUP_DELETED` - Sent when the server deletes a group. TBD
- `GROUP:RETURN_INIT_DATA` - Sent when the server returns the initial data for a group. Returns group members and messages.

### Chat Events
#### Send by client
- `CHAT:GET_INIT_MESSAGES` - Get initial messages for a group/chat, requires group id.
- `CHAT:START_TYPING` - Sent when a client is typing a message.
- `CHAT:STOP_TYPING` - Sent when a client stops typing a message.
- `CHAT:SEND_MESSAGE_TEXT` - Sent when a client wants to send a text message to a chat.
- `CHAT:SEND_MESSAGE_IMAGE` - Sent when a client wants to send an image message to a chat.
- `CHAT:SEND_MESSAGE_AUDIO` - Sent when a client wants to send an audio message to a chat.
- `CHAT:SEND_MESSAGE_GIF` - Sent when a client wants to send a gif message to a chat.

#### Send by server
- `CHAT:RETURN_INIT_MESSAGES` - Sent when the server returns the initial messages for a group/chat.
- `CHAT:USER_STARTED_TYPING` - Sent when a user starts typing a message.
- `CHAT:USER_STOPPED_TYPING` - Sent when a user stops typing a message.
- `CHAT:MESSAGE_RETURN_TEXT` - Sent when a user sends a text message to a chat.
- `CHAT:MESSAGE_RETURN_IMAGE` - Sent when a user sends an image message to a chat.
- `CHAT:MESSAGE_RETURN_AUDIO` - Sent when a user sends an audio message to a chat.
- `CHAT:MESSAGE_RETURN_GIF` - Sent when a user sends a gif message to a chat.

### User Events
#### Send by client
- `USER:REFRESH_PROFILE` - Sent when a client wants to refresh their profile.

#### Send by server
- `USER:PROFILE_REFRESHED` - Sent when the server refreshes the profile.


# V2

## Client Events

### Group Events
