import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Recipe } from 'src/app/_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { CookbookService } from 'src/app/_services/cookbook.service';
import { CookbookRecipe } from 'src/app/_models/cookbookRecipe';
import { UsersService } from 'src/app/_services/users.service';
import { Cookbook } from 'src/app/_models/cookbook';
import { ToastrService } from 'ngx-toastr';
import { ShoppingList } from 'src/app/_models/shoppingList';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { SelectShoppingListComponent } from 'src/app/modals/select-shopping-list/select-shopping-list.component';
import { ConfirmComponent } from 'src/app/modals/confirm/confirm.component';
import { CreateCookbookComponent } from 'src/app/modals/create-cookbook/create-cookbook.component';


@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.css']
})
export class RecipeDetailComponent implements OnInit {
  currentRecipe: Recipe;
  user: User; //current
  editRecipe: boolean = false;
  canAddToCookbook: boolean = true;
  userCookbook: Cookbook;
  //cookbookRecipe: any = {recipeId: null, userId: null};
  cookbookRecipe: CookbookRecipe;
  userShoppingLists: ShoppingList[];
  bsModalRef: BsModalRef;
  recipeIsInsideCookbook: boolean = true;
  //Delete recipe only when it is not in culinary book
  constructor(private recipeService: RecipesService, private route: ActivatedRoute, private accountService:AccountService, private cookbookService:CookbookService, private userService:UsersService, private toastr: ToastrService, private modalService: BsModalService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {this.user = user;});
  }

  ngOnInit(): void {
    this.loadRecipe();
    //this.loadCookbook();  
    this.loadShoppingListsIds();
    debugger;
    console.log(this.user);
    console.log(this.userCookbook);
  }

  loadRecipe(){
    debugger;
    this.recipeService.getRecipe(Number(this.route.snapshot.paramMap.get('id'))).subscribe(recipe =>{
      debugger;
      this.currentRecipe = recipe;       
      debugger; 
      //console.log(recipe);
      //console.log(this.user);
      // console.log(recipe.difficultyLevel);
      // console.log(recipe.preparationTime); 
      this.loadCookbook();
    }, error => {
      console.log(error);
    })
  }

  loadCookbook(){
    //debugger;
    this.userService.getUserCookbook(this.user.id).subscribe(userCookbook => {
      this.userCookbook = userCookbook;
      debugger;      
      console.log("loadCookbook");
      console.log(this.userCookbook);
      console.log(this.currentRecipe);
      // if(this.userCookbook !== undefined){
      //   var allRecipe = this.userCookbook.recipes;
      //  console.log(allRecipe);
      //  var a = this.userCookbook.recipes.find(e=>e.id === this.recipe.id);
      //   if(a !== undefined) {
      //     this.canAddToCookbook = false;
      //   }
      // }

      // if(this.userCookbook !== undefined){
      //   if(this.userCookbook.recipes !== undefined){ //spr co gdy użytkownik nie ma cookbook wcale, albo gdy nie ma ani jednego przepisu w cookbook
      //     var recipeInCookbook = this.userCookbook.recipes.find(r=>r.id === this.recipe.id);
      //     if(recipeInCookbook !== undefined) {
      //       this.canAddToCookbook = false;
      //     }
      //   }
      // }

      if(this.userCookbook !== undefined){
        if(this.userCookbook.cookbookRecipes !== undefined){
          var recipeInCookbook = this.userCookbook.cookbookRecipes.find(r=>r.recipeId === this.currentRecipe.id);
           if(recipeInCookbook !== undefined) {
              this.canAddToCookbook = false;
           }
        }
      }
    }, error => {
      console.log(error);
    })
  }

  loadShoppingListsIds(){
    //debugger; //potrzebne do modala bo user moze miec kilka list wiec trzeba wybrać 
    this.userService.getUserShoppingLists(this.user.id).subscribe(userShoppingLists => {
      this.userShoppingLists = userShoppingLists;
      //debugger;      
      //console.log("loadShoppingListsIds");
      //console.log(this.userShoppingLists);           
    }, error => {
      console.log(error);
    })
  }

  editThisRecipe()  {
    this.editRecipe = true;
  }

  addToCookbook(){  
    debugger;
    console.log(this.user);
    console.log(this.userCookbook);

    // if(this.userCookbook === undefined){
    //   this.cookbookRecipe.cookbookId = 0;
    // }else{
    //   this.cookbookRecipe.cookbookId = this.userCookbook.id;
    // }
    if(this.userCookbook === undefined){
        //modal + utowrz nową + 1 powiazanie
        //this.cookbookRecipe.recipeId = this.currentRecipe.id;
        //this.cookbookRecipe.userId = this.user.id;  
        this.cookbookRecipe = {recipeId: this.currentRecipe.id, userId: this.user.id, cookbookId: this.userCookbook.id, note: null, recipe: this.currentRecipe}
    
        const initialState = {     
          title: 'Create cookbook and add indicated recipe', 
          userId: this.user.id,
          currentRecipe: this.cookbookRecipe
        };
        this.bsModalRef = this.modalService.show(CreateCookbookComponent, {initialState});
        this.bsModalRef.content.closeBtnName = 'Cancel';
        this.bsModalRef.content.submitBtnName = 'Confirm';
        this.canAddToCookbook = false;
    }else{
    //this.cookbookRecipe.recipeId = this.currentRecipe.id;
    //this.cookbookRecipe.userId = this.user.id;    
    this.cookbookRecipe = {recipeId: this.currentRecipe.id, userId: this.user.id, cookbookId: this.userCookbook.id, note: null, recipe: this.currentRecipe}
    
      this.cookbookService.addRecipeToCookbook(this.cookbookRecipe)
      .subscribe(response =>{
         this.toastr.success('Recipe added successfully!');
         this.canAddToCookbook = false;
      }, error => {
        console.log(error);
      })
    }
  }

  removeFromCookbook(){debugger;
    //this.cookbookRecipe.recipeId = this.currentRecipe.id;
    //this.cookbookRecipe.userId = this.user.id;
    this.cookbookRecipe = {recipeId: this.currentRecipe.id, userId: this.user.id, cookbookId: this.userCookbook.id, note: null, recipe: this.currentRecipe}
    
    this.cookbookService.removeRecipeFromCookbook(this.userCookbook.id, this.cookbookRecipe)
    .subscribe(response => {
      this.toastr.success('Recipe removed successfully');
      this.canAddToCookbook = true;
    }, error => {
        console.log(error);
    })

  }

  modalAddToShoppingList() {
    console.log(this.userShoppingLists);

    const initialState = {     
      recipeIngredients: this.currentRecipe.recipeIngredients,
      list: this.userShoppingLists,
      title: 'Add ingredients to shopping list', 
      userId: this.user.id
    };
    this.bsModalRef = this.modalService.show(SelectShoppingListComponent, {initialState});
    this.bsModalRef.content.closeBtnName = 'Cancel';
    this.bsModalRef.content.submitBtnName = 'Confirm adding ingredients';
  }

  deleteRecipe(){debugger;
    //check if recipe is inside in cookbook if yes user cannot delete recipe
    

    // if(this.recipeIsInsideCookbook === true){
    // const initialState = {  
    //   title: 'You cannott delete the recipe because at least one user has it in their cookbook',      
    //   idToRemove: this.recipe.id,
    //   objectName:'Simple'
    // };
    // this.bsModalRef = this.modalService.show(ConfirmComponent, {initialState});
    // this.bsModalRef.content.closeBtnName = 'Cancel'; //a moze to w initial state wrzuc? co za róznica?
    // this.bsModalRef.content.submitBtnName = 'Ok';
    // } else{
    //delete list and items and refresh site
    const initialState = {  
      title: 'Are you sure that you would like to delete indicated recipe?',      
      idToRemove: this.currentRecipe.id,
      objectName:'Recipe'
    };
    this.bsModalRef = this.modalService.show(ConfirmComponent, {initialState});
    this.bsModalRef.content.closeBtnName = 'Cancel'; //a moze to w initial state wrzuc? co za róznica?
    this.bsModalRef.content.submitBtnName = 'Confirm deleting recipe';
    // }  

    //ale tu moge dodać modal bo to ważna akcja a i tak musze przeładowac stronę bo wrócić do strony z przepisami wszystkimi
    
  }
}
