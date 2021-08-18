import { CookbookRecipe } from "./cookbookRecipe";
import { Recipe } from "./recipe";

export interface Cookbook {
  id: number | null;
  name: string;
  description: string;
  userId: number;
  cookbookRecipes: CookbookRecipe[];
  //recipes: Recipe[]
}