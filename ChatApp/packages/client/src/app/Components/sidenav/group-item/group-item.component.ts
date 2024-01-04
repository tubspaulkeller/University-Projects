import { Component, Input } from '@angular/core';
import { IGroup } from '../../../Interfaces/IGroup';

@Component({
  selector: 'app-group-item',
  templateUrl: './group-item.component.html',
  styleUrls: ['./group-item.component.scss'],
})
export class GroupItemComponent {
  @Input() group?: IGroup;
  @Input() isExpanded = false;

  hasGroupImage(): boolean {
    return this.group?.imgSrc !== '';
  }
}
