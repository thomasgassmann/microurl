import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Stats, ApiError } from '../services/models';
import { Observable, EMPTY } from 'rxjs';
import { StatsService, SnackbarService } from '../services';

@Injectable({
  providedIn: 'root'
})
export class StatsInfoResolver implements Resolve<Stats | null> {
  constructor(
    private statsService: StatsService,
    private router: Router,
    private snackbarService: SnackbarService) {
  }

  public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    (Stats | null) | Observable<Stats | null> | Promise<Stats | null> {
    const key = route.paramMap.get('key');
    if (!key) {
      this.router.navigate(['/stats']);
      return EMPTY;
    }

    return this.statsService.getStats(key).catch((error: ApiError) => {
      this.snackbarService.show(error.message);
      this.router.navigate(['/stats']);
      return null;
    });
  }

}
