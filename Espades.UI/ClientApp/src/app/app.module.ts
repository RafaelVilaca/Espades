import { CUSTOM_ELEMENTS_SCHEMA, NgModule, LOCALE_ID } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_CONST_COMPONENTS } from './app.const.component';
import { HttpClientModule } from '@angular/common/http';
import { NgSpinKitModule } from 'ng-spin-kit';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { NgxMaskModule } from 'ngx-mask'
import { APP_CONST_ENTRY_COMPONENTS } from './app.entry.component';
import { APP_CONST_PRIMENG_COMPONENTS } from './app.primeng.entry.component';
import { APP_CONST_PROVIDERS } from './app.const.providers';

registerLocaleData(localePt);

@NgModule({
  declarations: [
    AppComponent,
    APP_CONST_COMPONENTS
  ],
  entryComponents: [
    APP_CONST_ENTRY_COMPONENTS
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    NgSpinKitModule,
    NgxMaskModule.forRoot(),
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    APP_CONST_PRIMENG_COMPONENTS
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  providers: [APP_CONST_PROVIDERS,
    { provide: LOCALE_ID, useValue: 'pt-BR' }],
  bootstrap: [AppComponent]
})
export class AppModule { }
