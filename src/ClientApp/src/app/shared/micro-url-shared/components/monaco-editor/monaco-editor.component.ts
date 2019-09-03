import { Component, OnInit, ViewChild, ElementRef, OnDestroy, Input } from '@angular/core';
import * as monaco from 'monaco-editor';
import { Subscription, fromEvent } from 'rxjs';
import { EditorCreator } from './editor-creator';

@Component({
  selector: 'app-monaco-editor',
  templateUrl: './monaco-editor.component.html',
  styleUrls: ['./monaco-editor.component.scss']
})
export class MonacoEditorComponent implements OnInit, OnDestroy {
  @ViewChild('editorContainer', { static: true }) private _editorContainer: ElementRef | null = null;

  private _editor: monaco.editor.IEditor | null = null;
  private _windowResizeSubscription: Subscription | null = null;

  @Input() public creator: EditorCreator | null = null;

  public ngOnInit() {
    (<any>window).require.config({ paths: { 'vs': '/assets/monaco/vs' } });
    (<any>window).require(['vs/editor/editor.main'], () => {
      this.initializeMonaco();
    });
  }

  public ngOnDestroy(): void {
    if (this._windowResizeSubscription) {
      this._windowResizeSubscription.unsubscribe();
    }

    if (this._editor) {
      this._editor.dispose();
      this._editor = null;
    }
  }

  private initializeMonaco(): void {
    if (!this._editorContainer || !this._editorContainer.nativeElement || !this.creator) {
      throw new Error('Not properly initialized');
    }

    this._editor = this.creator(this._editorContainer.nativeElement);

    if (this._windowResizeSubscription) {
      this._windowResizeSubscription.unsubscribe();
    }
    this._windowResizeSubscription = fromEvent(window, 'resize')
      .subscribe(() => {
        if (this._editor) {
          this._editor.layout();
        }
      });
  }

}
