import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthGuard } from '../helpers/auth-guard';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  userName = '';

  constructor(
    private router: Router,
    private authGuard: AuthGuard) { }

  ngOnInit(): void {
    this.userName = this.authGuard.getUserName();
  }

  home() {
    this.router.navigate(['/']);
  }

  signOut() {
    this.authGuard.signOut();
    this.router.navigate(['/login']);
  }
}
