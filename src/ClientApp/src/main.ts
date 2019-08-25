import { enableProdMode, InjectionToken } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import { CONFIG_TOKEN, Config } from './environments';

import appSettings from '../../appsettings.json';

if (environment.production) {
  enableProdMode();
}

ga('create', (appSettings as Config).MicroUrlSettings.AnalyticsId, 'auto');

platformBrowserDynamic([{ provide: CONFIG_TOKEN, useValue: appSettings }])
  .bootstrapModule(AppModule)
  .catch(err => console.error(err));
