import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotifyService {

  constructor(
    private snackBar: MatSnackBar
  ) { }
 config: MatSnackBarConfig = {
    duration:3000
  }
  message(msg, actions) {
    this.snackBar.open(msg, actions, this.config);
  }
}
