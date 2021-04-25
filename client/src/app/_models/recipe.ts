import { DifficultyLevelEnum } from './difficultyLevelEnum';
import { Instruction } from './instruction';
import { Photo } from './photo';
import { PreparationTimeEnum } from './preparationTimeEnum';
import { RecipeIngredient } from './recipeIngredient';

export interface Recipe {
  id: number;
  name: string;
  rate: number;
  description?: string;
  //userId: number;
  author: string;
  categoryId: number;
  category: string;
  difficultyLevel: DifficultyLevelEnum;
  difficulty: string;
  preparationTime: PreparationTimeEnum;
  preparation: string;
  instructions: Instruction[];
  photos: Photo[];
  recipeIngredients: RecipeIngredient[];
}


