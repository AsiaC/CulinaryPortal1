import { Instruction } from './instruction';
import { Photo } from './photo';

export interface Recipe {
  id: number;
  name: string;
  rate: number;
  description?: string;
  userId: number;
  instructions: Instruction[];
  photos: Photo[];
}

