import { Recipe } from "./recipe";

export interface CookbookRecipe {
  isRecipeAdded: boolean | null;
  cookbookId: number;
  recipeId: number;
  note: string;
  userId: number;
  recipe: Recipe;  
}