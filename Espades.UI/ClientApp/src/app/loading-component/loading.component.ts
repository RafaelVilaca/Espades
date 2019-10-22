import { Component, Inject } from '@angular/core';
import { ISessionConfig, SESSION_CONFIG } from '../session.config';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.scss']
})

export class LoadingComponent {
  constructor(
    @Inject(SESSION_CONFIG) public sessionConfig: ISessionConfig) { }
}
