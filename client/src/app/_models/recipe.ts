import { DifficultyLevelEnum } from './difficultyLevelEnum';
import { Instruction } from './instruction';
import { Photo } from './photo';
import { PreparationTimeEnum } from './preparationTimeEnum';
import { Rate } from './rate';
import { RecipeIngredient } from './recipeIngredient';

export interface Recipe {
  id: number | null;
  name: string;
  description?: string;
  userId: number;
  author: string;
 
  categoryId: number;
  category: string;
  difficultyLevel: DifficultyLevelEnum;
  //difficulty: string;
  preparationTime: PreparationTimeEnum;
  //preparation: string;
  instructions: Instruction[];
  photos: Photo[];
  recipeIngredients: RecipeIngredient[];
  countCookbooks: number;

  rateValues: number;
  countRates: number;
  rates: Rate[];
  totalScore: number;
}


