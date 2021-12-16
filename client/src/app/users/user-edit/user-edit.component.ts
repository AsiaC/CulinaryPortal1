import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { UsersService } from 'src/app/_services/users.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  user:User;

  constructor(private accountService:AccountService, private userService:UsersService, private toastr: ToastrService) {    
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser(){
    var checkId=this.user.id;
    this.userService.getUser(this.user.id).subscribe(user=>{
      this.user = user;
    })
  }

  updateUser(){
    this.userService.updateUser(this.user).subscribe(() => {
      this.toastr.success('User updated successfully!');
      this.editForm.reset(this.user);      
    }, error => {
      console.log(error);
    });    
  }
}
