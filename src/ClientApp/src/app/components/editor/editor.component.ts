import { Component, OnInit } from '@angular/core';
import * as monaco from 'monaco-editor';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss']
})
export class EditorComponent implements OnInit {

  private currentEditor: monaco.editor.IEditor | null = null;

  public selectedLanguage = 'markdown';

  public languages = [
    'markdown',
    'javascript'
  ];

  constructor() { }

  public ngOnInit() {
  }

  public editorCreator = (domElement: HTMLElement): monaco.editor.IEditor => {
    const editor = monaco.editor.create(domElement, {
      theme: 'vs-dark'
    });
    editor.setModel(monaco.editor.createModel('', this.selectedLanguage));
    this.currentEditor = editor;
    return editor;
  }

  public languageChanged(): void {
    if (this.currentEditor) {
      const model = this.currentEditor.getModel() as monaco.editor.ITextModel;
      monaco.editor.setModelLanguage(model, this.selectedLanguage);
    }
  }

}
