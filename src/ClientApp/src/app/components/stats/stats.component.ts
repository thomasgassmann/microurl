import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent {

  public statsForm: FormGroup;

  constructor(formBuilder: FormBuilder) {
    this.statsForm = formBuilder.group({
      key: new FormControl('', [Validators.required, Validators.pattern('[a-z0-9]{1,}')])
    });
  }

  public loadStats(): void {
    const key = this.statsForm.controls['key'].value;
  }

}
