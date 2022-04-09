import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  loginPane: boolean = false;
  registerPane: boolean = false;

  constructor(public accountService: AccountService, private router: Router, private toastr:ToastrService) { }

  ngOnInit(): void {
  }
  
  login(){
    this.accountService.login(this.model).subscribe(response => {
      this.router.navigateByUrl('/recipes');
      if(response === true) {
        this.toastr.success('Success. You have logged in correctly.');
      } else if(response.status === 401){
        this.toastr.error(response.error);
        console.log(response);
      } else {
        this.toastr.error('Error! You are not logged in, try again.');
        console.log(response);
      }
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
  }
}
