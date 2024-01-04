import { NgModule } from '@angular/core';
import { MainComponent } from 'src/app/Components/main/main.component';
import { MainRoutingModule } from './main-routing.module';
import { ChatComponent } from 'src/app/Components/chat/chat.component';
import { GroupsComponent } from 'src/app/Components/groups/groups.component';
import { UsersComponent } from 'src/app/Components/users/users.component';
import { SharedModule } from '../shared/shared.module';
import { UserService } from 'src/app/Services/user.service';
import { HttpClientModule } from '@angular/common/http';
import { MessageComponent } from 'src/app/Components/message/message.component';
import { TokenService } from 'src/app/Services/token.service';
import { WebsocketsService } from 'src/app/Services/websockets.service';
import { PickerModule } from '@ctrl/ngx-emoji-mart';
import { TextMessageComponent } from 'src/app/Components/message/text-message/text-message.component';
import { AvatarComponent } from '../../Components/avatar/avatar.component';
import { AudioMessageComponent } from 'src/app/Components/message/audio-message/audio-message.component';
import { ImageMessageComponent } from 'src/app/Components/message/image-message/image-message.component';
import { UploadService } from 'src/app/Services/upload.service';
import { FileUploadModule } from 'ng2-file-upload';
import { ProfilePictureComponent } from 'src/app/Components/profile-picture/profile-picture.component';
import { ImageService } from 'src/app/Services/image.service';
import { DataService } from '../../Services/data.service';
import { GroupItemComponent } from '../../Components/sidenav/group-item/group-item.component';
import { GroupListComponent } from '../../Components/sidenav/group-list/group-list.component';
import { AddGroupDialogComponent } from '../../Components/sidenav/add-group-dialog/add-group-dialog.component';
import { AddGroupItemComponent } from '../../Components/sidenav/add-group-item/add-group-item.component';
import { MainRoomComponent } from 'src/app/Components/main-room/main-room.component';
import { PrivateRoomComponent } from 'src/app/Components/private-room/private-room.component';
import { NormalRoomComponent } from 'src/app/Components/normal-room/normal-room.component';
import { ChatContainerComponent } from '../../Components/chat-container/chat-container.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ChatInputComponent } from '../../Components/chat-container/chat-input/chat-input.component';
import { MessageContainerComponent } from '../../Components/chat-container/message-container/message-container.component';
import { GifSearchComponent } from '../../Components/chat-container/chat-input/gif-search/gif-search.component';
import { MatRippleModule } from '@angular/material/core';
import { RecordAudioComponent } from '../../Components/chat-container/chat-input/record-audio/record-audio.component';

@NgModule({
  declarations: [
    MainComponent,
    ChatComponent,
    GroupsComponent,
    UsersComponent,
    MessageComponent,
    TextMessageComponent,
    AvatarComponent,
    AudioMessageComponent,
    ImageMessageComponent,
    ProfilePictureComponent,
    GroupItemComponent,
    GroupListComponent,
    AddGroupDialogComponent,
    AddGroupItemComponent,
    MainRoomComponent,
    PrivateRoomComponent,
    NormalRoomComponent,
    ChatContainerComponent,
    ChatInputComponent,
    MessageContainerComponent,
    GifSearchComponent,
    RecordAudioComponent,
  ],
  imports: [
    MainRoutingModule,
    SharedModule,
    HttpClientModule,
    PickerModule,
    FileUploadModule,
    MatTooltipModule,
    MatRippleModule,
  ],
  providers: [
    UserService,
    TokenService,
    WebsocketsService,
    UploadService,
    ImageService,
    DataService,
  ],
})
export class MainModule {}
