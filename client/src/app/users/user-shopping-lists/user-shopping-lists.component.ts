import { Component, OnInit } from '@angular/core';
import { UsersService } from 'src/app/_services/users.service';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { ShoppingList } from 'src/app/_models/shoppingList';
import { ShoppingListService } from 'src/app/_services/shoppingList.service';

@Component({
  selector: 'app-user-shopping-lists',
  templateUrl: './user-shopping-lists.component.html',
  styleUrls: ['./user-shopping-lists.component.css']
})
export class UserShoppingListsComponent implements OnInit {
  userShoppingLists: ShoppingList[];
  user: User;
  addNewListMode:boolean = false;
  selectedShoppingListId: number;

  constructor(private userService:UsersService, private accountService:AccountService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadUserShoppingLists();
  }

  loadUserShoppingLists(){
    //debugger;
    console.log(this.user);
    this.userService.getUserShoppingLists(this.user.id).subscribe(userShoppingLists=>{
      this.userShoppingLists = userShoppingLists;
      //debugger;
    }, error =>{
      console.log(error);
    })
  }

  addShoppingList(){
    this.addNewListMode = !this.addNewListMode;
  }

  editShoppingList(shoppingListId)  {//debugger;
    this.selectedShoppingListId = shoppingListId;
    this.addNewListMode = true;
  }

  deleteShoppingList(shoppingListId) {
    //deleta and refresh site
  }
}
