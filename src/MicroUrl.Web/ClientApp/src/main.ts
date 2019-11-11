import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import { MicroUrlSettings } from './environments';

if (environment.production) {
  enableProdMode();
}

// initialize google analytics async for now
fetch('/settings').then(async (response: Response) => {
  const settings: MicroUrlSettings = await response.json();
  ga('create', settings.analyticsId, 'auto');
});

platformBrowserDynamic()
  .bootstrapModule(AppModule)
  .catch(err => console.error(err));
