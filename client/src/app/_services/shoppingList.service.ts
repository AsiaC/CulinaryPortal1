import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Recipe } from '../_models/recipe';
import {map, repeat} from 'rxjs/operators';
import { Category } from '../_models/category';
import { Ingredient } from '../_models/ingredient';
import { Measure } from '../_models/measure';
import { ShoppingList } from '../_models/shoppingList';

@Injectable({
    providedIn: 'root'
  })
  export class ShoppingListService {
    baseUrl=environment.apiUrl;

    constructor(private http: HttpClient) { }
  
    getShoppingLists() {
        return this.http.get<ShoppingList[]>(this.baseUrl + 'shoppingLists');
    }

    getShoppingList(shoppingList: number): Observable<ShoppingList> {
        return this.http.get<ShoppingList>(this.baseUrl + 'shoppingLists/' + shoppingList);
        debugger;
    }

    addShoppingList(model: any){
        debugger;
            return this.http.post(this.baseUrl + 'shoppingLists', model).pipe(
              map((shoppingList: ShoppingList) => {
            })
        )    
    }

    updateShoppingList(shoppingListId: string, model: any){        
            return this.http.put(this.baseUrl + 'shoppingLists/' + shoppingListId, model).pipe(
              map((shoppingList: ShoppingList) => { 
            })
        )    
    }

    // addRecipeIngredients(listId: number, recipeIngredients: any){
    //   debugger;
    //   return this.http.post()
    // }

  }