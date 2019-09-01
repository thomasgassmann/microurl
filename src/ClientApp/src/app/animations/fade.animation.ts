import { trigger, animate, transition, style, query, animateChild, group } from '@angular/animations';
import { fadeInContent } from '@angular/material';

export const fadeAnimation = trigger('fadeAnimation', [
  transition('* => *', [
    query(':self',
      [
        style({ opacity: 0 })
      ],
      { optional: true }
    ),

    query(':self',
      [
        style({ opacity: 0 }),
        animate('.4s', style({ opacity: 1 }))
      ],
      { optional: true }
    )
  ])
]);
