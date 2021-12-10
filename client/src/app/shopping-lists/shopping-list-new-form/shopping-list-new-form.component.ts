import { Component, Input, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RecipesService } from 'src/app/_services/recipes.service';
import {Category} from 'src/app/_models/category';
import { DifficultyLevelEnum } from 'src/app/_models/difficultyLevelEnum';
import { PreparationTimeEnum } from 'src/app/_models/preparationTimeEnum';
import { Console } from 'console';
import { Ingredient } from 'src/app/_models/ingredient';
import { Measure } from 'src/app/_models/measure';
import { FormGroup, FormControl,FormArray, FormBuilder, Validators, NgForm } from '@angular/forms'
import { Recipe } from 'src/app/_models/recipe';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { first, take } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
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
  
  constructor(private shoppingListService: ShoppingListService, private fb:FormBuilder, private accountService:AccountService, private route: ActivatedRoute, private router: Router) { 
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
    //console.log(this.addShoppingListForm.value);   
    if(this.isAddMode){
      this.createNewShoppingList();
    }
    else{
      this.updateShoppingList();
    }
  }
  createNewShoppingList(){
    this.submitted = true;
    this.shoppingListService.addShoppingList(this.addShoppingListForm.value).subscribe(response => {
      //console.log(response);
      //this.router.navigateByUrl('/members');
      this.isAddMode = false;
      window.location.reload(); //spr czy to konieczne
    }, error => {
      //this.validationErrors = error;
      console.log(error);
    })
  }
  updateShoppingList(){
    this.shoppingListService.updateShoppingList(this.id, this.addShoppingListForm.value)
      .subscribe(response => {
        //this.toastr.success('Profile updated successfully');
        this.shoppingList = this.addShoppingListForm.value;
        this.isAddMode = false;
        this.addShoppingListForm.reset(this.shoppingList);
        window.location.reload();
      }, error => {
          console.log(error);                      
      })
  }
  cancel(){
    //console.log("cancel");
    //REFRESH PAGE OR addNewMode=FALSE ALE TO JEST Z componentu rodzica wiec trzebaby przekazać do rodzica
    window.location.reload();
  }

 
}
