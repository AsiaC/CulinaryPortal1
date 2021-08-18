import { Component, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ShoppingListService } from 'src/app/_services/shoppingList.service';
import { RecipesService } from 'src/app/_services/recipes.service';
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

  readioSelected: any;  
  shoppingListDto: ShoppingList;
  //newListItems: ShoppingList;

  constructor(public bsModalRef: BsModalRef, private toastr: ToastrService, private recipeService: RecipesService, private shoppingListService: ShoppingListService) {}

  ngOnInit(): void {
    //this.list.push({name: "--select--", id:0});
  }

  confirmAddingIngredients(){
    debugger;
    console.log(this.newShoppingListName);
    console.log(this.selectOptionVal);
    //this.shoppingListDto.id = this.selectOptionVal;    
    
    var newIngredients= [];
    this.recipeIngredients.forEach(element => {
      var newName = element.quantity + " " + element.measure.name + " " + element.ingredient.name;
      newIngredients.push({id: null, name: newName});
    });

    if(this.readioSelected === 'radio1'){
      if(this.selectOptionVal !== undefined){
        //dodaj do istniejacej        
        this.shoppingListDto = this.list.find(x=>x.id === parseInt(this.selectOptionVal));
        //this.shoppingListDto.items = this.shoppingListDto.items.concat(newIngredients);
        this.shoppingListDto.items = this.shoppingListDto.items.concat(newIngredients);
        console.log(this.shoppingListDto);

        this.shoppingListService.updateShoppingList(this.selectOptionVal, this.shoppingListDto)
        .subscribe(response => {
          debugger;
          console.log(response);
            this.toastr.success('Success. Ingredients added to the list.');        
        }, error => {
            console.log(error);                      
        })
      } else {         
        this.toastr.error('Failed to add ingredients to the shopping list. No list was selected.');    
      }
    }
    else if (this.readioSelected === 'radio2'){
      if(this.newShoppingListName !== null){ debugger;
        //dodaj do nowej
        var shoppingListDto2: ShoppingList = {id: null, name: this.newShoppingListName, items: newIngredients, userId: this.userId};

        this.shoppingListService.addShoppingList(shoppingListDto2).subscribe(response => {
          console.log(response);
          this.toastr.success('Success. Ingredients added to the new list.');
        }, error => {
          console.log(error);
        })        
      } else {      
        this.toastr.error('Failed to add ingredients to the shopping list. The name of the new list has not been provided.'); 
      }
    }
    this.bsModalRef.hide();//TODO dla error?toastr? - przy sukcesie dodawania jest
  }

  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }

  onChange(event): number {
    return event;
  }
}
