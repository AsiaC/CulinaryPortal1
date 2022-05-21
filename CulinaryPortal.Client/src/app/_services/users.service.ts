import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { AccountService } from './account.service';
import { Recipe } from '../_models/recipe';
import { Rate } from '../_models/rate';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  user: User;
  baseUrl = environment.apiUrl;
  httpOptions = {    
    headers: { 'Content-Type': 'application/json' },    
    observe: 'response' as const    
  } 

  constructor(private http: HttpClient, private accountService: AccountService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
    })
  }

  getUsers(): Observable<any> {
    return this.http.get(this.baseUrl + 'users').pipe(
      catchError(this.handleError<any>('getUsers')));
  }

  getUser(userId: number): Observable<User> { 
    return this.http.get<User>(this.baseUrl + 'users/' + userId).pipe(
      catchError(this.handleError<User>('getUser userId = ' + userId)));
  }

  updateUser(user: User): Observable<Response> {
    return this.http.put(this.baseUrl + 'users', user, this.httpOptions).pipe(
      catchError(this.handleError<any>('updateUser')));
  }

  deleteUser(userId: number): Observable<Response>{
    return this.http.delete(this.baseUrl + 'users/'+ userId, this.httpOptions).pipe(
      catchError(this.handleError<any>('deleteUser')));
  }

  getUserRecipes(userId: number): Observable<any> {
    return this.http.get(this.baseUrl + 'users/' + userId + '/recipes').pipe(
      catchError(this.handleError<any>('getUserRecipes userId = ' + userId)));
  }

  getUserCookbook(userId: number): Observable<any> {
    return this.http.get(this.baseUrl + 'users/' + userId + '/cookbook').pipe(
      catchError(this.handleError<any>('getUserCookbook userId = ' + userId)));
  }

  getUserShoppingLists(userId: number): Observable<any> {
    return this.http.get(this.baseUrl + 'users/' + userId + '/shoppingLists').pipe(
      catchError(this.handleError<any>('getUserShoppingLists userId = ' + userId )));
  }

  searchUserRecipes(dict: any, user: number){
    var httpSearchUrl = '/recipes/search?';

    for (const key of Object.keys(dict)) {
      if(!httpSearchUrl.endsWith('?')){
          httpSearchUrl = httpSearchUrl + '&';
      }
      httpSearchUrl += key +"="+dict[key]        
    }   
    
    return this.http.get<Recipe[]>(this.baseUrl  + 'users/' + user + httpSearchUrl).pipe(
      catchError(this.handleError<Recipe[]>('searchUserRecipes', [])));
  }

  searchUserCookbookRecipes(dict: any, user:number){
    var httpSearchUrl = '/cookbook/search?';

    for (const key of Object.keys(dict)) {
      if(!httpSearchUrl.endsWith('?')){
          httpSearchUrl = httpSearchUrl + '&';
      }
      httpSearchUrl += key +"="+dict[key]        
    }     
    
    return this.http.get<Recipe[]>(this.baseUrl  + 'users/' + user + httpSearchUrl).pipe(
      catchError(this.handleError<Recipe[]>('searchUserCookbookRecipes', [])));
  }

  getUserRecipeRate(userId: number, recipeId: number): Observable<Rate>{
    return this.http.get<Rate>(this.baseUrl  + 'users/' + userId + '/recipes/' + recipeId ).pipe(
      catchError(this.handleError<Rate>('getUserRecipeRate userId = ' + userId + ' recipeId = ' + recipeId)));
  }  

  private handleError<T> (operation = 'operation',result?:T){
    return (error: any): Observable<T> => {
        console.log(operation + ' has error.');
        console.log(error);
        console.log('result = ' + result);
        return of(error);
    }
  }
}

