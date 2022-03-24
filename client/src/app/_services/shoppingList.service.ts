import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { ShoppingList } from '../_models/shoppingList';

@Injectable({
    providedIn: 'root'
  })
  export class ShoppingListService {
    baseUrl = environment.apiUrl;
   
    httpOptions = {    
      headers: { 'Content-Type': 'application/json' },    
      observe: 'response' as const    
    }    
    
    constructor(private http: HttpClient) { }
  
    getShoppingLists(): Observable<ShoppingList[]> {
        return this.http.get<ShoppingList[]>(this.baseUrl + 'shoppingLists').pipe(
          catchError(this.handleError<any>('getShoppingLists')));
    }

    getShoppingList(shoppingListId: number): Observable<ShoppingList> {
        return this.http.get<ShoppingList>(this.baseUrl + 'shoppingLists/' + shoppingListId).pipe(
          catchError(this.handleError<ShoppingList>('getShoppingList shoppingListId = ' + shoppingListId)));
    }   
 
    addShoppingList(shoppingList: ShoppingList):  Observable<Response> {
        return this.http.post(this.baseUrl + 'shoppingLists', shoppingList, this.httpOptions).pipe(
          catchError(this.handleError<any>('addShoppingList')));
    }

    updateShoppingList(shoppingListId: string, shoppingList: ShoppingList): Observable<Response>{       
        return this.http.put(this.baseUrl + 'shoppingLists/' + shoppingListId, shoppingList, this.httpOptions).pipe(
          catchError(this.handleError<any>('updateShoppingList')));
    }

    deleteShoppingList(shoppingListId: number): Observable<Response> {      
      return this.http.delete(this.baseUrl + 'shoppinglists/' + shoppingListId, this.httpOptions).pipe(
        catchError(this.handleError<any>('deleteShoppingList')));
    }

    private handleError<T> (operation = 'operation',result?:T){
      return (error: any): Observable<T> => {
          console.log(operation + ' has error.');
          console.log(error);
          return of(result as T);
      }
    }
  }