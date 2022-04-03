import { Component, OnInit } from '@angular/core';
import { Recipe } from 'src/app/_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { DifficultyLevelEnum } from 'src/app/_models/difficultyLevelEnum';
import { PreparationTimeEnum } from 'src/app/_models/preparationTimeEnum';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/_models/category';

@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.css']
})
export class RecipeListComponent implements OnInit {
  //property to store recipes
  recipes: Recipe[];
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
  title: string = "All recipes";

  selectedCategory = null;

  constructor(private recipeService: RecipesService, private toastr: ToastrService) { 
    this.difficultyLevelKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(Number);
    this.preparationTimeKeys = Object.keys(this.preparationTime).filter(k => !isNaN(Number(k))).map(Number);   
    
  }

  ngOnInit(): void {
    this.loadRecipes();
    this.getAllCategories();
  }

  loadRecipes(){
    this.recipeService.getRecipes().subscribe(recipesResponse => {
      if(recipesResponse?.length !== undefined){
        this.recipes = recipesResponse;  
        this.title = "All recipes";
      } else {        
        if(recipesResponse.error.status === 401){
          this.toastr.error('You do not have access to this content.');  
        } else if(recipesResponse.error.status === 404){
          this.toastr.error('No recipes yet.');          
        } else {               
          this.toastr.error('An error occurred, please try again.');  
        }
      }
    }, error =>{
      console.log(error);       
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
    
    this.recipeService.searchRecipes(dict).subscribe(recipesResponse => {
      if(recipesResponse?.length !== undefined){
        this.title = "Filtered recipes";
        this.recipes = recipesResponse;
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
    this.loadRecipes();
    this.isNoResults = false;
    this.selectOptionVal = null;
    this.selectedDifficultyLevel = null;
    this.selectedPreparationTime = null;
    this.searchByName = null;
    this.toastr.success('Filter removed.');  
  }
  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }
  onChange(event): number {
    return event;
  }

}
