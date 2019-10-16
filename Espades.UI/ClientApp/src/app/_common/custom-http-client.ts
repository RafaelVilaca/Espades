import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ISessionConfig, SESSION_CONFIG } from '../session.config';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ToastService } from '../toast-component/toast.service';
import { Status } from '../_enums/status.enum';

@Injectable()
export class CustomHttpClient {

  constructor(private http: HttpClient,
    @Inject(SESSION_CONFIG) private sessionConfig: ISessionConfig,
    private toastService: ToastService,
    private router: Router) { }

  createAuthorizationHeader(contentTypeFormUrl: boolean = false): HttpHeaders {
    let language: any = localStorage.getItem("app-language");
    let token: any = localStorage.getItem("token");

    return new HttpHeaders(
      {
        'Content-Type': contentTypeFormUrl ? 'application/x-www-form-urlencoded' : 'application/json',
        'Accept': 'application/json',
        'Accept-Language': language || 'pt-Br',
        'Authorization': token ? 'Bearer ' + token : '',
      }
    );
  }

  get(url: any, showLoading: boolean = true, contentTypeFormUrl: boolean = false) {
    return this.ManageLoading(this.http.get(url, {
      headers: this.createAuthorizationHeader(contentTypeFormUrl)
    }), showLoading);
  }

  post(url: any, data: any, showLoading = true, contentTypeFormUrl: boolean = false) {
    return this.ManageLoading(this.http.post(url, data, {
      headers: this.createAuthorizationHeader(contentTypeFormUrl)
    }), showLoading);
  }

  put(url: any, data: any, showLoading: boolean = true) {
    return this.ManageLoading(this.http.put(url, data, {
      headers: this.createAuthorizationHeader()
    }), showLoading);
  }

  delete(url: any, showLoading: boolean = true) {
    return this.ManageLoading(this.http.delete(url, {
      headers: this.createAuthorizationHeader()
    }), showLoading);
  }

  handleError(error: any): Promise<any> {
    if (error.status == 401) {
      this.router.navigate(['/login'], { replaceUrl: true });
    }

    //Unauthorized
    if (error.status == 400 && error.error.status == -2) {
      this.toastService.show({
        status: Status.Warning,
        messages: ['Você não possui as permissões necessárias para realizar essa ação']
      });
    }

    return Promise.reject(error.message || error);
  }

  private ManageLoading(request: any, showLoading: any): Promise<any> {
    this.AddLoading(showLoading);

    return request
      .toPromise()
      .then((result: any) => {
        this.RemoveLoading(showLoading);
        return result;
      })
      .catch((error: any) => {
        this.RemoveLoading(showLoading);
        this.handleError(error);
        return error;
      });
  }

  private AddLoading(showLoading: boolean) {
    if (showLoading) {
      this.sessionConfig.isRequesting.push(true);
    }
  }

  private RemoveLoading(showLoading: boolean) {
    if (showLoading) {
      this.sessionConfig.isRequesting.pop();
    }
  }
}
