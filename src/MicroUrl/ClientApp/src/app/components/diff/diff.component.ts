import { Component, OnInit } from '@angular/core';
import { DiffEditorModel } from 'ngx-monaco-editor';

@Component({
  selector: 'app-diff',
  templateUrl: './diff.component.html',
  styleUrls: ['./diff.component.scss']
})
export class DiffComponent implements OnInit {

  public editorOptions = { theme: 'vs-dark', language: 'javascript' };
  public originalModel: DiffEditorModel = {
    language: 'javascript',
    code: 'function x() {\nconsole.log("Hello world!");\n}'
  };
  public modifiedModel: DiffEditorModel = {
    language: 'javascript',
    code: 'function x() {\nconsole.log("Hello world!");\n}'
  };

  constructor() { }

  ngOnInit() {
  }

}
