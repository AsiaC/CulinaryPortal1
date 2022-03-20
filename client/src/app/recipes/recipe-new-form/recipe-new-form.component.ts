import { Component, Input, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RecipesService } from 'src/app/_services/recipes.service';
import {Category} from 'src/app/_models/category';
import { DifficultyLevelEnum } from 'src/app/_models/difficultyLevelEnum';
import { PreparationTimeEnum } from 'src/app/_models/preparationTimeEnum';
import { Ingredient } from 'src/app/_models/ingredient';
import { Measure } from 'src/app/_models/measure';
import { FormGroup, FormControl,FormArray, FormBuilder, Validators, NgForm } from '@angular/forms'
import { Recipe } from 'src/app/_models/recipe';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { first, take } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { CreateIngredientComponent } from 'src/app/modals/create-ingredient/create-ingredient.component';

@Component({
  selector: 'app-recipe-new-form',
  templateUrl: './recipe-new-form.component.html',
  styleUrls: ['./recipe-new-form.component.css']
})
export class RecipeNewFormComponent implements OnInit {
  allCategories: Category[];
  difficultyLevel = DifficultyLevelEnum;
  difficultyLevelKeys = [];
  preparationTime = PreparationTimeEnum;
  preparationTimeKeys = [];
  submitted = false;
  allIngredients: Ingredient[];
  allMeasures: Measure[];

  addRecipeForm: FormGroup;
  user: User; //current
  recipe: Recipe;
  isAddMode: boolean;
  id: string;

  bsModalRef: BsModalRef;
  itemIsRemoved: boolean = false;
  
  constructor(private recipesService: RecipesService, private fb:FormBuilder, private accountService:AccountService, private route: ActivatedRoute, private router: Router, private modalService: BsModalService) { 
    this.difficultyLevelKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(Number);
    this.preparationTimeKeys = Object.keys(this.preparationTime).filter(k => !isNaN(Number(k))).map(Number);   
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.isAddMode = !this.id;
    
    this.getAllCategories();
    this.getAllIngredients();
    this.getAllMeasures();
    this.initializeForm();
    if(!this.isAddMode)
    {
      this.loadRecipe();
    }    
  }
  loadRecipe(){
    this.recipesService.getRecipe(Number(this.route.snapshot.paramMap.get('id')))
    .pipe(first())
    .subscribe(recipe =>{      
      this.recipe = recipe; 
      this.addRecipeForm.patchValue({
        id: recipe.id,
        name: recipe.name,
        description: recipe.description,
        difficultyLevel: recipe.difficultyLevel,
        preparationTime: recipe.preparationTime,
        categoryId: recipe.categoryId,
        userId: recipe.userId
      });
      var instructionsArray = [];
      this.recipe.instructions.forEach(instruction => instructionsArray.push(this.fb.group({
        name: instruction.name,
        description: instruction.description,
        id: instruction.id,
        step: instruction.step
      })));
      this.addRecipeForm.setControl('instructions', this.fb.array(instructionsArray || []));
      
      var recipeIngredientsArray = [];
      this.recipe.recipeIngredients.forEach(recipeIngredient => recipeIngredientsArray.push(this.fb.group({
        quantity: recipeIngredient.quantity,
        measureId: recipeIngredient.measure.id, 
        ingredientId: recipeIngredient.ingredient.id,    
      })));
      this.addRecipeForm.setControl('recipeIngredients', this.fb.array(recipeIngredientsArray || []));
    }, error => {
      console.log(error);
    });
  }
  
  initializeForm(){
    this.addRecipeForm = this.fb.group({
      id: [],
      name: ['', [Validators.required]],
      description: [],
      difficultyLevel:[null, [Validators.required]],
      preparationTime:[null, [Validators.required]],
      categoryId: [null, [Validators.required]],
      recipeIngredients:this.fb.array([this.createIngrFormGroup()], [Validators.required]),
      instructions: this.fb.array([this.createInstrFormGroup()], [Validators.required]),
      userId: [this.user.id]
    });
  }

  createIngrFormGroup(){
    return this.fb.group({
      quantity: ['', [Validators.required, Validators.min(0)]],
      ingredientId: [null, [Validators.required, Validators.min(1)]],
      measureId: [null, [Validators.required]],
    })
  }

  get recipeIngredients() : FormArray {
    return this.addRecipeForm.get("recipeIngredients") as FormArray
  }

