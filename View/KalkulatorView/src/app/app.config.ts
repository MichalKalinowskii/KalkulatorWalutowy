import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

export const appConfig: ApplicationConfig = {
  providers: [ 
    [provideRouter(routes)],
    provideHttpClient(), 
    provideAnimations(),
    {provide: MAT_DATE_LOCALE, useValue: 'pl-PL'},
    BrowserModule,
    FormsModule
  ]
};
