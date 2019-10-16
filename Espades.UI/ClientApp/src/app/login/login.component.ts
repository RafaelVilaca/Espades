import { Component, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginService } from './login.service';
import { ToastService } from '../toast-component/toast.service';
import { Status } from '../_enums/status.enum';
import { colors } from '../_enums/colors-profile.enum';
import { SESSION_CONFIG, ISessionConfig } from '../session.config';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  constructor(private router: Router,
    private route: ActivatedRoute,
    private loginService: LoginService,
    private toastService: ToastService,
    @Inject(SESSION_CONFIG) public sessionConfig: ISessionConfig) { }

  entity: any = {};
  returnUrl: string = null;
  colorDefaultProfile: string = "";
  colorProfile: string = "";

  ngOnInit() {
    setTimeout(() => {
      this.loginService.doLogout();
      this.sessionConfig.isAuth = false;
    }, 0);

    let colorNumber = Math.floor(Math.random() * (94 - 0) + 0);

    this.colorProfile = colors[colorNumber];
    this.colorDefaultProfile = localStorage.getItem("colorProfile");
    this.prepareReturnUrl();
  }

  prepareReturnUrl() {
    let url = this.route.snapshot.queryParams['returnUrl'] || '/home';

    if (url === '/login' || url === '/') {
      url = '/home';
    }

    if (url !== '') {
      this.returnUrl = url;
    }
  }

  authenticate() {
    //this.loginService.doLogin(this.entity).then(response => {
    //  this.toastService.show(response);
    //  if (response.status == Status.Success) {
    this.storeLoginData();//(response.data);
    this.router.navigateByUrl(this.returnUrl);
    //  }
    //});
  }

  storeLoginData(data?: any) {
    this.sessionConfig.isAuth = true;
    localStorage.setItem("loggedUser", "1");//data.userId);
    localStorage.setItem("userName", "Admin");//data.userName);
    if (this.colorDefaultProfile === "" || this.colorDefaultProfile === null) {
      localStorage.setItem("colorProfile", this.colorProfile);
    }
  }
}