  addIngredients() {
    let fg = this.createIngrFormGroup();
    this.recipeIngredients.push(fg);
  } 
  removeIngredient(i:number) {
    this.recipeIngredients.removeAt(i);
    this.itemIsRemoved = true;
  }

  createInstrFormGroup(){
    return this.fb.group({
      //step: '',
      name: '',
      description: ['', [Validators.required]]      
    })
  }

  get instructions() : FormArray {
    return this.addRecipeForm.get("instructions") as FormArray
  } 

  addInstructions() {
    let fb = this.createInstrFormGroup();
    this.instructions.push(fb);
  }

  removeInstruction(i:number) {
    this.instructions.removeAt(i);
    this.itemIsRemoved = true;
  }

  getAllCategories(){
    this.recipesService.getCategories().subscribe(allCategories => {
      this.allCategories = allCategories;
    }, error =>{
      console.log(error);
    })
  }

  getAllIngredients(){
    this.recipesService.getIngredients().subscribe(allIngredients => {
      this.allIngredients = allIngredients;
    }, error =>{
      console.log(error);
    })
  }  

  getAllMeasures(){
    this.recipesService.getMeasures().subscribe(allMeasures => {
      this.allMeasures = allMeasures;
    }, error =>{
      console.log(error);
    })
  }

  createNewRecipe(){
    this.submitted = true;    
    this.recipesService.addRecipe(this.addRecipeForm.value).subscribe(response => {
      this.isAddMode = false;
      window.location.reload();
    }, error => {
      console.log(error);
    })

  }

  onSubmit() { 
    var recipeIngredientsArray = [];
      this.addRecipeForm.value.recipeIngredients.forEach(recipeIngredient => recipeIngredientsArray.push(this.fb.group({
        quantity: recipeIngredient.quantity,        
        measureId: recipeIngredient.measureId,        
        ingredientId: recipeIngredient.ingredientId,         
      })));
      this.addRecipeForm.setControl('recipeIngredients', this.fb.array(recipeIngredientsArray || []));
    if(this.isAddMode){
      this.createNewRecipe();
    }
    else{
      this.updateRecipe();
    }  
  }

  private updateRecipe() {
    
    this.recipesService.updateRecipe(Number(this.id), this.addRecipeForm.value).subscribe(response => {
      
        //this.toastr.success('Profile updated successfully');
        this.recipe=this.addRecipeForm.value;
        this.isAddMode = false;
        this.addRecipeForm.reset(this.recipe);
        window.location.reload();
      }, error => {
          console.log(error);                      
      })    
  }

  cancel(){
    //console.log("cancel");
    //this.cancelRegister.emit(false);
    //this.router.navigateByUrl('/user/recipes');
    //REFRESH PAGE OR addNewMode=FALSE ALE TO JEST Z componentu rodzica wiec trzebaby przekazać do rodzica
    window.location.reload();
  }
  changeOnIgredient(recipeIngredient){
    var selectedIngredientId = recipeIngredient.value.ingredientId;    
    console.log(this.addRecipeForm.status);

    if(selectedIngredientId === 0){
      console.log(this.addRecipeForm.status);
      const initialState = {};
      this.bsModalRef = this.modalService.show(CreateIngredientComponent, {initialState});

      this.bsModalRef.content.createNewIngredient.subscribe(value => {
        var newIngredientName = value;
        if(newIngredientName !== null){
          var newIngredientToAdd: Ingredient = {id: null, name: newIngredientName};
          this.recipesService.addIngredient(newIngredientToAdd).subscribe(response => {            
            if(response.id !== null){
              this.allIngredients.push(response);
            }            
            this.recipe=this.addRecipeForm.value;
            this.recipe.recipeIngredients.forEach(element => {
              if(element.ingredientId === 0){
                element.ingredientId = response.id;
              }
            });

            var recipeIngredientsArray = [];
            this.recipe.recipeIngredients.forEach(recipeIngredient => recipeIngredientsArray.push(this.fb.group({
              quantity: recipeIngredient.quantity,
              measureId: recipeIngredient.measureId,
              ingredientId: recipeIngredient.ingredientId, 
            })));
            this.addRecipeForm.setControl('recipeIngredients', this.fb.array(recipeIngredientsArray || []));

          })
        }
      });
    }
  }
}