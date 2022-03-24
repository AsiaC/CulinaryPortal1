import { Component, OnInit } from '@angular/core';
import { ShoppingList } from 'src/app/_models/shoppingList';
import { ShoppingListService } from 'src/app/_services/shoppingList.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-shoping-list-list',
  templateUrl: './shoping-list-list.component.html',
  styleUrls: ['./shoping-list-list.component.css']
})
export class ShopingListListComponent implements OnInit {
  shoppingLists: ShoppingList[];
  alertText: string;

  constructor(private shoppingListService: ShoppingListService, private toastr: ToastrService,) { }

  ngOnInit(): void {
    this.loadShoppingLists();
  }

  loadShoppingLists(){
    this.shoppingListService.getShoppingLists().subscribe(shoppingLists=>{
      this.shoppingLists = shoppingLists;
    }, error => {
      if(error.status === 401){
        this.alertText = "You do not have access to this content.";
      } else if(error.status === 404){
        this.alertText = "Users do not have any shopping lists yet."
      }
    })
  }

  deleteShoppingList(shoppingListId: number) {    
    this.shoppingListService.deleteShoppingList(shoppingListId).subscribe(response => {
      this.loadShoppingLists(); 
      if(response.status === 200 ){ 
        this.toastr.success('Shopping list removed successfully!');  
      } else {
        this.toastr.error('Error! The shopping list has not been removed.');  
      }
    }, error => {
      this.toastr.error('Error during deleting the shopping list.'); 
      console.log(error);                      
    })
  }
}
