import { Component } from '@angular/core';
import { Router, RouterEvent, NavigationEnd } from '@angular/router';

interface NavigationOption {
  route: string;
  name: string;
  icon: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  public navigationEntries: NavigationOption[] = [
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
    router.events.subscribe((event: RouterEvent) => {
      if (event instanceof NavigationEnd) {
        ga('set', 'page', event.urlAfterRedirects);
        ga('send', 'pageview');
      }
    });
  }
}
