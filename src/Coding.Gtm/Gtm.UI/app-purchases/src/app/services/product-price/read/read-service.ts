import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { ReadRequest } from "./models/request/read-request";
import { ReadResponse } from "./models/response/read-response";

@Injectable({
    providedIn: 'root'
  })
  export class ReadService {
    constructor(private http: HttpClient) {
      
    }
  
    read(request: ReadRequest): Observable<ReadResponse> {
      const options = {
        headers: null,
        params: {}
      };
      
      return this.http.get<ReadResponse>(environment.apiUrl + 'products/prices/1.0?ProductId=' + request.productId, options);
    }
  
  }