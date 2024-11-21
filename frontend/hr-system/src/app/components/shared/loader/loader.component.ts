import { Component } from '@angular/core';
import { LoadingService } from '../../../core/services/loading.service';

@Component({
  selector: 'app-loader',
  template: `
    <div class="loader-container" *ngIf="loading$ | async">
      <mat-spinner></mat-spinner>
    </div>
  `,
  styles: [
    `
      .loader-container {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        display: flex;
        align-items: center;
        justify-content: center;
        z-index: 1000;
      }
    `,
  ],
})
export class LoaderComponent {
  loading$ = this.loadingService.loading$;

  constructor(private loadingService: LoadingService) {}
}