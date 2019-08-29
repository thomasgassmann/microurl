import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss']
})
export class EditorComponent implements OnInit {

  public editorOptions = {
    theme: 'vs-dark',
    language: 'csharp'
  };
  public code = '';

  constructor() { }

  ngOnInit() {
  }

}
