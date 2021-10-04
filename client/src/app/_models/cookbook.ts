import { CookbookRecipe } from "./cookbookRecipe";
import { Recipe } from "./recipe";

export interface Cookbook {
  id: number | null;
  name: string;
  userId: number; //TODO czy tego potrzebuje
  userName: string;
  cookbookRecipes: CookbookRecipe[];
  //recipes: Recipe[]
}