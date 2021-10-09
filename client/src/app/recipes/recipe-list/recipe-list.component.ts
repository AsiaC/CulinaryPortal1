import { Component, OnInit } from '@angular/core';
import { Recipe } from 'src/app/_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { DifficultyLevelEnum } from 'src/app/_models/difficultyLevelEnum';
import { PreparationTimeEnum } from 'src/app/_models/preparationTimeEnum';
import { ToastrService } from 'ngx-toastr';
import { SearchRecipe } from 'src/app/_models/searchRecipe';

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
  allCategories: any[] = [];
  difficultyLevel = DifficultyLevelEnum;
  difficultyLevelKeys = [];
  preparationTime = PreparationTimeEnum;
  preparationTimeKeys = [];
  searchModel : SearchRecipe = null;
  isNoResults: boolean = false;
  selectedDifficultyLevel: any;
  selectedPreparationTime: any;
  title: string = "All recipes";

  constructor(private recipeService: RecipesService, private toastr: ToastrService) { 
    this.difficultyLevelKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(Number);
    this.preparationTimeKeys = Object.keys(this.preparationTime).filter(k => !isNaN(Number(k))).map(Number);   
    
  }

  ngOnInit(): void {
    this.loadRecipes();
    this.getAllCategories();
  }

  loadRecipes(){
    this.recipeService.getRecipes().subscribe(recipes => {
      this.recipes = recipes;
      this.title = "All recipes";
    }, error => {
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

  searchRecipes(){ 
    console.log(this.searchByName);
    console.log(this.selectOptionVal);
    console.log(this.selectedDifficultyLevel);
    console.log(this.selectedPreparationTime);


    this.searchModel = {name: this.searchByName, categoryId: Number(this.selectOptionVal), difficultyLevelId: Number(this.selectedDifficultyLevel), preparationTimeId: Number(this.selectedPreparationTime), userId: null, top: null}

    this.recipeService.searchRecipes(this.searchModel)
    .subscribe(response => {
      this.isNoResults = false; 
      this.title = "Filtered recipes";
      this.recipes = response;
      this.toastr.success('Recipes filtered.');  
      }, error => {
        console.log(error);   
        this.isNoResults = true;                   
    })
  }
  clearSearch(){
    this.loadRecipes();
    this.isNoResults = false;        
  }
  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }
  onChange(event): number {
    return event;
  }

}
