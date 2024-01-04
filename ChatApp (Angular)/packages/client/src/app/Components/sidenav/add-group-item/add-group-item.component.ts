import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddGroupDialogComponent } from '../add-group-dialog/add-group-dialog.component';
import { DataService } from '../../../Services/data.service';
import { TokenService } from '../../../Services/token.service';
import { SocketEvent } from '../../../Lib/SocketEvent';

@Component({
  selector: 'app-add-group-item',
  templateUrl: './add-group-item.component.html',
  styleUrls: ['./add-group-item.component.scss'],
})
export class AddGroupItemComponent implements OnInit {
  constructor(
    public dialog: MatDialog,
    private dataService: DataService,
    private tokenService: TokenService
  ) {}

  @Input() isExpanded = false;

  private currentUserId = '';

  ngOnInit() {
    this.currentUserId = this.tokenService.GetPayload()._id;
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(AddGroupDialogComponent, {
      width: '50%',
      data: { name: '', file: null },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (!result) return;
      if (result !== '' || result !== undefined) {
        this.addGroup(result);
      }
    });
  }

  addGroup(result: any): void {
    this.dataService.sendMessage({
      type: SocketEvent.Group.ADD_GROUP,
      groupName: result.name,
      sender: this.currentUserId,
      receiver: '',
      message: '',
      gif: '',
      file: result.file,
    });
  }
}
