import { Component, OnInit } from '@angular/core';
import * as monaco from 'monaco-editor';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss']
})
export class EditorComponent implements OnInit {


  constructor() { }

  public ngOnInit() {
  }

  public editorCreator(domElement: HTMLElement): monaco.editor.IEditor {
    return monaco.editor.create(domElement, {
      theme: 'vs-dark'
    });
  }

}
