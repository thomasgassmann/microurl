import * as monaco from 'monaco-editor';

export type EditorCreator = (element: HTMLElement) => monaco.editor.IEditor;
