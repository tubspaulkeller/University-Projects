import { NgModule } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSliderModule } from '@angular/material/slider';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { DrawerRailModule } from 'angular-material-rail-drawer';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';

const modules = [
  MatCardModule,
  MatInputModule,
  MatButtonModule,
  MatSliderModule,
  FlexLayoutModule,
  MatMenuModule,
  MatToolbarModule,
  MatSelectModule,
  MatSidenavModule,
  MatIconModule,
  MatListModule,
  DrawerRailModule,
  MatGridListModule,
  MatFormFieldModule,
  MatDialogModule,
];

@NgModule({
  imports: modules,
  exports: modules,
})
export class MaterialModule {}
