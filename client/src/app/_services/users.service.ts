import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
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
  users: User[] = [];
  user:User;
  baseUrl=environment.apiUrl;


  constructor(private http: HttpClient, private accountService: AccountService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
    })
  }

  getUsers(){
    return this.http.get<User[]>(this.baseUrl+'users');
  }

  getUser(user:number): Observable<User> { 
    return this.http.get<User>(this.baseUrl+'users/'+user);
  }

  updateUser(user: User) {
    return this.http.put(this.baseUrl + 'users', user);
  }

  getUserRecipes(user:number){
    return this.http.get<Recipe[]>(this.baseUrl + 'users/' + user + '/recipes');
  }

  getUserCookbook(user:number){
    return this.http.get<Cookbook>(this.baseUrl + 'users/' + user + '/cookbook');
  }

  getUserShoppingLists(user:number){
    return this.http.get<ShoppingList[]>(this.baseUrl + 'users/' + user + '/shoppingLists');
  }

  searchUserRecipes(model: any, user:number){
    return this.http.put<Recipe[]>(this.baseUrl  + 'users/' + user + '/recipes/search', model)
  }

  searchUserCookbook(model: any, user:number){
    return this.http.put<Recipe[]>(this.baseUrl  + 'users/' + user + '/cookbook/search', model)
  }

  getUserRecipeRate(userId: number, recipeId: number){
    return this.http.get(this.baseUrl  + 'users/' + userId + '/recipes/' + recipeId ).pipe(
      map((rate: Rate) => rate)
    )    
  }
  //dorobić w kontrolerze
  // deleteUser(userId: number){
  //   return this.http.delete(this.baseUrl + 'users/'+ userId)
  // }

  getNumberOfUsers(){
    return this.http.get<number>(this.baseUrl  + 'users/' + this.user.id + '/registeredUsers') //to moze nie potrzebuje w kontrolerach tego user skoro tu mam
  }
}

