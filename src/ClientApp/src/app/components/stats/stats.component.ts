import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Toggler } from 'src/app/common';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent {

  public loading = false;
  public readonly statsForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router) {
    this.statsForm = this.formBuilder.group({
      key: new FormControl('', [Validators.required, Validators.pattern('[a-z0-9]{1,}')])
    });
  }

  @Toggler<StatsComponent>('loading')
  public async loadStats(): Promise<void> {
    const key = this.statsForm.controls['key'].value;
    await this.router.navigate(['/stats/' + key]);
    this.statsForm.markAsPristine();
  }

}
