import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthGuard } from '../helpers/auth-guard';
import { SignInRequest } from '../services/user/sign-in/models/request/sign-in-request';
import { SignInResponse } from '../services/user/sign-in/models/response/sign-in-response';
import { SignInService } from '../services/user/sign-in/sign-in-service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {
  formGroup: FormGroup;

  constructor(
    private authGuard: AuthGuard,
    private signInService: SignInService, 
    private router: Router,) { }

  ngOnInit(): void {
    this.formGroup = new FormGroup({
      user: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required])
    });
  }

  onSubmit() {
    const user = this.formGroup.get('user').value;
    const password = this.formGroup.get('password').value;

    this.signIn(user, password).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: () => { alert("No authorized") }
   })
  }

  private signIn(userId: string, password: string): Observable<SignInResponse> {
    return new Observable<SignInResponse>((observer) => {
      const request = new SignInRequest();

      request.login = userId;
      request.password = password;

      this.signInService.signIn(request).subscribe({
        next: (response) => {
          this.authGuard.signIn(response);

          observer.next(response);
        },
        error: (error) => {observer.error(error) }
     });
    });
  }

}
