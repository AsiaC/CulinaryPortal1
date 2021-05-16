import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Recipe } from 'src/app/_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { CookbookService } from 'src/app/_services/cookbook.service';
import { CookbookRecipe } from 'src/app/_models/cookbookRecipe';

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.css']
})
export class RecipeDetailComponent implements OnInit {
  recipe: Recipe;
  user: User; //current
  editRecipe: boolean = false;
  cookbookRecipe: any = {recipeId: null, userId: null};
  //Delete recipe only when it is not in culinary book
  constructor(private recipeService: RecipesService, private route: ActivatedRoute, private accountService:AccountService, private cookbookService:CookbookService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {this.user = user;});
  }

  ngOnInit(): void {
    this.loadRecipe();
    
  }

  loadRecipe(){
    this.recipeService.getRecipe(Number(this.route.snapshot.paramMap.get('id'))).subscribe(recipe =>{
      this.recipe = recipe;       
      //debugger; 
      // console.log(recipe.difficultyLevel);
      // console.log(recipe.preparationTime); 
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
         //this.toastr.success('Profile updated successfully');
         //debugger;  
      }, error => {
        console.log(error);
      })
  }
}
