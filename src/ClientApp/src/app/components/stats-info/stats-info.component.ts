import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Data } from '@angular/router';
import { Stats, VisitorRanking } from 'src/app/services/models';
import { MultiSeries, DataItem } from '@swimlane/ngx-charts';
import { format } from 'date-fns/fp';

@Component({
  selector: 'app-stats-info',
  templateUrl: './stats-info.component.html',
  styleUrls: ['./stats-info.component.scss']
})
export class StatsInfoComponent implements OnInit {

  public stats: Stats | undefined = undefined;
  public data: MultiSeries | undefined = undefined;

  constructor(private route: ActivatedRoute) { }

  public ngOnInit() {
    this.route.data.subscribe((data: Data) => {
      this.stats = data.stats;
      this.data = this.calculateChartData();
    });
  }

  private calculateChartData(): MultiSeries {
    return [
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
    if (!this.stats) {
      return [];
    }

    return this.stats.recents.map(x => {
      return {
        name: format('yyyy/MM/dd', x.from),
        value: propertySelector(x)
      } as DataItem;
    });
  }

}
