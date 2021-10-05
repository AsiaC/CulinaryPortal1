import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/user';
import { UsersService } from 'src/app/_services/users.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[];

  constructor(private userService: UsersService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(){
    this.userService.getUsers().subscribe(users => {
      this.users = users;
    }, error => {
      console.log(error);
    })
  }

  deleteUser(userId: number){//TODO delete user
    // this.userService.deleteUser(userId)
    // .subscribe(response => {
    //   this.toastr.success('User removed successfully!');  
    //   this.loadUsers(); 
    // }, error => {
    //    console.log(error);                      
    // })
  }

}
