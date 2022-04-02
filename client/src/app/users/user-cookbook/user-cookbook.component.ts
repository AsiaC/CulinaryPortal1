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
    this.recipeService.getCategories().subscribe(allCategoriesResponse => {
      if(allCategoriesResponse?.length !== undefined){
        this.allCategories = allCategoriesResponse;
      } else {        
        if(allCategoriesResponse.error.status === 401){
          this.toastr.error('You do not have access to this content.');  
        } else if(allCategoriesResponse.error.status === 404){
          this.toastr.error('No categories found.');          
        } else {               
          this.toastr.error('An error occurred, please try again.');  
        }
      }
    }, error =>{
      console.log(error);
      this.toastr.error('An error occurred, please try again.');  
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
    var dict = {};
    if(this.searchByName !== null){
      dict["name"] = this.searchByName;
    }
    if(this.selectOptionVal !== null && this.selectOptionVal !== undefined){
      dict["categoryId"] = Number(this.selectOptionVal);
    }
    if(this.selectedDifficultyLevel !== null && this.selectedDifficultyLevel !== undefined){
      dict["difficultyLevelId"] = Number(this.selectedDifficultyLevel);
    }
    if(this.selectedPreparationTime !== null && this.selectedPreparationTime !== undefined){
      dict["preparationTimeId"] = Number(this.selectedPreparationTime);
    }
    if(this.user.id !== null || this.user.id !== undefined){
      dict["userId"] = this.user.id;
    }

    this.userService.searchUserCookbookRecipes(dict, this.user.id).subscribe(recipesResponse => {
      if(recipesResponse?.length !== undefined){
        this.userFavouriteRecipes = recipesResponse;  
        this.toastr.success('Recipes filtered.');  
        if(recipesResponse?.length >0){
          this.isNoResults = false; 
        } else {
          this.isNoResults = true;
        }
      } else {                      
        this.toastr.error('An error occurred, please try again.');  
        console.log(recipesResponse);
      }
    }, error => {
        console.log(error);   
        this.isNoResults = true;    
        this.toastr.error('An error occurred, please try again.');               
    })
  }

  clearSearch(){
    this.loadUserCookbook();
    this.isNoResults = false;        
  }  
}
