import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RecipesService } from 'src/app/_services/recipes.service';
import {Category} from 'src/app/_models/category';
import { DifficultyLevelEnum } from 'src/app/_models/difficultyLevelEnum';
import { FormGroup, FormControl } from "@angular/forms";
import { PreparationTimeEnum } from 'src/app/_models/preparationTimeEnum';
import { Console } from 'console';
//import { Ingredient } from 'src/app/_models/ingredient';

@Component({
  selector: 'app-recipe-new-form',
  templateUrl: './recipe-new-form.component.html',
  styleUrls: ['./recipe-new-form.component.css']
})
export class RecipeNewFormComponent implements OnInit {

  model: any = {};
  allCategories: Category[];
  difficultyLevel = DifficultyLevelEnum;
  enumKeys = [];
  preparationTime = PreparationTimeEnum;
  preparationTimeKeys = [];
  submitted = false;
 // firstPartFormSubmitted = false;
 // allIngredients:Ingredient[];

  constructor(private recipesService: RecipesService) { 
    //this.enumKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k)));
    //this.enumKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(Number);
    this.enumKeys = Object.keys(this.difficultyLevel);
    this.preparationTimeKeys = Object.keys(this.preparationTime);
  }

  ngOnInit(): void {//debugger;
    this.getAllCategories();
   
  }

  getAllCategories(){//debugger;
    this.recipesService.getCategories().subscribe(allCategories => {
      this.allCategories = allCategories;
      //debugger;
      //console.log(allCategories);
    }, error =>{
      console.log(error);
    })
  }

  createNewRecipe(){
    debugger;
    console.log("create new recipe");
    console.log(this.model);
    console.log(this.submitted);
    this.submitted = true;
    console.log(this.submitted);
    
    // this.recipesService.addRecipe(this.model).subscribe(response => {
    //   console.log(response);
    //   this.cancel();  
    // })
  }

  //pod przyciskiem cancel
  cancel(){
    console.log("cancel");
    //debugger;
    //this.cancelRegister.emit(false);
  }

}
