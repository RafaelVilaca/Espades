import { Injectable } from "@angular/core";
import { BaseService } from '../_common/base.service';
import { environment } from '../../environments/environment';
import { CustomHttpClient } from "../_common/custom-http-client";

@Injectable()
export class ProdutoService extends BaseService {
  constructor(httpClient: CustomHttpClient) {
    super(httpClient, environment.authUrl, "produto");
  }
}
