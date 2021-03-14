import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { UsersService } from 'src/app/_services/users.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  user:User;

  constructor(private accountService:AccountService, private userService:UsersService) {
    
    var check=this.accountService.currentUser$
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.loadUser();
  }

  //member
  loadUser(){
    var checkId=this.user.id;
    this.userService.getUser(this.user.id).subscribe(user=>{
      this.user = user;
    })
  }

  updateUser(){
    
    console.log(this.user);
    this.userService.updateUser(this.user).subscribe(() => {
      this.editForm.reset(this.user);
    });
    
  }
}
