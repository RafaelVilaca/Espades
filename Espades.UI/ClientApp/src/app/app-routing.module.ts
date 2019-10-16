import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { APP_CONST_ROUTES } from './app.const.routes';

@NgModule({
  imports: [RouterModule.forRoot(APP_CONST_ROUTES)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
