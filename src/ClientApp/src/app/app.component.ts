import { Component } from '@angular/core';
import { Router, RouterEvent, NavigationEnd, Event } from '@angular/router';
import { NavigationEntry } from './models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  public navigationEntries: NavigationEntry[] = [
    {
      icon: 'link',
      name: 'Link',
      route: '/'
    },
    {
      icon: 'code',
      name: 'Editor',
      route: '/editor'
    },
    {
      icon: 'compare',
      name: 'Diff',
      route: '/diff'
    },
    {
      icon: 'trending_up',
      name: 'Stats',
      route: '/stats'
    }
  ];

  constructor(router: Router) {
    router.events.subscribe((event: Event) => {
      if (event instanceof NavigationEnd) {
        ga('set', 'page', event.urlAfterRedirects);
        ga('send', 'pageview');
      }
    });
  }
}
