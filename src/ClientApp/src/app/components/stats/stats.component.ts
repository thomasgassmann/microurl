import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Toggler } from 'src/app/common';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit {
  public loading = false;
  public readonly statsForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute) {
    this.statsForm = this.formBuilder.group({
      key: new FormControl('', [Validators.required, Validators.pattern('[a-z0-9]{1,}')])
    });
  }

  public ngOnInit(): void {
    if (this.activatedRoute.firstChild) {
      this.activatedRoute.firstChild.paramMap.subscribe((paramMap: ParamMap) => {
        this.statsForm.controls['key'].setValue(paramMap.get('key'));
      });
    }
  }

  @Toggler<StatsComponent>('loading')
  public async loadStats(): Promise<void> {
    const key = this.statsForm.controls['key'].value;
    await this.router.navigate(['/stats/' + key]);
    this.statsForm.markAsPristine();
  }

}
