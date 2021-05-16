import { Cookbook } from "./cookbook";
import { Recipe } from "./recipe";

export interface CookbookRecipe {
  cookbookId: number;
  recipeId: number;
  note: string;
  userId: number; //dod
  //recipe: Recipe;
  //cokbook: Cookbook
}