import { CompileShallowModuleMetadata } from '@angular/compiler';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() usersFromHomeComponents: any;
  @Output() cancelRegister = new EventEmitter();
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
    this.cancelRegister.emit(false);
  }

}
