import { Component, OnInit } from '@angular/core';
import { Recipe } from 'src/app/_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { UsersService } from 'src/app/_services/users.service';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { DifficultyLevelEnum } from 'src/app/_models/difficultyLevelEnum';
import { PreparationTimeEnum } from 'src/app/_models/preparationTimeEnum';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-recipes',
  templateUrl: './user-recipes.component.html',
  styleUrls: ['./user-recipes.component.css']
})
export class UserRecipesComponent implements OnInit {
  userRecipes: Recipe[];
  user: User;
  addNewMode:boolean = false;
  searchByName: string = null; 
  selectOptionVal: any;
  allCategories: any[] = [];
  difficultyLevel = DifficultyLevelEnum;
  difficultyLevelKeys = [];
  preparationTime = PreparationTimeEnum;
  preparationTimeKeys = [];  
  isNoResults: boolean = false;
  selectedDifficultyLevel: any;
  selectedPreparationTime: any;

  constructor(private userService:UsersService, private accountService:AccountService, private toastr: ToastrService, private recipeService: RecipesService, private router: Router) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    this.difficultyLevelKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(Number);
    this.preparationTimeKeys = Object.keys(this.preparationTime).filter(k => !isNaN(Number(k))).map(Number);       
  }

  ngOnInit(): void {
    this.loadUserRecipes();
    this.getAllCategories();
  }

  loadUserRecipes(){
    this.userService.getUserRecipes(this.user.id).subscribe(userRecipesResponse=>{
      if(userRecipesResponse?.length !== undefined){
        this.userRecipes = userRecipesResponse;  
      } else {        
        if(userRecipesResponse.error.status === 401){
          this.toastr.error('You do not have access to this content.');  
        } else if(userRecipesResponse.error.status === 404){
          this.toastr.error('You do not have any recipes yet.');          
        } else {
          this.router.navigateByUrl('/recipes');     
          this.toastr.error('An error occurred, please try again.');  
        }
      }
    }, error =>{
      console.log(error);
      this.router.navigateByUrl('/recipes');     
      this.toastr.error('An error occurred, please try again.');  
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

  addRecipe(){
    this.addNewMode = !this.addNewMode;
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

    this.userService.searchUserRecipes(dict, this.user.id).subscribe(recipesResponse => {
      if(recipesResponse?.length !== undefined){
        this.userRecipes = recipesResponse;
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
    this.loadUserRecipes();
    this.isNoResults = false;        
  }
  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }
  onChange(event): number {
    return event;
  }

}
