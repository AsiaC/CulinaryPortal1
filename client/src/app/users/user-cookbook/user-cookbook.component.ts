import { Component, OnInit } from '@angular/core';
import { Cookbook } from 'src/app/_models/cookbook'
import { UsersService } from 'src/app/_services/users.service';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';


@Component({
  selector: 'app-user-cookbook',
  templateUrl: './user-cookbook.component.html',
  styleUrls: ['./user-cookbook.component.css']
})
export class UserCookbookComponent implements OnInit {
  userCookbook: Cookbook;
  user: User;

  constructor(private userService:UsersService, private accountService:AccountService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadUserCookbook();
  }

  loadUserCookbook(){
    //debugger;
    console.log(this.user);
    this.userService.getUserCookbook(this.user.id).subscribe(userCookbook => {
      this.userCookbook = userCookbook;
      //debugger;
      //console.log(this.userCookbook);
    }, error => {
      console.log(error);
    })
  }

}
