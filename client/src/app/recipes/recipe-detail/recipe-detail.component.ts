import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
import { Rate } from 'src/app/_models/rate';
import { PreparationTimeEnum } from 'src/app/_models/preparationTimeEnum';
import { DifficultyLevelEnum } from 'src/app/_models/difficultyLevelEnum';

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
  cookbookRecipe: CookbookRecipe;
  userShoppingLists: ShoppingList[];
  bsModalRef: BsModalRef;
  recipeIsInsideCookbook: boolean = true;
  rateModel: Rate = {recipeId: 0, userId: 0, value: 0, id: 0};
  PreparationTimeEnum = PreparationTimeEnum;
  DifficultyLevelEnum = DifficultyLevelEnum;

  //Delete recipe only when it is not in culinary book TODO NIE DZIAA?
  constructor(private recipeService: RecipesService, private route: ActivatedRoute, private accountService:AccountService, private cookbookService:CookbookService, private userService:UsersService, private toastr: ToastrService, private modalService: BsModalService, private router: Router) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {this.user = user;});
  }

  ngOnInit(): void {
    this.loadRecipe();   
  }

  loadRecipe(){  
    this.recipeService.getRecipe(Number(this.route.snapshot.paramMap.get('id'))).subscribe(recipe =>{ 
      if(recipe?.id !== undefined){
        this.currentRecipe = recipe; 
        if(this.user !== undefined){
          this.loadShoppingListsIds();  
          this.loadCookbook();

          if(this.user !== undefined){
            this.userService.getUserRecipeRate(this.user.id, Number(this.route.snapshot.paramMap.get('id'))).subscribe(rateResponse=>{
              if(rateResponse?.id !== undefined){
                this.rateModel = {recipeId: Number(this.route.snapshot.paramMap.get('id')), userId: this.user.id, value: rateResponse.value, id: rateResponse.id};
              } else if(rateResponse !== undefined) {
                this.rateModel = {recipeId: Number(this.route.snapshot.paramMap.get('id')), userId: this.user.id, value: 0, id: 0};
              } else {
                this.toastr.error('An error occurred, please refresh the page.');  
                console.log(rateResponse);
              }            
            })
          }
          
        } 
      } else {
        console.log('Error while displaying a recipe');        
        this.router.navigateByUrl('/recipes');
        this.toastr.error('An error occurred, please try again.');
      }              
    }, error => {
      console.log(error);
      this.router.navigateByUrl('/recipes');
      this.toastr.error('An error occurred, please try again.');      
    })    
  }

  loadCookbook(){
    this.userService.getUserCookbook(this.user.id).subscribe(userCookbook => {      
      if(userCookbook?.id !== undefined){
        this.userCookbook = userCookbook;  
        if(this.userCookbook.cookbookRecipes !== undefined){
          var recipeInCookbook = this.userCookbook.cookbookRecipes.find(r=>r.recipeId === this.currentRecipe.id);
           if(recipeInCookbook !== undefined) {
              this.canAddToCookbook = false;
           }
        }
      }
    }, error => {      
      console.log(error);
      this.router.navigateByUrl('/recipes');    
      this.toastr.error('An error occurred, please try again.');       
    })
  }

  loadShoppingListsIds(){
    // Potrzebne do modala, user moze miec kilka list wiec trzeba wybrać 
    this.userService.getUserShoppingLists(this.user.id).subscribe(userShoppingLists => {
      if(userShoppingLists?.length !== undefined){
        this.userShoppingLists = userShoppingLists; 
      }             
    }, error => {
      console.log(error);
      this.router.navigateByUrl('/recipes');    
      this.toastr.error('An error occurred, please try again.');   
    })
  }

  editThisRecipe()  {
    this.editRecipe = true;
  }

  addToCookbook(){
    if(this.userCookbook === undefined){
        this.cookbookRecipe = {recipeId: this.currentRecipe.id, userId: this.user.id, cookbookId: 0, recipe: this.currentRecipe, isRecipeAdded: true}
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
    this.cookbookRecipe = {recipeId: this.currentRecipe.id, userId: this.user.id, cookbookId: this.userCookbook.id, recipe: this.currentRecipe, isRecipeAdded: true}
    this.userCookbook.cookbookRecipes.push(this.cookbookRecipe);
    this.cookbookService.updateCookbook(this.userCookbook.id, this.cookbookRecipe)
      .subscribe(response =>{
        if(response.status === 200 ){ 
          this.toastr.success('Recipe added successfully!');          
        } else {
          this.toastr.error('Error! Recipe cannot be added.');
          console.log(response);
        }   
        this.canAddToCookbook = false;
      }, error => {
        this.toastr.error('Error! Recipe cannot be added.');
        console.log(error);
      })
    }
  }

  removeFromCookbook(){
    this.cookbookRecipe = {recipeId: this.currentRecipe.id, userId: this.user.id, cookbookId: this.userCookbook.id, recipe: this.currentRecipe, isRecipeAdded: false}
    
    this.cookbookService.updateCookbook(this.userCookbook.id, this.cookbookRecipe)
    .subscribe(response => {
      if(response.status === 200 ){ 
        this.toastr.success('Recipe removed successfully!');          
      } else {
        this.toastr.error('Error! Recipe cannot be removed.');
        console.log(response);
      }        
      this.canAddToCookbook = true;
    }, error => {
      this.toastr.error('Error! Recipe cannot be removed.');
      console.log(error);
    })
  }

  modalAddToShoppingList() {
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

  deleteRecipe(){//TODO! NIE MA SPRAWDZENIA CZY MOZNA USUNAC PRZEPIS - JESLI ISTNIEJE W CZYJEJ KSIAZCE TO POWINNAM MIEC MOZLIWO USUNIECIA CZY NIE?
    //NA TEN MOMENT MOGÉ USUNAC PRZEPIS SWÓJ JHELI MAM TEZ GO W SWOJEJ KSIAZCE
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

  editPhotos(){
    this.router.navigateByUrl('/recipes/'+ this.currentRecipe.id +'/photos');
  }

  rateRecipe(){ //todo czy to jest potrzebne
  }

  rate(rating: number){
    this.rateModel = {recipeId: this.currentRecipe.id, userId: this.user.id, value: rating, id: null};
    this.recipeService.addRate(this.rateModel).subscribe(response => {
      this.loadRecipe();
      if(response.status === 200 ){         
        this.toastr.success('Recipe assessed successfully');
      } else {
        this.toastr.error('Error! Rate cannot be added.');
        console.log(response);
      }  
    }, error => {
      this.toastr.error('Error! Rate cannot be added.');
      console.log(error);
    })
  }

  deleteVote(){
    this.recipeService.deleteRate(this.rateModel.id).subscribe(response => {
      this.loadRecipe();
      if(response.status === 200 ){ 
        this.toastr.success('Vote removed successfully');        
      } else {
        this.toastr.error('Error! Rate cannot be removed.');
        console.log(response);
      }          
    }, error => {
      this.toastr.error('Error! Rate cannot be removed.');
      console.log(error);
    })
  }
}
