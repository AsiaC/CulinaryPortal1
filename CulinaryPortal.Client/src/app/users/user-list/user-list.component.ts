import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { UsersService } from 'src/app/_services/users.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[];
  currentUser: User;

  constructor(private userService: UsersService, private toastr: ToastrService, private accountService: AccountService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {this.currentUser = user;});
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(){
    this.userService.getUsers().subscribe(usersResponse => {
      if(usersResponse?.length !== undefined){
        this.users = usersResponse;        
      } else { 
        this.toastr.error('An error occurred while loading users, please try again.');     
        console.log(usersResponse.error.status);      
      }
    })
  }

  deleteUser(userId: number){ 
    this.userService.deleteUser(userId).subscribe(response => {
      this.loadUsers(); 
      if(response.status === 200 ){ 
        this.toastr.success('User removed successfully!');  
      } else {
        this.toastr.error('Error! The user has not been removed.');  
      }
    })
  }
}
