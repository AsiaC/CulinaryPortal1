import { ListItem } from "./listItem";

export interface ShoppingList{
    id: number | null;
    name: string;
    items: ListItem[];
    userId: number; //TODO CZY TEGO POTRZEBUJE?
    userName: string;
}