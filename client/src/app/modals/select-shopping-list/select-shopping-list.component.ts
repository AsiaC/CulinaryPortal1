import { Component, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-select-shopping-list',
  templateUrl: './select-shopping-list.component.html',
  styleUrls: ['./select-shopping-list.component.css']
})
export class SelectShoppingListComponent implements OnInit {
  title: string;
  closeBtnName: string;
  submitBtnName: string;
  list: any[] = [];
 
  newShoppingListName: String = null; 
  selectOptionVal: any;// = {name: "--select--", id:0};

  constructor(public bsModalRef: BsModalRef) {}

  ngOnInit(): void {
    //this.list.push({name: "--select--", id:0});
  }

  confirmAddingIngredients(){
    debugger;
    console.log(this.newShoppingListName);
    console.log(this.selectOptionVal);

    this.bsModalRef.hide();
  }
  
  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }

  onChange(event: Event): any {
    //debugger;
    return event;
  }
}
