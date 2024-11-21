import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoadingService {
  private loading = new BehaviorSubject<boolean>(false);
  public loading$ = this.loading.asObservable();
  private activeRequests = 0;

  startLoading() {
    this.activeRequests++;
    this.loading.next(true);
  }

  stopLoading() {
    this.activeRequests--;
    if (this.activeRequests === 0) {
      this.loading.next(false);
    }
  }
}
