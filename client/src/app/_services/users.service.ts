import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { AccountService } from './account.service';
import { Recipe } from '../_models/recipe';
import { Cookbook } from '../_models/cookbook';
import { ShoppingList } from '../_models/shoppingList';
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

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'users').pipe(
      catchError(this.handleError<User[]>('getUsers', [])));
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

  getUserRecipes(userId: number): Observable<Recipe[]> {
    return this.http.get<Recipe[]>(this.baseUrl + 'users/' + userId + '/recipes').pipe(
      catchError(this.handleError<Recipe[]>('getUserRecipes userId = ' + userId, [])));
  }

  getUserCookbook(userId: number): Observable<Cookbook> {
    return this.http.get<Cookbook>(this.baseUrl + 'users/' + userId + '/cookbook').pipe(
      catchError(this.handleError<Cookbook>('getUserCookbook userId = ' + userId)));
  }

  getUserShoppingLists(userId: number): Observable<ShoppingList[]> {
    return this.http.get<ShoppingList[]>(this.baseUrl + 'users/' + userId + '/shoppingLists').pipe(
      catchError(this.handleError<ShoppingList[]>('getUserShoppingLists userId = ' + userId, [])));
  }

  searchUserRecipes(model: any, user: number){
    var httpSearchUrl = '/recipes/search';
    if(model.name !== null || (model.categoryId !== null && !Number.isNaN(model.categoryId)) || (model.difficultyLevelId !== null && !Number.isNaN(model.difficultyLevelId)) || (model.preparationTimeId !== null && !Number.isNaN(model.preparationTimeId)) || model.userId !== null){
      httpSearchUrl = httpSearchUrl + '?';
   
      if(model.name !== null){
        httpSearchUrl = httpSearchUrl + 'name=' + model.name;
      }
      if(model.categoryId !== null && !Number.isNaN(model.categoryId)){
        if(!httpSearchUrl.endsWith('?')){
          httpSearchUrl = httpSearchUrl + '&';
        }
        httpSearchUrl = httpSearchUrl + 'categoryId=' + model.categoryId;
      }
      if(model.difficultyLevelId !== null && !Number.isNaN(model.difficultyLevelId)){
        if(!httpSearchUrl.endsWith('?')){
          httpSearchUrl = httpSearchUrl + '&';
        }
        httpSearchUrl = httpSearchUrl + 'difficultyLevelId=' + model.difficultyLevelId;
      }
      if(model.preparationTimeId !== null && !Number.isNaN(model.preparationTimeId)){
        if(!httpSearchUrl.endsWith('?')){
          httpSearchUrl = httpSearchUrl + '&';
        }
        httpSearchUrl = httpSearchUrl + 'preparationTimeId=' + model.preparationTimeId;
      }
      if(model.userId !== null){
        if(!httpSearchUrl.endsWith('?')){
          httpSearchUrl = httpSearchUrl + '&';
        }
        httpSearchUrl = httpSearchUrl + 'userId=' + model.userId;
      }
    }
    return this.http.get<Recipe[]>(this.baseUrl  + 'users/' + user + httpSearchUrl).pipe(
      catchError(this.handleError<Recipe[]>('searchUserRecipes', [])));
  }

  searchUserCookbookRecipes(model: any, user:number){
    var httpSearchUrl = '/cookbook/search';
    if(model.name !== null || (model.categoryId !== null && !Number.isNaN(model.categoryId)) || (model.difficultyLevelId !== null && !Number.isNaN(model.difficultyLevelId)) || (model.preparationTimeId !== null && !Number.isNaN(model.preparationTimeId)) || model.userId !== null){
      httpSearchUrl = httpSearchUrl + '?';
   
      if(model.name !== null){
        httpSearchUrl = httpSearchUrl + 'name=' + model.name;
      }
      if(model.categoryId !== null && !Number.isNaN(model.categoryId)){
        if(!httpSearchUrl.endsWith('?')){
          httpSearchUrl = httpSearchUrl + '&';
        }
        httpSearchUrl = httpSearchUrl + 'categoryId=' + model.categoryId;
      }
      if(model.difficultyLevelId !== null && !Number.isNaN(model.difficultyLevelId)){
        if(!httpSearchUrl.endsWith('?')){
          httpSearchUrl = httpSearchUrl + '&';
        }
        httpSearchUrl = httpSearchUrl + 'difficultyLevelId=' + model.difficultyLevelId;
      }
      if(model.preparationTimeId !== null && !Number.isNaN(model.preparationTimeId)){
        if(!httpSearchUrl.endsWith('?')){
          httpSearchUrl = httpSearchUrl + '&';
        }
        httpSearchUrl = httpSearchUrl + 'preparationTimeId=' + model.preparationTimeId;
      }
      if(model.userId !== null){
        if(!httpSearchUrl.endsWith('?')){
          httpSearchUrl = httpSearchUrl + '&';
        }
        httpSearchUrl = httpSearchUrl + 'userId=' + model.userId;
      }      
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
        console.log(result);
        return of(error);
    }
  }
}

