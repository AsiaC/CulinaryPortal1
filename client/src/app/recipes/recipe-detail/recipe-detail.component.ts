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

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.css']
})
export class RecipeDetailComponent implements OnInit {
  recipe: Recipe;
  user: User; //current
  editRecipe: boolean = false;
  canAddToCookbook: boolean = true;
  userCookbook: Cookbook;
  cookbookRecipe: any = {recipeId: null, userId: null};
  userShoppingLists: ShoppingList[];
  bsModalRef: BsModalRef;
  //Delete recipe only when it is not in culinary book
  constructor(private recipeService: RecipesService, private route: ActivatedRoute, private accountService:AccountService, private cookbookService:CookbookService, private userService:UsersService, private toastr: ToastrService, private modalService: BsModalService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {this.user = user;});
  }

  ngOnInit(): void {
    this.loadRecipe();
    this.loadCookbook();  
    this.loadShoppingListsIds();
  }

  loadRecipe(){
    this.recipeService.getRecipe(Number(this.route.snapshot.paramMap.get('id'))).subscribe(recipe =>{
      this.recipe = recipe;       
      //debugger; 
      //console.log(recipe);
      //console.log(this.user);
      // console.log(recipe.difficultyLevel);
      // console.log(recipe.preparationTime); 
    }, error => {
      console.log(error);
    })
  }

  loadCookbook(){
    //debugger;
    this.userService.getUserCookbook(this.user.id).subscribe(userCookbook => {
      this.userCookbook = userCookbook;
      //debugger;      
      console.log("loadCookbook");
      console.log(this.userCookbook);
      // if(this.userCookbook !== undefined){
      //   var allRecipe = this.userCookbook.recipes;
      //  console.log(allRecipe);
      //  var a = this.userCookbook.recipes.find(e=>e.id === this.recipe.id);
      //   if(a !== undefined) {
      //     this.canAddToCookbook = false;
      //   }
      // }
      if(this.userCookbook !== undefined){
        if(this.userCookbook.recipes !== undefined){ //spr co gdy użytkownik nie ma cookbook wcale, albo gdy nie ma ani jednego przepisu w cookbook
          var recipeInCookbook = this.userCookbook.recipes.find(r=>r.id === this.recipe.id);
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
    this.cookbookRecipe.recipeId = this.recipe.id;
    this.cookbookRecipe.userId = this.user.id;
    
      this.cookbookService.addRecipeToCookbook(this.cookbookRecipe)
      .subscribe(response =>{
         this.toastr.success('Recipe added successfully');
         this.canAddToCookbook = false;
      }, error => {
        console.log(error);
      })
  }

  removeFromCookbook(){debugger;
    this.cookbookRecipe.recipeId = this.recipe.id;
    this.cookbookRecipe.userId = this.user.id;

    this.cookbookService.removeRecipeFromCookbook(this.cookbookRecipe)
    .subscribe(response => {
      this.toastr.success('Recipe removed successfully');
      this.canAddToCookbook = true;
    }, error => {
        console.log(error);
    })

  }

  addToShoppingList() {
    //check if user have one shoppingList
    //if 1 dodaj
    if(this.userShoppingLists.length == 1) {
      
    }

    //if 2 modal i wybierz do której dodać
    //if 0 modal stwórz liste - nazwa tylko 
  }

  modalAddToShoppingList() {
    console.log(this.userShoppingLists);

    const initialState = {     
      recipeIngredients: this.recipe.recipeIngredients,
      list: this.userShoppingLists,
      title: 'Add ingredients to shopping list', 
      userId: this.user.id
    };
    this.bsModalRef = this.modalService.show(SelectShoppingListComponent, {initialState});
    this.bsModalRef.content.closeBtnName = 'Cancel';
    this.bsModalRef.content.submitBtnName = 'Confirm adding ingredients';
  }
}
