import { Instruction } from './instruction';
import { Photo } from './photo';
import { RecipeIngredient } from './recipeIngredient';

export interface Recipe {
  id: number;
  name: string;
  rate: number;
  description?: string;
  userId: number;
  instructions: Instruction[];
  photos: Photo[];
  recipeIngredients: RecipeIngredient[];
}


