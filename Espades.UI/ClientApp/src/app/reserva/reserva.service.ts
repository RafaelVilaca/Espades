import { Injectable } from "@angular/core";
import { BaseService } from '../_common/base.service';
import { environment } from '../../environments/environment';
import { CustomHttpClient } from "../_common/custom-http-client";

@Injectable()
export class ReservaService extends BaseService {
  constructor(httpClient: CustomHttpClient) {
    super(httpClient, environment.authUrl, "reserva");
  }

  //public getFinanceChart(): Promise<RequestResult> {
  //  return this.httpClient.get(`${environment.kpiUrl}finance/finances`, false)
  //    .then(response => {
  //      return response;
  //    });
  //}
}
