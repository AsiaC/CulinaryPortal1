import { Recipe } from "./recipe";

export interface CookbookRecipe {
  isRecipeAdded: boolean | null;
  cookbookId: number;
  recipeId: number;  
  userId: number;
  recipe: Recipe;  
}