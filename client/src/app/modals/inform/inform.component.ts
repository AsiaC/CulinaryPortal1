import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-inform',
  templateUrl: './inform.component.html',
  styleUrls: ['./inform.component.css']
})
export class InformComponent implements OnInit {
  message: string;
  objectName: string;

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
  }

  ok(){
    if(this.objectName ==='Recipe'){//debugger; TODO localhost moze trzeba zamienic jakoś ?
      window.location.href='http://localhost:4200/recipes';         
    }
    this.bsModalRef.hide();
  }

}
