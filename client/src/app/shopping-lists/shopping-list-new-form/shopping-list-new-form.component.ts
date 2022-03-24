import { Component, Input, OnInit} from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms'
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { first, take } from 'rxjs/operators';
import { ShoppingList } from 'src/app/_models/shoppingList';
import { ShoppingListService } from 'src/app/_services/shoppingList.service';

@Component({
  selector: 'app-shopping-list-new-form',
  templateUrl: './shopping-list-new-form.component.html',
  styleUrls: ['./shopping-list-new-form.component.css']
})
export class ShoppingListNewFormComponent implements OnInit {
  submitted: boolean = false;
  addShoppingListForm: FormGroup
  user: User;
  shoppingList: ShoppingList;
  isAddMode: boolean;
  id: string;
  itemIsRemoved: boolean = false;
  @Input()selectedListId: number;
  
  constructor(private shoppingListService: ShoppingListService, private fb:FormBuilder, private accountService:AccountService, private toastr: ToastrService,) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    if(this.selectedListId !== undefined){
      this.id = this.selectedListId.toString();
    } 
    this.isAddMode = !this.id;
    this.initializeListForm();
    if(!this.isAddMode)
    {
      this.loadShoppingList();
    } 
  }

  loadShoppingList() {
    this.shoppingListService.getShoppingList(this.selectedListId)
    .pipe(first())
    .subscribe(shoppingList => {
      this.shoppingList = shoppingList;
      this.addShoppingListForm.patchValue({
        id: shoppingList.id,
        name: shoppingList.name,
      });
      var listItemsArray = [];
      this.shoppingList.items.forEach(listItem => listItemsArray.push(this.fb.group({
        itemName: listItem.itemName,
        id: listItem.id
      })));
      this.addShoppingListForm.setControl('items', this.fb.array(listItemsArray || []));      
    }, error => {
      console.log('Error during list loading.');
      console.log(error);
    });
  }

  initializeListForm() {
    this.addShoppingListForm = this.fb.group({
      id: [],
      name: ['', [Validators.required]],
      items: this.fb.array([this.createItemFormGroup()], [Validators.required]),
      userId: [this.user.id]
    })
  }

  createItemFormGroup(){
    return this.fb.group({
      itemName:  ['', [Validators.required]]
    })
  }

  get items() : FormArray {
    return (this.addShoppingListForm.get("items") as FormArray)
  }

  addItem() {
    let fg = this.createItemFormGroup();
    this.items.push(fg);
  }  

  removeItem(i:number) {
    this.items.removeAt(i);
    this.itemIsRemoved = true;
  }

  onSubmit() {
    if(this.isAddMode){
      this.createNewShoppingList();
    } else{
      this.updateShoppingList();
    }
  }

  createNewShoppingList(){
    this.submitted = true;
    this.shoppingListService.addShoppingList(this.addShoppingListForm.value).subscribe(response => {
      if(response.status === 200 ){ 
        this.isAddMode = false;
        // It is necessary to reload the window
        window.location.reload(); 
        this.toastr.success('Success. The new list has been created.');
      } else {   
        this.isAddMode = false;
        // It is necessary to reload the window
        window.location.reload();      
        this.toastr.error('Error! The new list has not been created.');
        console.log(response);
      }   
    }, error => {     
      console.log('Error during creating a list.'); 
      console.log(error);
    })
  }

  updateShoppingList(){
    this.shoppingListService.updateShoppingList(this.id, this.addShoppingListForm.value)
      .subscribe(response => {
        if(response.status === 200 ){ 
          this.shoppingList = this.addShoppingListForm.value;
          this.isAddMode = false;
          this.addShoppingListForm.reset(this.shoppingList);
          // It is necessary to reload the window
          window.location.reload();
          this.toastr.success('Success. The list has been updated.');
        } else {  
          this.isAddMode = false;
          this.addShoppingListForm.reset(this.shoppingList);
          // It is necessary to reload the window
          window.location.reload();
          console.log('Error during updating the list.');         
          console.log(response);
        }
      }, error => {
        console.log('Error during updating the list.'); 
        console.log(error);                    
      })
  }

  cancel(){    
    window.location.reload();
  }
}
