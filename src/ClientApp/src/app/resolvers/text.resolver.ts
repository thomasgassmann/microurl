import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { ApiError, Text } from '../services/models';
import { Observable, EMPTY } from 'rxjs';
import { SnackbarService, TextService } from '../services';

@Injectable({
  providedIn: 'root'
})
export class TextResolver implements Resolve<Text | null> {
  constructor(
    private textService: TextService,
    private router: Router,
    private snackbarService: SnackbarService) {
  }

  public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    (Text | null) | Observable<Text | null> | Promise<Text | null> {
    const key = route.paramMap.get('key');
    if (!key) {
      this.router.navigate(['/editor']);
      return EMPTY;
    }

    return this.textService.get(key).catch((error: ApiError) => {
      this.snackbarService.show(error.message);
      this.router.navigate(['/editor']);
      return null;
    });
  }

}
