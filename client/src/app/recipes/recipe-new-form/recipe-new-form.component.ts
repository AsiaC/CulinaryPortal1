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
  user: User; //current
  recipe: Recipe;
  isAddMode: boolean;
  id: string;
  
  constructor(private recipesService: RecipesService, private fb:FormBuilder, private accountService:AccountService, private route: ActivatedRoute, private router: Router) { 
    //this.enumKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k)));
    this.enumKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(Number);
    //this.enumKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(key => ({ title: this.difficultyLevel[key], value: key }));
    //this.enumKeys = Object.keys(this.difficultyLevel);
    //this.preparationTimeKeys = Object.keys(this.preparationTime);   
    this.preparationTimeKeys = Object.keys(this.preparationTime).filter(k => !isNaN(Number(k))).map(Number);   
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {//debugger;
    //this.id = this.route.snapshot.params['id'];
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
    debugger;
    this.recipesService.getRecipe(Number(this.route.snapshot.paramMap.get('id')))
    .pipe(first())
    .subscribe(recipe =>{      
      this.recipe = recipe; 
      //this.addRecipeForm.patchValue(recipe)
      this.addRecipeForm.patchValue({
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
      debugger;
      var recipeIngredientsArray = [];
      this.recipe.recipeIngredients.forEach(recipeIngredient => recipeIngredientsArray.push(this.fb.group({
        quantity: recipeIngredient.quantity,
        //measureId: recipeIngredient.measureId,
        measureId: recipeIngredient.measure.id,
        //ingredientId: recipeIngredient.ingredientId,  
        ingredientId: recipeIngredient.ingredient.id, 
        measure: recipeIngredient.measure,   
        ingredient: recipeIngredient.ingredient,
        measureName: recipeIngredient.measureName,  
        ingredientName: recipeIngredient.ingredientName,    
          
      })));
      this.addRecipeForm.setControl('recipeIngredients', this.fb.array(recipeIngredientsArray || []));

      
      debugger;      
    }, error => {
      console.log(error);
    });
    // this.recipesService.getRecipe(Number(this.id))
    //             .pipe(first())
    //             .subscribe(x => this.addRecipeForm.patchValue(x));

    // this.recipesService.getRecipe(Number(this.id))
    //             .pipe(first())
    //             .subscribe(x => {
    //               debugger;
    //               this.addRecipeForm.patchValue(x)
    //               debugger;
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
      userId: [{value: this.user.id}]
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

  createNewRecipe(){
    debugger;
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
    }, error => {
      //this.validationErrors = error;
      console.log(error);
    })

  }

  onSubmit() {
    console.log(this.addRecipeForm.value);
    debugger;
    if(this.isAddMode){
      this.createNewRecipe();
    }
    else{
      this.updateRecipe();
    }
    
  }
  private updateRecipe() {
    debugger;
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
            console.log(response);
            this.router.navigate(['../../'], { relativeTo: this.route });
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

// updateBlogPost(postId: number, blogPost): Observable<BlogPost> {
//   return this.http.put<BlogPost>(this.myAppUrl + this.myApiUrl + postId, JSON.stringify(blogPost), this.httpOptions)
//     .pipe(
//       retry(1),
//       catchError(this.errorHandler)
//     );
// }

  //pod przyciskiem cancel
  cancel(){
    console.log("cancel");
    //this.cancelRegister.emit(false);
    //this.router.navigateByUrl('/user/recipes');
    //REFRESH PAGE OR addNewMode=FALSE ALE TO JEST Z componentu rodzica wiec trzebaby przekazać do rodzica
    window.location.reload();
  }

}
