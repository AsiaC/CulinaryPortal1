import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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

  constructor(public accountService: AccountService, private router: Router, private toastr:ToastrService) { }

  ngOnInit(): void {
  }
  
  login(){
    //console.log(this.model);
    this.accountService.login(this.model).subscribe(response => {    
      this.router.navigateByUrl('/recipes');
    }, error => {
      if(error.status === 401){        
        this.toastr.error(error.error);
      }// todo what if else
    })
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  } 

  showLoginPane(){
    this.loginPane = true;
  }

  showRegisterPane(){

    this.registerPane = true;
    //this.registerPane = !this.registerPane;
  }

}
