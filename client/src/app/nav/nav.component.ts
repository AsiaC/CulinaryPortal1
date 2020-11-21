import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  //loggedIn: boolean; //jesli nie przypiszemy to jest zawsze false
  loginPane: boolean = false;
  registerPane: boolean = false;

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
  }
  
  login(){
    //console.log(this.model);
    this.accountService.login(this.model).subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);      
    })
  }

  logout(){
    this.accountService.logout();
  } 

  showLoginPane(){
    this.loginPane = true;
  }

  showRegisterPane(){
    debugger;
    this.registerPane = true;
    //this.registerPane = !this.registerPane;
  }

}
