import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { UsersService } from 'src/app/_services/users.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  user: User;

  constructor(private accountService:AccountService, private userService:UsersService, private toastr: ToastrService, private router: Router) {    
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser(){
    this.userService.getUser(this.user.id).subscribe(user=>{
      if(user.id !== undefined) {
        this.user = user;
      } else {
        console.log('Error while loading the user data');        
        this.router.navigateByUrl('/recipes');
        this.toastr.error('An error occurred, please try again.');
      }
    }, error => {
      console.log(error);
      this.router.navigateByUrl('/recipes');
      this.toastr.error('An error occurred, please try again.'); 
    })
  }
  
  updateUser(){
    this.userService.updateUser(this.user).subscribe(response => {
      if(response.status === 200 ){ 
        this.toastr.success('User updated successfully!');
        this.editForm.reset(this.user);     
      } else {
        console.log('Error during updating the user.');  
        this.editForm.reset(this.user);
      }
    });    
  }
}
