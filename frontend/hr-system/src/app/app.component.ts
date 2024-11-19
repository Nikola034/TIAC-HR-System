import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Subject, takeUntil, tap } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, OnDestroy {
  showNavbar = true;
  private readonly destroy$ = new Subject<void>();
  constructor(private router: Router) {}

  ngOnInit(): void {
    this.router.events.pipe(takeUntil(this.destroy$),tap((event) =>{
      if (event instanceof NavigationEnd) {
        // List of routes that should not show the navbar
        const excludedRoutes = ['/', '/reset-password'];
        const parameterizedRouteRegex = /^\/passwordReset\/[^/]+$/; 
        const currentRoute = event.urlAfterRedirects;

        // Check if the route matches any of the excluded routes
        this.showNavbar =
          !excludedRoutes.includes(currentRoute) &&
          !parameterizedRouteRegex.test(currentRoute);
      }
    })).subscribe();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}
