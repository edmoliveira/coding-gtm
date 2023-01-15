import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { SignInRequest } from "./models/request/sign-in-request";
import { SignInResponse } from "./models/response/sign-in-response";

@Injectable({
    providedIn: 'root'
  })
  export class SignInService {
    constructor(private http: HttpClient) {
      
    }
  
    signIn(request: SignInRequest): Observable<SignInResponse> {
      const options = {
        headers: null,
        params: {}
      };
      
      return this.http.post<SignInResponse>(environment.apiUrl + 'users/sign-in/1.0', request, options);
    }
  
  }