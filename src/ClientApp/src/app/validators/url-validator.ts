import { ValidationErrors, AbstractControl } from '@angular/forms';
import isUrl from 'is-url-superb';

const KEY = 'url';

export function urlValidator(control: AbstractControl): ValidationErrors | null {
  const currentValue = control.value as string;
  if (!isUrl(currentValue)) {
    return {
      [KEY]: 'Invalid url'
    };
  }

  return null;
}
