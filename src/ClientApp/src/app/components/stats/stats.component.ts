import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Toggler } from 'src/app/common';
import { StatsKeyStoreService } from './services';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent implements OnInit, AfterViewInit, OnDestroy {
  public loading = false;
  public readonly statsForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private statsKeyStoreService: StatsKeyStoreService) {
    this.statsForm = this.formBuilder.group({
      key: new FormControl('', [Validators.required, Validators.pattern('[a-z0-9]{1,}')])
    });
  }

  public ngOnInit(): void {
    if (this.activatedRoute.firstChild) {
      this.activatedRoute.firstChild.paramMap.subscribe((paramMap: ParamMap) => {
        this.setKeyTextValue(paramMap.get('key') as string);
      });
    }
  }

  public ngAfterViewInit(): void {
    setTimeout(async () => {
      const lastKey = this.statsKeyStoreService.getLastKey();
      if (lastKey !== null) {
        this.setKeyTextValue(lastKey);
        await this.navigateToKey(lastKey);
      }
    });
  }

  public ngOnDestroy(): void {
    this.statsKeyStoreService.storeLastKey(this.getKeyTextValue());
  }

  @Toggler<StatsComponent>('loading')
  public async loadStats(): Promise<void> {
    await this.navigateToKey(this.getKeyTextValue());
    this.statsForm.markAsPristine();
  }

  public async navigateToKey(key: string): Promise<void> {
    await this.router.navigate(['/stats/' + key]);
  }

  private getKeyTextValue(): string {
    const key = this.statsForm.controls['key'].value;
    return key;
  }

  private setKeyTextValue(key: string): void {
    this.statsForm.controls['key'].setValue(key);
  }

}
