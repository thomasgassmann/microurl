import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

  constructor(private matSnackbar: MatSnackBar) { }

  public show(message: string): void {
    this.matSnackbar.open(message, undefined, {
      verticalPosition: 'bottom',
      horizontalPosition: 'right',
      duration: 2000
    });
  }
}
