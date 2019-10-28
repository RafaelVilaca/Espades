import { Injectable } from "@angular/core";
import { RequestResult } from '../_interfaces/request-result.interface';
import { CustomHttpClient } from "./custom-http-client";

@Injectable()
export class BaseService {
  constructor(protected httpClient: CustomHttpClient,
    private baseUrl: string,
    private baseResource: string = "") {
  }

  public save(data: any): Promise<RequestResult> {
    if (!data.id) {
      return this.httpClient.post(this.baseUrl + this.baseResource, data)
        .then(response => {
          return response;
        });
    }
    else {
        console.log(this.baseUrl);
      return this.httpClient.put(this.baseUrl + this.baseResource, data)
        .then(response => {
          return response;
        });
    }
  }

  public delete(id: any): Promise<RequestResult> {
    return this.httpClient.delete(`${this.baseUrl}${this.baseResource}/${id}`)
      .then(response => {
        return response;
      });
  }

  public get(id: any): Promise<RequestResult> {
    return this.httpClient.get(`${this.baseUrl}${this.baseResource}/${id}`)
      .then(response => {
        return response;
      });
  }

  public getAll(): Promise<RequestResult> {
    return this.httpClient.get(this.baseUrl + this.baseResource)
      .then(response => {
        return response;
      });
  }
}
