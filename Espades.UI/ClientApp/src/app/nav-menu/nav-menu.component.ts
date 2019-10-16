import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../login/login.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  constructor(private route: Router,
    private loginService: LoginService) { }

  isExpanded = false;
  loggedUser: string = localStorage.getItem("loggedUser");

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  ngOnInit() {
    this.loggedUser = localStorage.getItem("loggedUser");
  }

  logout() {
    this.route.navigate(['login']);
    this.loginService.doLogout();
  }
}
