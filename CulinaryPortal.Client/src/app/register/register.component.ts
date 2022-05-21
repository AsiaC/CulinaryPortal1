import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }
  
  register(){
    this.accountService.register(this.model).subscribe(response => {
      this.cancel();  
      if(response === true) {
        this.toastr.success('Success. You have registered correctly.');
      } else if(response.status === 400){
        this.toastr.error('Error! ' + response.error);
        console.log(response);
      } else {
        this.toastr.error('Error! You are not registered, try again.');
        console.log(response);
      }
    })
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
