import { Injectable } from "@angular/core";
import { environment } from '../../environments/environment';
import { CustomHttpClient } from '../_common/custom-http-client';
import { RequestResult } from '../_interfaces/request-result.interface';

@Injectable()
export class LoginService {
  constructor(private httpClient: CustomHttpClient) {
  }

  public doLogin(data: any): Promise<RequestResult> {
    return this.httpClient.post(environment.authUrl + "TokenAuth", data)
      .then(response => {
        return response;
      });
  }

  public doLogout() {
    localStorage.removeItem("loggedUser");
    localStorage.removeItem("userName");
  }
}
