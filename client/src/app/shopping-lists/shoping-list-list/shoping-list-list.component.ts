import { Component, OnInit } from '@angular/core';
import { ShoppingList } from 'src/app/_models/shoppingList';
import { ShoppingListService } from 'src/app/_services/shoppingList.service';

@Component({
  selector: 'app-shoping-list-list',
  templateUrl: './shoping-list-list.component.html',
  styleUrls: ['./shoping-list-list.component.css']
})
export class ShopingListListComponent implements OnInit {
  shoppingLists: ShoppingList[];

  constructor(private shoppingListService: ShoppingListService) { }

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
}
