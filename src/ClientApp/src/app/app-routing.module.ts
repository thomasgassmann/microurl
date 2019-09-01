import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {
  ShortenUrlComponent,
  EditorComponent,
  StatsComponent,
  DiffComponent,
  StatsInfoComponent
} from './components';
import { StatsInfoResolver } from './resolvers';

const routes: Routes = [
  {
    path: '',
    component: ShortenUrlComponent
  },
  {
    path: 'stats',
    component: StatsComponent,
    children: [
      {
        path: ':key',
        component: StatsInfoComponent,
        resolve: {
          stats: StatsInfoResolver
        }
      }
    ]
  },
  {
    path: 'editor',
    component: EditorComponent
  },
  {
    path: 'diff',
    component: DiffComponent
  },
  {
    path: '**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
