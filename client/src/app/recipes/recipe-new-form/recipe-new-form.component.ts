import { Component, Input, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RecipesService } from 'src/app/_services/recipes.service';
import {Category} from 'src/app/_models/category';
import { DifficultyLevelEnum } from 'src/app/_models/difficultyLevelEnum';
import { PreparationTimeEnum } from 'src/app/_models/preparationTimeEnum';
import { Console } from 'console';
import { Ingredient } from 'src/app/_models/ingredient';
import { Measure } from 'src/app/_models/measure';
import { FormGroup, FormControl,FormArray, FormBuilder, Validators, NgForm } from '@angular/forms'
import { Recipe } from 'src/app/_models/recipe';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { first, take } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-recipe-new-form',
  templateUrl: './recipe-new-form.component.html',
  styleUrls: ['./recipe-new-form.component.css']
})
export class RecipeNewFormComponent implements OnInit {
 // @ViewChild('editForm') editForm: NgForm;
  allCategories: Category[];
  difficultyLevel = DifficultyLevelEnum;
  difficultyLevelKeys = [];
  preparationTime = PreparationTimeEnum;
  preparationTimeKeys = [];
  submitted = false;
 // firstPartFormSubmitted = false;
  allIngredients: Ingredient[];
  allMeasures: Measure[];

  addRecipeForm: FormGroup;
  user: User; //current
  recipe: Recipe;
  isAddMode: boolean;
  id: string;
  
  constructor(private recipesService: RecipesService, private fb:FormBuilder, private accountService:AccountService, private route: ActivatedRoute, private router: Router) { 
    //this.enumKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k)));
    this.difficultyLevelKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(Number);
    //this.enumKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(key => ({ title: this.difficultyLevel[key], value: key }));
    //this.enumKeys = Object.keys(this.difficultyLevel);
    //this.preparationTimeKeys = Object.keys(this.preparationTime);   
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
      
      //this.addRecipeForm.setControl('instructions', this.fb.array(this.recipe.instructions || []));
      //this.addRecipeForm.setControl('recipeIngredients', this.fb.array(this.recipe.recipeIngredients || []));      

      var recipeIngredientsArray = [];
      this.recipe.recipeIngredients.forEach(recipeIngredient => recipeIngredientsArray.push(this.fb.group({
        quantity: recipeIngredient.quantity,
        //measureId: recipeIngredient.measureId,
        measureId: recipeIngredient.measure.id,
        //ingredientId: recipeIngredient.ingredientId,  
        ingredientId: recipeIngredient.ingredient.id, 
        //measure: recipeIngredient.measure, //OST ZAKOMENTOWANE
        //measure: this.allMeasures.find
        //ingredient: recipeIngredient.ingredient, //OST ZAKOMENTOWANE
        //measureName: recipeIngredient.measureName,  
        //ingredientName: recipeIngredient.ingredientName,    
          
      })));
      this.addRecipeForm.setControl('recipeIngredients', this.fb.array(recipeIngredientsArray || []));
    }, error => {
      console.log(error);
    });
    // this.recipesService.getRecipe(Number(this.id))
    //             .pipe(first())
    //             .subscribe(x => this.addRecipeForm.patchValue(x));

    // this.recipesService.getRecipe(Number(this.id))
    //             .pipe(first())
    //             .subscribe(x => {
    //               this.addRecipeForm.patchValue(x)
    //             });

                // this.userService.editUserBlog(id).pipe(first()).subscribe(user => {
                //   user = user;
                //   this.editBlogForm.setValue({
                //     title: user["0"].title,
                //     blog: user["0"].blog,
                //   });
                // });
  }
  
  initializeForm(){
    this.addRecipeForm = this.fb.group({
      // name: new FormControl(),
      // description: new FormControl(),
      id: [],
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
      //userId: [{value: this.user.id}]
      userId: [this.user.id]
    });
  }

  get recipeIngredients() : FormArray {
    return this.addRecipeForm.get("recipeIngredients") as FormArray
  }
 
  newIngredient(): FormGroup {
    return this.fb.group({
      quantity: '',
      ingredientId: [],
      measureId: [],
      //ingredient:[],      
      //measure: [],
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
      //console.log(allCategories);
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
    console.log("create new recipe");
    console.log(this.addRecipeForm.value);
    console.log(this.submitted);
    this.submitted = true;
    console.log(this.submitted);
    console.log(this.addRecipeForm.value);  
    //let newUser: User = this.userForm.value;   
    
    this.recipesService.addRecipe(this.addRecipeForm.value).subscribe(response => {
      console.log(response);
      //this.router.navigateByUrl('/members');
      this.isAddMode = false;
      window.location.reload();
    }, error => {
      //this.validationErrors = error;
      console.log(error);
    })

  }

  onSubmit() {
    console.log(this.addRecipeForm.value);      

    var recipeIngredientsArray = [];
      this.addRecipeForm.value.recipeIngredients.forEach(recipeIngredient => recipeIngredientsArray.push(this.fb.group({
        quantity: recipeIngredient.quantity,        
        measureId: recipeIngredient.measureId,        
        ingredientId: recipeIngredient.ingredientId,         
        //measure: this.allMeasures.find(({id}) => id === recipeIngredient.measureId),               
        //ingredient: this.allIngredients.find(({id}) => id === recipeIngredient.ingredientId)
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
    //this.recipesService.updateRecipe(this.member).subscribe(() => {
      //this.toastr.success('Profile updated successfully');
      //this.editForm.reset(this.member);
    //})

    // let recipeToUpdate: BlogPost = {
    //   postId: this.existingBlogPost.postId,
    //   dt: this.existingBlogPost.dt,
    //   creator: this.existingBlogPost.creator,
    //   title: this.form.get(this.formTitle).value,
    //   body: this.form.get(this.formBody).value
    // };
    // this.recipesService.updateRecipe(recipeToUpdate.postId, recipeToUpdate)
    //   .subscribe((data) => {
    //     this.router.navigate([this.router.url]);
    //   });

    

     this.recipesService.updateRecipe(this.id, this.addRecipeForm.value)
          .subscribe(response => {
            //this.toastr.success('Profile updated successfully');
            this.recipe=this.addRecipeForm.value;
            this.isAddMode = false;
            this.addRecipeForm.reset(this.recipe);
            window.location.reload();
          }, error => {
              console.log(error);                      
          })




    //     .pipe(first())
    //     .subscribe({
    //         next: () => {
    //             this.alertService.success('User updated', { keepAfterRouteChange: true });
    //             this.router.navigate(['../../'], { relativeTo: this.route });
    //         },
    //         error: error => {
    //             this.alertService.error(error);
    //             this.loading = false;
    //         }
    //     });

    
}

  //pod przyciskiem cancel
  cancel(){
    console.log("cancel");
    //this.cancelRegister.emit(false);
    //this.router.navigateByUrl('/user/recipes');
    //REFRESH PAGE OR addNewMode=FALSE ALE TO JEST Z componentu rodzica wiec trzebaby przekazać do rodzica
    window.location.reload();
  }

}
