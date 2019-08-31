import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import {
  MatButtonModule,
  MatToolbarModule,
  MatCardModule,
  MatFormFieldModule,
  MatInputModule,
  MatIconModule,
  MatSidenavModule,
  MatListModule,
  MatProgressSpinnerModule,
  MatGridListModule,
  MatSnackBarModule,
  MatProgressBarModule
} from '@angular/material';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MonacoEditorModule } from 'ngx-monaco-editor';
import {
  ShortenUrlComponent,
  StatsComponent,
  EditorComponent,
  DiffComponent,
  ToolbarLogoComponent,
  UrlResultComponent,
  TitledCardComponent,
  StatsInfoComponent
} from './components';
import { RouteReuseStrategy } from '@angular/router';
import { CacheRouteReuseStrategy } from './common';

@NgModule({
  declarations: [
    AppComponent,
    ShortenUrlComponent,
    StatsComponent,
    EditorComponent,
    DiffComponent,
    ToolbarLogoComponent,
    UrlResultComponent,
    TitledCardComponent,
    StatsInfoComponent
  ],
  imports: [
    MatProgressBarModule,
    NgxChartsModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatGridListModule,
    MatListModule,
    MatSidenavModule,
    MonacoEditorModule.forRoot(),
    MatIconModule,
    HttpClientModule,
    FormsModule,
    MatInputModule,
    CommonModule,
    MatFormFieldModule,
    MatCardModule,
    MatButtonModule,
    BrowserAnimationsModule,
    BrowserModule,
    MatToolbarModule,
    AppRoutingModule,
    ReactiveFormsModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production
    })
  ],
  providers: [
    {
      provide: RouteReuseStrategy,
      useClass: CacheRouteReuseStrategy
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
