import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MonacoEditorComponent } from './components';


@NgModule({
  declarations: [
    MonacoEditorComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    MonacoEditorComponent
  ]
})
export class MicroUrlSharedModule { }
