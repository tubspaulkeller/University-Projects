import { Component, Input } from '@angular/core';
import { IGroup } from '../../../Interfaces/IGroup';

@Component({
  selector: 'app-group-list',
  templateUrl: './group-list.component.html',
  styleUrls: ['./group-list.component.scss'],
})
export class GroupListComponent {
  @Input() groups?: IGroup[] = [];
  @Input() isExpanded = false;

  addGroup(): void {
    console.log('addGroup');
  }
}
