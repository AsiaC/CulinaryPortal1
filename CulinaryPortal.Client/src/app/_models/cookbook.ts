import { CookbookRecipe } from "./cookbookRecipe";

export interface Cookbook {  
  id: number | null;
  name: string;
  userId: number;  
  cookbookRecipes: CookbookRecipe[];
}