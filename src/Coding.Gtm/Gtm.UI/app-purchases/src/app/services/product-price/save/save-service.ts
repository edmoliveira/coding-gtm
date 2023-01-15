import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { SaveRequest } from "./models/request/save-request";

@Injectable({
    providedIn: 'root'
  })
  export class SaveService {
    constructor(private http: HttpClient) {
      
    }
  
    save(request: SaveRequest): Observable<object> {
      const options = {
        headers: null,
        params: {}
      };
      
      return this.http.post(environment.apiUrl + 'products/prices/1.0', request, options);
    }
  
  }