import { Ingredient } from "./ingredient";
import { Measure } from "./measure";

export interface RecipeIngredient {
    quantity: number;
    //ingredientName: string;
    //measureName: string;
    
     ingredientId: number;
     measureId: number;
    ingredient: Ingredient;
    measure: Measure;
  }
