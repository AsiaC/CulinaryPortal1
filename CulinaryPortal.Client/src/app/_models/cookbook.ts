import { CookbookRecipe } from "./cookbookRecipe";
import { Recipe } from "./recipe";

export interface Cookbook {  
  id: number | null;
  name: string;
  userId: number; //TODO czy tego potrzebuje
  userName: string | null; //TODO CZY JA TEGO POTRZEBUJE?
  cookbookRecipes: CookbookRecipe[];
}