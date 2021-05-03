import { Component, Input, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RecipesService } from 'src/app/_services/recipes.service';
import {Category} from 'src/app/_models/category';
import { DifficultyLevelEnum } from 'src/app/_models/difficultyLevelEnum';
import { PreparationTimeEnum } from 'src/app/_models/preparationTimeEnum';
import { Console } from 'console';
import { Ingredient } from 'src/app/_models/ingredient';
import { Measure } from 'src/app/_models/measure';
import { FormGroup, FormControl,FormArray, FormBuilder, Validators } from '@angular/forms'
import { Recipe } from 'src/app/_models/recipe';

@Component({
  selector: 'app-recipe-new-form',
  templateUrl: './recipe-new-form.component.html',
  styleUrls: ['./recipe-new-form.component.css']
})
export class RecipeNewFormComponent implements OnInit {
  allCategories: Category[];
  difficultyLevel = DifficultyLevelEnum;
  enumKeys = [];
  preparationTime = PreparationTimeEnum;
  preparationTimeKeys = [];
  submitted = false;
 // firstPartFormSubmitted = false;
  allIngredients: Ingredient[];
  allMeasures: Measure[];

  addRecipeForm: FormGroup;
  
  constructor(private recipesService: RecipesService, private fb:FormBuilder) { 
    //this.enumKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k)));
    this.enumKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(Number);
    //this.enumKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(key => ({ title: this.difficultyLevel[key], value: key }));
    //this.enumKeys = Object.keys(this.difficultyLevel);
    //this.preparationTimeKeys = Object.keys(this.preparationTime);   
    this.preparationTimeKeys = Object.keys(this.preparationTime).filter(k => !isNaN(Number(k))).map(Number);   
  }

  ngOnInit(): void {//debugger;
    this.getAllCategories();
    this.getAllIngredients();
    this.getAllMeasures();
    this.initializeForm();
  }

  
  initializeForm(){
    this.addRecipeForm = this.fb.group({
      // name: new FormControl(),
      // description: new FormControl(),
      name: ['',Validators.required],
      description: [],
      difficultyLevel:[],
      //difficultyLevel:this.fb.array([]),
      preparationTime:[],
      categoryId: [],
      //category: [],
      //ingredients: this.fb.array([]),
      recipeIngredients:this.fb.array([]),
      instructions: this.fb.array([]),
    });
  }

  get recipeIngredients() : FormArray {
    return this.addRecipeForm.get("recipeIngredients") as FormArray
  }
 
  newIngredient(): FormGroup {
    return this.fb.group({
      quantity: '',
      ingredientId: [],
      measureId: []
    })
  } 
  addIngredients() {
    this.recipeIngredients.push(this.newIngredient());
  } 
  removeIngredient(i:number) {
    this.recipeIngredients.removeAt(i);
  }

  get instructions() : FormArray {
    return this.addRecipeForm.get("instructions") as FormArray
  }
 
  newInstructions(): FormGroup {
    return this.fb.group({
      //step: '',
      name: '',
      description: ''      
    })
  } 
  addInstructions() {
    this.instructions.push(this.newInstructions());
  } 
  removeInstruction(i:number) {
    this.instructions.removeAt(i);
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

  getAllIngredients(){//debugger;
    this.recipesService.getIngredients().subscribe(allIngredients => {
      this.allIngredients = allIngredients;
      //debugger;
      //console.log(allCategories);
    }, error =>{
      console.log(error);
    })
  }  

  getAllMeasures(){//debugger;
    this.recipesService.getMeasures().subscribe(allMeasures => {
      this.allMeasures = allMeasures;
      //debugger;
      //console.log(allCategories);
    }, error =>{
      console.log(error);
    })
  }

  createNewRecipe0(){
    debugger;
    console.log("create new recipe");
    console.log(this.addRecipeForm.value);
    console.log(this.submitted);
    this.submitted = true;
    console.log(this.submitted);
    console.log(this.addRecipeForm.value);  
    //let newUser: User = this.userForm.value;    
  }

  createNewRecipe() {
    console.log(this.addRecipeForm.value);
    debugger;
    this.recipesService.addRecipe(this.addRecipeForm.value).subscribe(response => {
      console.log(response);
      //this.router.navigateByUrl('/members');
    }, error => {
      //this.validationErrors = error;
      console.log(error);
    })
  }

  //pod przyciskiem cancel
  cancel(){
    console.log("cancel");
    //debugger;
    //this.cancelRegister.emit(false);
  }

}
