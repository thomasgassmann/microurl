import { Component, OnInit } from '@angular/core';
import * as monaco from 'monaco-editor';

@Component({
  selector: 'app-diff',
  templateUrl: './diff.component.html',
  styleUrls: ['./diff.component.scss']
})
export class DiffComponent implements OnInit {


  constructor() { }

  ngOnInit() {
  }

  public editorCreator(domElement: HTMLElement): monaco.editor.IEditor {
    const editor = monaco.editor.createDiffEditor(domElement, {
      theme: 'vs-dark',
      originalEditable: true,
    });
    editor.setModel({
      original: monaco.editor.createModel('', 'javascript'),
      modified: monaco.editor.createModel('', 'javascript')
    });
    return editor;
  }

}
