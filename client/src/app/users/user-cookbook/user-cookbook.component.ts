import { Component, OnInit } from '@angular/core';
import { Cookbook } from 'src/app/_models/cookbook';
import { UsersService } from 'src/app/_services/users.service';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { Recipe } from 'src/app/_models/recipe';
import { CookbookService } from 'src/app/_services/cookbook.service';
import { DifficultyLevelEnum } from 'src/app/_models/difficultyLevelEnum';
import { PreparationTimeEnum } from 'src/app/_models/preparationTimeEnum';
import { ToastrService } from 'ngx-toastr';
import { SearchRecipe } from 'src/app/_models/searchRecipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { CookbookRecipe } from 'src/app/_models/cookbookRecipe';
import { Category } from 'src/app/_models/category';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-cookbook',
  templateUrl: './user-cookbook.component.html',
  styleUrls: ['./user-cookbook.component.css']
})
export class UserCookbookComponent implements OnInit {
  userCookbook: Cookbook;
  user: User;
  cookbookRecipe: CookbookRecipe;
  userFavouriteRecipes: Recipe[];
  searchByName: string = null; 
  selectOptionVal: any;
  allCategories: Category[] = [];
  difficultyLevel = DifficultyLevelEnum;
  difficultyLevelKeys = [];
  preparationTime = PreparationTimeEnum;
  preparationTimeKeys = [];
  searchModel : SearchRecipe = null;
  isNoResults: boolean = false;
  selectedDifficultyLevel: any;
  selectedPreparationTime: any;
  PreparationTimeEnum = PreparationTimeEnum;
  DifficultyLevelEnum = DifficultyLevelEnum;
  alertText: string;

  constructor(private userService:UsersService, private accountService:AccountService,  private cookbookService:CookbookService, private toastr: ToastrService, private recipeService: RecipesService, private router: Router) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    this.difficultyLevelKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(Number);
    this.preparationTimeKeys = Object.keys(this.preparationTime).filter(k => !isNaN(Number(k))).map(Number);       
  }

  ngOnInit(): void {
    this.loadUserCookbook();
    this.getAllCategories();
  }

  loadUserCookbook(){
    this.userService.getUserCookbook(this.user.id).subscribe(userCookbookResponse => {
      if(userCookbookResponse.cookbookRecipes !== undefined){
        this.userCookbook = userCookbookResponse;
        if(userCookbookResponse?.cookbookRecipes.length > 0){
          this.userFavouriteRecipes = userCookbookResponse.cookbookRecipes.map(x=>x.recipe);
        } else {
          this.userFavouriteRecipes = undefined;
          this.alertText = "User does not have any favourite recipes yet."
        }
      } else {  
          if(userCookbookResponse.error.status === 404){        
            this.alertText = "User does not have a cookbook yet."
          } else if(userCookbookResponse.status === 401){        
            this.alertText = "You do not have access to this content.";
          } else {   
            this.router.navigateByUrl('/recipes');     
            this.alertText = 'An error occurred, please try again.';
          }
        }
    }, error => {
      console.log(error);
      this.router.navigateByUrl('/recipes');     
      this.alertText = 'An error occurred, please try again.';
    })
  }

  getAllCategories(){
    this.recipeService.getCategories().subscribe(allCategories => {
      this.allCategories = allCategories;
    }, error =>{
      console.log(error);
    })
  }

  removeFromCookbook(recipeId){
    this.cookbookRecipe = {recipeId: recipeId, userId: this.user.id, cookbookId: this.userCookbook.id, recipe: null, isRecipeAdded: false}
    
    this.cookbookService.updateCookbook(this.userCookbook.id, this.cookbookRecipe)
    .subscribe(response => {
      if(response.status === 200 ){ 
        this.toastr.success('Recipe removed successfully!');          
      } else {
        this.toastr.error('Error! Recipe cannot be removed.');
        console.log(response);
      }      
      this.loadUserCookbook();
    }, error => {
      this.toastr.error('Error! Recipe cannot be removed.');
      console.log(error);
    })
  }

  deleteCookbook(cookbookId: number){
    this.cookbookService.deleteCookbook(cookbookId).subscribe(response => {
      this.loadUserCookbook(); 
      if(response.status === 200){
        this.userCookbook = undefined;
        this.toastr.success('Cookbook removed successfully!');        
      } else {
        this.toastr.error('Error! Cookbook cannot be removed.'); 
      }
    }, error => {
      this.toastr.error('Error! Cookbook cannot be removed.'); 
      console.log(error);                      
    })
  }

  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }
  onChange(event): number {
    return event;
  }

  searchRecipes(){   
    this.searchModel = {name: this.searchByName, categoryId: Number(this.selectOptionVal), difficultyLevelId: Number(this.selectedDifficultyLevel), preparationTimeId: Number(this.selectedPreparationTime), userId: this.user.id, top: null}

    this.userService.searchUserCookbookRecipes(this.searchModel, this.user.id)
    .subscribe(response => {
      this.isNoResults = false; 
      this.userFavouriteRecipes = response;    
      //this.userFavouriteRecipes = response.cookbookRecipes.map(x=>x.recipe);  
      this.toastr.success('Recipes filtered.');  
      }, error => {
        console.log(error);   
        this.isNoResults = true;                   
    })
  }

  clearSearch(){
    this.loadUserCookbook();
    this.isNoResults = false;        
  }  
}
