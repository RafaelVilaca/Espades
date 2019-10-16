import { Component, ViewContainerRef, Inject, OnInit } from '@angular/core';
import { ToastService } from './toast-component/toast.service';
import { ISessionConfig, SESSION_CONFIG } from './session.config';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(public viewRef: ViewContainerRef,
    private toastService: ToastService,
    @Inject(SESSION_CONFIG) public sessionConfig: ISessionConfig) { }

  title = 'ESPADES';

  ngOnInit() {
    this.toastService.initialize(this.viewRef);
  }
}
