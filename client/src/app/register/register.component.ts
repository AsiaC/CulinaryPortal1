import { CompileShallowModuleMetadata } from '@angular/compiler';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { error } from 'protractor';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() usersFromHomeComponents: any;
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }
  
  //pod przycieskiem Register
  register(){
    console.log("przed rej");
    console.log(this.model);
    this.accountService.register(this.model).subscribe(response=>{
      console.log(response);
      this.cancel();  
    }, error=>{
      console.log(error);
      this.toastr.error(error.error);
    })
  }

  //pod przyciskiem cancel
  cancel(){
    this.cancelRegister.emit(false);
  }

}
