import { Ingredient } from "./ingredient";
import { Measure } from "./measure";

export interface RecipeIngredient {
    quantity: number;    
    ingredientId: number;
    measureId: number;
    ingredient: Ingredient;
    measure: Measure;
  }
