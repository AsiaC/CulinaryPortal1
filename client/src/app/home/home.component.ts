import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode: boolean = false;
  users: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
    // console.log(this.registerMode);
    // this.registerMode = true;
    // console.log(this.registerMode);
  }
  getUsers(){
    this.http.get('http://localhost:50725/api/users').subscribe( users => {
      this.users = users;
    }, error => {
      console.log(error);
    })
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }

}
