import { Component, OnInit } from '@angular/core';
import { UsersService } from 'src/app/_services/users.service';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { ShoppingList } from 'src/app/_models/shoppingList';
import { ShoppingListService } from 'src/app/_services/shoppingList.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ConfirmComponent } from 'src/app/modals/confirm/confirm.component';
import { ToastrService } from 'ngx-toastr';

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

  constructor(private userService:UsersService, private accountService:AccountService, private modalService: BsModalService, private toastr: ToastrService, private shoppingListService: ShoppingListService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadUserShoppingLists();
  }

  loadUserShoppingLists(){
    this.userService.getUserShoppingLists(this.user.id).subscribe(userShoppingLists=>{
      this.userShoppingLists = userShoppingLists;
    }, error =>{     
      this.userShoppingLists = undefined; //TODO czy to potrzebne?     
      if(error.status === 401){
        this.alertText = "You do not have access to this content.";
      } else if(error.status === 404){
        this.userShoppingLists = undefined;
        this.alertText = "You do not have any shopping lists yet."
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
    //delete list and items and refresh site
    // const initialState = {  
    //   title: 'Are you sure that you would like to delete indicated shopping list?',      
    //   idToRemove: shoppingListId,
    //   objectName:'Shopping list', 
    //   userId: this.user.id,
    //   userShoppingLists: this.userShoppingLists,
    //   //functionAfterClose: this.loadUserShoppingLists()
    // };
    // this.bsModalRef = this.modalService.show(ConfirmComponent, {initialState});
    // this.bsModalRef.content.closeBtnName = 'Cancel'; //a moze to w initial state wrzuc? co za róznica?
    // this.bsModalRef.content.submitBtnName = 'Confirm deleting shopping list';
    
    //ponizsze rozwiazanie jest lepsze zamiast modale potwierdzającego bo nie trzeba przeładowywać strony tylko nika lista z ekranu, ale nie ma modala.
    this.shoppingListService.deleteShoppingList(shoppingListId)
      .subscribe(response => {
        this.toastr.success('Shopping list removed successfully!');
        this.loadUserShoppingLists(); 
      }, error => {
         console.log(error);                      
      })

  }
}
