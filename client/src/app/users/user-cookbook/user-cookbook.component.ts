import { Component, OnInit } from '@angular/core';
import { Cookbook } from 'src/app/_models/cookbook';
import { UsersService } from 'src/app/_services/users.service';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { Recipe } from 'src/app/_models/recipe';
import { CookbookService } from 'src/app/_services/cookbook.service';
import { DecimalPipe } from '@angular/common';
import { FormControl } from '@angular/forms';
import { DifficultyLevelEnum } from 'src/app/_models/difficultyLevelEnum';
import { PreparationTimeEnum } from 'src/app/_models/preparationTimeEnum';
import { ToastrService } from 'ngx-toastr';
import { SearchRecipe } from 'src/app/_models/searchRecipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { CookbookRecipe } from 'src/app/_models/cookbookRecipe';

@Component({
  selector: 'app-user-cookbook',
  templateUrl: './user-cookbook.component.html',
  styleUrls: ['./user-cookbook.component.css']
})
export class UserCookbookComponent implements OnInit {
  userCookbook: Cookbook;
  user: User;
  //cookbookRecipe: any = {recipeId: null, userId: null};  
  cookbookRecipe: CookbookRecipe;
  userFavouriteRecipes: Recipe[];
  searchByName: string = null; 
  selectOptionVal: any;
  allCategories: any[] = [];
  difficultyLevel = DifficultyLevelEnum;
  difficultyLevelKeys = [];
  preparationTime = PreparationTimeEnum;
  preparationTimeKeys = [];
  searchModel : SearchRecipe = null;
  isNoResults: boolean = false;
  selectedDifficultyLevel: any;
  selectedPreparationTime: any;

  constructor(private userService:UsersService, private accountService:AccountService,  private cookbookService:CookbookService, private toastr: ToastrService, private recipeService: RecipesService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    this.difficultyLevelKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(Number);
    this.preparationTimeKeys = Object.keys(this.preparationTime).filter(k => !isNaN(Number(k))).map(Number);       
  }

  ngOnInit(): void {
    this.loadUserCookbook();
    this.getAllCategories();
  }

  loadUserCookbook(){
    this.userService.getUserCookbook(this.user.id).subscribe(userCookbook => {
      this.userCookbook = userCookbook;
      if(userCookbook.cookbookRecipes.length > 0){
      this.userFavouriteRecipes = userCookbook.cookbookRecipes.map(x=>x.recipe);
      } else {
        this.userFavouriteRecipes = undefined;
      }
      console.log(this.userFavouriteRecipes);
    }, error => {
      if(error.status === 404){
        this.userCookbook = undefined;
        this.userFavouriteRecipes = undefined;
      } 
      console.log(error);
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
    //this.cookbookRecipe.recipeId = recipeId;
    //this.cookbookRecipe.userId = this.user.id;
    //TODO note ponizej chyba do usuniecia?
    this.cookbookRecipe = {recipeId: recipeId, userId: this.user.id, cookbookId: this.userCookbook.id, note: null, recipe: null}
    
    this.cookbookService.removeRecipeFromCookbook(this.userCookbook.id, this.cookbookRecipe)
    .subscribe(response => {
      console.log("success");
      this.toastr.success('Recipe removed successfully!');
      this.loadUserCookbook();
    }, error => {
        console.log(error);
    })
  }

  deleteCookbook(cookbookId: number){
    this.cookbookService.deleteCookbook(cookbookId)
    .subscribe(response => {
      console.log(response);
      this.toastr.success('Cookbook removed successfully!');  
      this.loadUserCookbook(); 
    }, error => {
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
    this.searchModel = {name: this.searchByName, categoryId: Number(this.selectOptionVal), difficultyLevelId: Number(this.selectedDifficultyLevel), preparationTimeId: Number(this.selectedPreparationTime), userId: this.user.id}

    this.userService.searchUserCookbook(this.searchModel, this.user.id)
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
