import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ShoppingListService } from 'src/app/_services/shoppingList.service';
import { ShoppingList } from 'src/app/_models/shoppingList';

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
  userId: number;
 
  newShoppingListName: string = null; 
  selectOptionVal: any;// = {name: "--select--", id:0};

  radioSelected: any;  
  shoppingListDto: ShoppingList;

  constructor(public bsModalRef: BsModalRef, private toastr: ToastrService, private shoppingListService: ShoppingListService) {}

  ngOnInit(): void {
  }

  confirmAddingIngredients(){   
    var newIngredients= [];
    this.recipeIngredients.forEach(element => {
      var newName = element.quantity + " " + element.measure.name + " " + element.ingredient.name;
      newIngredients.push({id: null, itemName: newName});
    });

    if(this.radioSelected === 'radio1'){
      if(this.selectOptionVal !== undefined){
        // Adding ingredients to an existing and selected shopping list         
        this.shoppingListDto = this.list.find(x=>x.id === parseInt(this.selectOptionVal));
        this.shoppingListDto.items = this.shoppingListDto.items.concat(newIngredients);

        this.shoppingListService.updateShoppingList(this.selectOptionVal, this.shoppingListDto)
        .subscribe(response => {
          if(response.status === 200 ){ 
            this.toastr.success('Success. Ingredients added to the existing list.');
          } else {
            this.toastr.error('Error! Ingredients cannot be added.');
            console.log(response);
          }  
        })
      } else {         
        this.toastr.error('Failed to add ingredients to the shopping list. No list was selected.');    
        console.log('Failed to add ingredients to the shopping list. No list was selected.');
      }
    }
    else if (this.radioSelected === 'radio2'){
      if(this.newShoppingListName !== null){        
        // Adding ingredients to a new shopping list
        var shoppingListDto2: ShoppingList = {id: null, name: this.newShoppingListName, items: newIngredients, userId: this.userId, userName: ''};

        this.shoppingListService.addShoppingList(shoppingListDto2).subscribe(response => {
          if(response.status === 200 ){ 
            this.toastr.success('Success. Ingredients added to the new list.');
          } else {
            this.toastr.error('Error! Ingredients cannot be added.');
            console.log(response);
          }
        })        
      } else {      
        this.toastr.error('Failed to add ingredients to the shopping list. The name of the new list has not been provided.'); 
        console.log('Failed to add ingredients to the shopping list. The name of the new list has not been provided.');
      }
    }
    this.bsModalRef.hide();
  }

  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }

  onChange(event): number {
    return event;
  }
}
