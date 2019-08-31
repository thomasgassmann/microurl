import { Component, AfterViewInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { StatsService, SnackbarService } from 'src/app/services';
import { Stats } from 'src/app/services/models';
import { MultiSeries, DataItem } from '@swimlane/ngx-charts';
import { Router } from '@angular/router';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements AfterViewInit {

  public readonly statsForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router) {
    this.statsForm = this.formBuilder.group({
      key: new FormControl('', [Validators.required, Validators.pattern('[a-z0-9]{1,}')])
    });
  }

  ngAfterViewInit(): void {
    console.log('test');
  }

  public loadStats(): void {
    const key = this.statsForm.controls['key'].value;
    this.router.navigate(['/stats/' + key]);
  }

}
