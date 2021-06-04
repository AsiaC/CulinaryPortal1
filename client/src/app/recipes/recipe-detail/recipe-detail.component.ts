import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Recipe } from 'src/app/_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { CookbookService } from 'src/app/_services/cookbook.service';
import { CookbookRecipe } from 'src/app/_models/cookbookRecipe';
import { UsersService } from 'src/app/_services/users.service';
import { Cookbook } from 'src/app/_models/cookbook';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.css']
})
export class RecipeDetailComponent implements OnInit {
  recipe: Recipe;
  user: User; //current
  editRecipe: boolean = false;
  canAddToCookbook: boolean = true;
  userCookbook: Cookbook;
  cookbookRecipe: any = {recipeId: null, userId: null};
  //Delete recipe only when it is not in culinary book
  constructor(private recipeService: RecipesService, private route: ActivatedRoute, private accountService:AccountService, private cookbookService:CookbookService, private userService:UsersService, private toastr: ToastrService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {this.user = user;});
  }

  ngOnInit(): void {
    this.loadRecipe();
    this.loadCookbook();  
  }

  loadRecipe(){
    this.recipeService.getRecipe(Number(this.route.snapshot.paramMap.get('id'))).subscribe(recipe =>{
      this.recipe = recipe;       
      //debugger; 
      //console.log(recipe);
      //console.log(this.user);
      // console.log(recipe.difficultyLevel);
      // console.log(recipe.preparationTime); 
    }, error => {
      console.log(error);
    })
  }

  loadCookbook(){
    //debugger;
    this.userService.getUserCookbook(this.user.id).subscribe(userCookbook => {
      this.userCookbook = userCookbook;
      //debugger;      
      console.log("loadCookbook");
      console.log(this.userCookbook);
      // if(this.userCookbook !== undefined){
      //   var allRecipe = this.userCookbook.recipes;
      //  console.log(allRecipe);
      //  var a = this.userCookbook.recipes.find(e=>e.id === this.recipe.id);
      //   if(a !== undefined) {
      //     this.canAddToCookbook = false;
      //   }
      // }
      if(this.userCookbook !== undefined){
        if(this.userCookbook.recipes !== undefined){ //spr co gdy użytkownik nie ma cookbook wcale, albo gdy nie ma ani jednego przepisu w cookbook
          var recipeInCookbook = this.userCookbook.recipes.find(r=>r.id === this.recipe.id);
          if(recipeInCookbook !== undefined) {
            this.canAddToCookbook = false;
          }
        }
      }
    }, error => {
      console.log(error);
    })
  }

  editThisRecipe()  {
    this.editRecipe = true;
  }

  addToCookbook(){  
    this.cookbookRecipe.recipeId = this.recipe.id;
    this.cookbookRecipe.userId = this.user.id;
    
      this.cookbookService.addRecipeToCookbook(this.cookbookRecipe)
      .subscribe(response =>{
         this.toastr.success('Recipe added successfully');
         this.canAddToCookbook = false;
      }, error => {
        console.log(error);
      })
  }

  removeFromCookbook(){debugger;
    this.cookbookRecipe.recipeId = this.recipe.id;
    this.cookbookRecipe.userId = this.user.id;

    this.cookbookService.removeRecipeFromCookbook(this.cookbookRecipe)
    .subscribe(response => {
      this.toastr.success('Recipe removed successfully');
      this.canAddToCookbook = true;
    }, error => {
        console.log(error);
    })

  }
}
