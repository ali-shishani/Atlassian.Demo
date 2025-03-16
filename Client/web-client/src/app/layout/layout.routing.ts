import { Routes } from '@angular/router';

import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';

import { LayoutComponent } from './layout.component';
import { HomeComponent } from 'src/app/pages/home/home.component';
import { ViewRecordsComponent } from 'src/app/pages/view-records/view-records.component';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: 'home', component: HomeComponent },
      { path: 'viewrecords', component: ViewRecordsComponent, canActivate: [AuthorizeGuard] },
      { path: '**', redirectTo: 'home' },
    ],
  },
];

export const components = [LayoutComponent];
