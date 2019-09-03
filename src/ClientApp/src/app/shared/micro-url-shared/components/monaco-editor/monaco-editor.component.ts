import { Component, OnInit, ViewChild, ElementRef, AfterViewInit, OnDestroy } from '@angular/core';
import * as monaco from 'monaco-editor';
import { Subscription, fromEvent } from 'rxjs';

@Component({
  selector: 'app-monaco-editor',
  templateUrl: './monaco-editor.component.html',
  styleUrls: ['./monaco-editor.component.scss']
})
export class MonacoEditorComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('editorContainer', { static: true }) private _editorContainer: ElementRef | null = null;

  private _editor: monaco.editor.IStandaloneCodeEditor | null = null;
  private _windowResizeSubscription: Subscription | null = null;

  public ngOnInit() {
    (<any>window).require.config({ paths: { 'vs': '/assets/monaco/vs' } });
    (<any>window).require(['vs/editor/editor.main'], () => {
      this.initializeMonaco();
    });
  }

  public ngAfterViewInit(): void {
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
    if (!this._editorContainer || !this._editorContainer.nativeElement) {
      throw new Error('nativeElement not defined');
    }

    this._editor = monaco.editor.create(this._editorContainer.nativeElement, {
    });

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
