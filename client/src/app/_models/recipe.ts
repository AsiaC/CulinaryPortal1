import { Instruction } from './instruction';

export interface Recipe {
  id: number;
  name: string;
  rate: number;
  description?: string;
  userId: number;
  instructions: Instruction[];
}

