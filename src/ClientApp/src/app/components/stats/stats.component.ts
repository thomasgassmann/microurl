import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { StatsService } from 'src/app/services';
import { Toggler } from 'src/app/common/toggler';
import { Stats, VisitorRanking } from 'src/app/services/models';
import { format } from 'date-fns/fp';
import { MultiSeries, DataItem } from '@swimlane/ngx-charts';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.scss']
})
export class StatsComponent {

  public loading = false;
  public stats: Stats | undefined = undefined;
  public data: MultiSeries | undefined = undefined;
  public statsForm: FormGroup;

  constructor(formBuilder: FormBuilder, private statsService: StatsService) {
    this.statsForm = formBuilder.group({
      key: new FormControl('', [Validators.required, Validators.pattern('[a-z0-9]{1,}')])
    });
  }

  @Toggler<StatsComponent>('loading')
  public async loadStats(): Promise<void> {
    const key = this.statsForm.controls['key'].value;
    const stats = await this.statsService.getStats(key);

    this.stats = stats;
    this.data = [
      {
        name: 'Total',
        series: this.getSeries(x => x.visitors)
      },
      {
        name: 'Unique',
        series: this.getSeries(x => x.uniqueVisitors)
      }
    ];
  }

  private getSeries(propertySelector: (ranking: VisitorRanking) => number): DataItem[] {
    return this.stats.recents.map(x => {
      return {
        name: format('yyyy/MM/dd', x.from),
        value: propertySelector(x)
      } as DataItem;
    });
  }

}
