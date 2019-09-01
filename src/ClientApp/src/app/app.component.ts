import { Component } from '@angular/core';
import { Router, NavigationEnd, Event, NavigationStart, NavigationCancel, NavigationError, RouterOutlet } from '@angular/router';
import { NavigationEntry } from './models';
import { fadeAnimation } from './animations';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  animations: [fadeAnimation]
})
export class AppComponent {

  public navigationEntries: NavigationEntry[] = [
    {
      icon: 'link',
      name: 'Link',
      route: '/',
      matchExact: true
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

  public loading = false;

  constructor(router: Router) {
    router.events.subscribe((event: Event) => {
      switch (true) {
        case event instanceof NavigationStart:
          this.loading = true;
          break;
        case event instanceof NavigationEnd:
        case event instanceof NavigationCancel:
        case event instanceof NavigationError:
          this.loading = false;
          break;
        default:
          break;
      }

      if (event instanceof NavigationEnd) {
        ga('set', 'page', event.urlAfterRedirects);
        ga('send', 'pageview');
      }
    });
  }

  public prepareRoute(outlet: RouterOutlet): any {
    return outlet.activatedRoute;
  }
}
