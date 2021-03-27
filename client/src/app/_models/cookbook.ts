import { CookbookRecipe } from "./cookbookRecipe";
import { Recipe } from "./recipe";

export interface Cookbook {
  id: number;
  name: string;
  description: string;
  userId: number;
  cookbookRecipes: CookbookRecipe[];
}