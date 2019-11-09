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
  MatProgressBarModule,
  MatSelectModule
} from '@angular/material';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
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
import { MicroUrlSharedModule } from './shared/micro-url-shared';

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
    MicroUrlSharedModule,

    MatProgressBarModule,
    NgxChartsModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatGridListModule,
    MatListModule,
    MatSidenavModule,
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
    MatSelectModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production
    })
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
