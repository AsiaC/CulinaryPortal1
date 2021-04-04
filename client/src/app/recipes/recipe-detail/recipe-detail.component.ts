import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Recipe } from 'src/app/_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.css']
})
export class RecipeDetailComponent implements OnInit {
  recipe: Recipe;

  constructor(private recipeService: RecipesService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadRecipe();
  }

  loadRecipe(){
    this.recipeService.getRecipe(Number(this.route.snapshot.paramMap.get('id'))).subscribe(recipe =>{
      this.recipe = recipe; 
      debugger;      
    }, error => {
      console.log(error);
    })
  }

}
