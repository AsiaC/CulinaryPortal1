import { Component, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ShoppingListService } from 'src/app/_services/shoppingList.service';

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
  recipeIngredients: any[] = [];
 
  newShoppingListName: String = null; 
  selectOptionVal: any;// = {name: "--select--", id:0};

  readioSelected: any;  

  constructor(public bsModalRef: BsModalRef, private toastr: ToastrService, private shoppingListService: ShoppingListService) {}

  ngOnInit(): void {
    //this.list.push({name: "--select--", id:0});
  }

  confirmAddingIngredients(){
    debugger;
    console.log(this.newShoppingListName);
    console.log(this.selectOptionVal);
    if(this.readioSelected === 'radio1'){
      if(this.selectOptionVal !== undefined){
        //dodaj do istniejacej
      //   this.shoppingListService.addRecipeIngredients(this.selectOptionVal, this.recipeIngredients).subscribe(response => {
      //     console.log(response);
      //     this.toastr.success('Success. Ingredients added to the list.');
      //   }, error => {
      //     console.log(error);
      //   })
      // } else {         
      //   this.toastr.error('Failed to add ingredients to shopping list. No list was selected.');    
      // }
    }
  }
    else if (this.readioSelected === 'radio2'){
      if(this.newShoppingListName !== null){
        //dodaj do nowej
        // this.shoppingListService.addShoppingList(this.addShoppingListForm.value).subscribe(response => {
        //   console.log(response);
        //   this.toastr.success('Success. Ingredients added to the list.');
        // }, error => {
        //   console.log(error);
        // })
        
      } else {      
        this.toastr.error('Failed to add ingredients to shopping list. The name of the new list has not been provided.'); 
      }
    }
    this.bsModalRef.hide();//toastr?
  }

  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }

  onChange(event): number {
    return event;
  }
}
