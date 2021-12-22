import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { ShoppingList } from '../_models/shoppingList';

@Injectable({
    providedIn: 'root'
  })
  export class ShoppingListService {
    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }
  
    getShoppingLists() {
        return this.http.get<ShoppingList[]>(this.baseUrl + 'shoppingLists');
    }

    getShoppingList(shoppingListId: number): Observable<ShoppingList> {
        return this.http.get<ShoppingList>(this.baseUrl + 'shoppingLists/' + shoppingListId);
    }

    addShoppingList(model: any){
        return this.http.post(this.baseUrl + 'shoppingLists', model).pipe(
          map((shoppingList: ShoppingList) => shoppingList)
      )    
    }

    updateShoppingList(shoppingListId: string, model: any){ 
        return this.http.put(this.baseUrl + 'shoppingLists/' + shoppingListId, model).pipe(
          map((shoppingList: ShoppingList) => shoppingList)
      )    
    }

    deleteShoppingList(shoppingListId: number){      
      return this.http.delete(this.baseUrl + 'shoppinglists/' + shoppingListId)
    }
  }