import { CompileShallowModuleMetadata } from '@angular/compiler';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() usersFromHomeComponents: any;
  model: any = {};

  constructor() { }

  ngOnInit(): void {
  }
  
  //pod przycieskiem Register
  register(){
    console.log(this.model);
  }

  //pod przyciskiem cancel
  cancel(){
    console.log('canceled');
  }

}
