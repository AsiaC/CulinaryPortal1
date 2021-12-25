import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { AccountService } from './account.service';
import { Recipe } from '../_models/recipe';
import { Cookbook } from '../_models/cookbook';
import { ShoppingList } from '../_models/shoppingList';
import { map } from 'rxjs/operators';
import { Rate } from '../_models/rate';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  user: User;
  baseUrl = environment.apiUrl;
  httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' })};

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

  updateUser(user: User): Observable<any> {
    return this.http.put(this.baseUrl + 'users', user, this.httpOptions).pipe(
      catchError(this.handleError<any>('updateUser')));
  }

  deleteUser(userId: number): Observable<User>{
    return this.http.delete<User>(this.baseUrl + 'users/'+ userId, this.httpOptions).pipe(
      catchError(this.handleError<User>('deleteUser')));
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
    return this.http.put<Recipe[]>(this.baseUrl  + 'users/' + user + '/recipes/search', model)
  }

  searchUserCookbook(model: any, user:number){
    return this.http.put<Recipe[]>(this.baseUrl  + 'users/' + user + '/cookbook/search', model)
  }

  getUserRecipeRate(userId: number, recipeId: number): Observable<Rate>{
    return this.http.get<Rate>(this.baseUrl  + 'users/' + userId + '/recipes/' + recipeId ).pipe(
      catchError(this.handleError<Rate>('getUserRecipeRate userId = ' + userId + ' recipeId = ' + recipeId)));
  } 

  getNumberOfUsers(){
    return this.http.get<number>(this.baseUrl  + 'users/' + this.user.id + '/registeredUsers') //to moze nie potrzebuje w kontrolerach tego user skoro tu mam
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

