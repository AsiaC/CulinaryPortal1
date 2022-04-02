import { Component, OnInit } from '@angular/core';
import { ShoppingList } from 'src/app/_models/shoppingList';
import { ShoppingListService } from 'src/app/_services/shoppingList.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shoping-list-list',
  templateUrl: './shoping-list-list.component.html',
  styleUrls: ['./shoping-list-list.component.css']
})
export class ShopingListListComponent implements OnInit {
  shoppingLists: ShoppingList[];
  alertText: string;

  constructor(private shoppingListService: ShoppingListService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.loadShoppingLists();
  }

  loadShoppingLists(){
    this.shoppingListService.getShoppingLists().subscribe(userShoppingListsResponse=>{
      if(userShoppingListsResponse?.length !== undefined){
        this.shoppingLists = userShoppingListsResponse;
      } else {
        this.shoppingLists = [];
        this.router.navigateByUrl('/recipes');   
        if(userShoppingListsResponse.error.status === 401){
          this.toastr.error('You do not have access to this content.');
        } else {
          this.toastr.error('An error occurred, please try again.');
        }    
      }      
    }, error => {
      console.log(error);
      this.router.navigateByUrl('/recipes');     
      this.toastr.error('An error occurred, please try again.');
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
