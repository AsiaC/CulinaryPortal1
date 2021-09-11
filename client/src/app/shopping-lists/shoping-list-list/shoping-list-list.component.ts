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

  constructor(private shoppingListService: ShoppingListService, private toastr: ToastrService,) { }

  ngOnInit(): void {
    this.loadShoppingLists();
  }

  loadShoppingLists(){
    this.shoppingListService.getShoppingLists().subscribe(shoppingLists=>{
      this.shoppingLists = shoppingLists;
    }, error => {
      console.log(error);
    })
  }

  deleteShoppingList(shoppingListId) {    
    this.shoppingListService.deleteShoppingList(shoppingListId)
      .subscribe(response => {
        this.toastr.success('Shopping list removed successfully!');
        this.loadShoppingLists(); 
      }, error => {
         console.log(error);                      
      })

  }
}
