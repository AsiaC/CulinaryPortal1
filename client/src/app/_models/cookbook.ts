import { CookbookRecipe } from "./cookbookRecipe";
import { Recipe } from "./recipe";

export interface Cookbook {
  isRecipeAdded: boolean | null;
  id: number | null;
  name: string;
  userId: number; //TODO czy tego potrzebuje
  userName: string;
  cookbookRecipes: CookbookRecipe[];
}