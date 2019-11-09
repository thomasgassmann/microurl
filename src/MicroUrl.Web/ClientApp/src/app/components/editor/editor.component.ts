import { Component, OnInit } from '@angular/core';
import * as monaco from 'monaco-editor';
import { MONACO_LANGUAGES } from 'src/app/monaco-languages';
import { TextService } from 'src/app/services';
import { ActivatedRoute, Data } from '@angular/router';
import { Text } from 'src/app/services/models';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss']
})
export class EditorComponent implements OnInit {

  private currentEditor: monaco.editor.IEditor | null = null;

  public selectedLanguage = 'markdown';
  public initialContent = '';
  public languages: string[] = [];

  constructor(private textService: TextService, private route: ActivatedRoute) { }

  public ngOnInit() {
    this.languages = MONACO_LANGUAGES;
    this.route.data.subscribe((data: Data) => {
      const text: Text | undefined = data.text;
      if (!text) {
        return;
      }

      this.selectedLanguage = text.language;
      if (this.currentEditor) {
        const textModel = this.currentEditor.getModel() as monaco.editor.ITextModel;
        textModel.setValue(text.content);
      } else {
        this.initialContent = text.content;
      }
    });
  }

  public async save(): Promise<void> {
    if (!this.currentEditor) {
      return;
    }

    const textModel = this.currentEditor.getModel() as monaco.editor.ITextModel;
    const content = textModel.getValue();
    await this.textService.create(this.selectedLanguage, content);
  }

  public editorCreator = (domElement: HTMLElement): monaco.editor.IEditor => {
    const editor = monaco.editor.create(domElement, {
      theme: 'vs-dark'
    });
    editor.setModel(monaco.editor.createModel(this.initialContent, this.selectedLanguage));
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
