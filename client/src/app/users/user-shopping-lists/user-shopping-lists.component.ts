import { Component, OnInit } from '@angular/core';
import { UsersService } from 'src/app/_services/users.service';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { ShoppingList } from 'src/app/_models/shoppingList';
import { ShoppingListService } from 'src/app/_services/shoppingList.service';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-shopping-lists',
  templateUrl: './user-shopping-lists.component.html',
  styleUrls: ['./user-shopping-lists.component.css']
})
export class UserShoppingListsComponent implements OnInit {
  userShoppingLists: ShoppingList[];
  user: User;
  addNewListMode: boolean = false;
  selectedShoppingListId: number;
  bsModalRef: BsModalRef;
  alertText: string;

  constructor(private userService:UsersService, private accountService:AccountService, private toastr: ToastrService, private shoppingListService: ShoppingListService, private router: Router) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadUserShoppingLists();
  }

  loadUserShoppingLists(){
    this.userService.getUserShoppingLists(this.user.id).subscribe(userShoppingListsResponse => {
      if(userShoppingListsResponse?.length !== undefined){
        this.userShoppingLists = userShoppingListsResponse;
      } else {
        this.userShoppingLists = undefined;
        if(userShoppingListsResponse.error.status === 401){
          this.alertText = "You do not have access to this content.";
        } else if(userShoppingListsResponse.error.status === 404){
          this.alertText = "You do not have any shopping lists yet."
        } else {
          this.router.navigateByUrl('/recipes');     
          this.alertText = 'An error occurred, please try again.';
        }    
      }
    })
  }

  addShoppingList(){
    this.addNewListMode = !this.addNewListMode;
  }

  editShoppingList(shoppingListId)  {
    this.selectedShoppingListId = shoppingListId;
    this.addNewListMode = true;
  }

  deleteShoppingList(shoppingListId) {
    this.shoppingListService.deleteShoppingList(shoppingListId).subscribe(response => {
      if(response.status === 200 ){ 
        this.toastr.success('Shopping list removed successfully!');
        this.loadUserShoppingLists()
      } else {
        this.toastr.error('Error! Shopping list cannot be removed.');
        console.log(response);
      }        
    })
  }
}
