import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ShortenUrlComponent, EditorComponent, StatsComponent } from './components';

const routes: Routes = [
  {
    path: '',
    component: ShortenUrlComponent
  },
  {
    path: 'stats',
    component: StatsComponent
  },
  {
    path: 'editor',
    component: EditorComponent
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
