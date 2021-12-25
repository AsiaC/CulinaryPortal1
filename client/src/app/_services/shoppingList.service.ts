import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError, map } from 'rxjs/operators';
import { ShoppingList } from '../_models/shoppingList';

@Injectable({
    providedIn: 'root'
  })
  export class ShoppingListService {
    baseUrl = environment.apiUrl;
    httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' })};

    constructor(private http: HttpClient) { }
  
    getShoppingLists(): Observable<ShoppingList[]> {
        return this.http.get<ShoppingList[]>(this.baseUrl + 'shoppingLists').pipe(
          catchError(this.handleError<ShoppingList[]>('getShoppingLists')));
    }

    getShoppingList(shoppingListId: number): Observable<ShoppingList> {
        return this.http.get<ShoppingList>(this.baseUrl + 'shoppingLists/' + shoppingListId).pipe(
          catchError(this.handleError<ShoppingList>('getShoppingList shoppingListId = ' + shoppingListId)));
    }

    addShoppingList(shoppingList: ShoppingList): Observable<ShoppingList> {
        return this.http.post<ShoppingList>(this.baseUrl + 'shoppingLists', shoppingList, this.httpOptions).pipe(
          catchError(this.handleError<ShoppingList>('addShoppingList')));
    }

    updateShoppingList(shoppingListId: string, shoppingList: ShoppingList): Observable<any>{ 
        return this.http.put(this.baseUrl + 'shoppingLists/' + shoppingListId, shoppingList, this.httpOptions).pipe(
          catchError(this.handleError<any>('updateShoppingList')));
    }

    deleteShoppingList(shoppingListId: number): Observable<ShoppingList> {      
      return this.http.delete<ShoppingList>(this.baseUrl + 'shoppinglists/' + shoppingListId, this.httpOptions).pipe(
        catchError(this.handleError<ShoppingList>('deleteShoppingList')));
    }

    private handleError<T> (operation = 'operation',result?:T){ debugger;
      return (error: any): Observable<T> => {
          console.log(operation + ' has error.');
          console.log(error);
        //console.error(operation + ' has error = '+ error);
          return of(result as T);
      }
    }
  }