import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { ReadAllResponse } from "./models/response/read-all-response";

@Injectable({
    providedIn: 'root'
  })
  export class ReadAllService {
    constructor(private http: HttpClient) {
      
    }
  
    readAll(): Observable<ReadAllResponse> {
      const options = {
        headers: null,
        params: {}
      };
      
      return this.http.get<ReadAllResponse>(environment.apiUrl + 'products/1.0', options);
    }
  
  }