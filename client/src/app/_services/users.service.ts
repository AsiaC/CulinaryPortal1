import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { AccountService } from './account.service';
import { Recipe } from '../_models/recipe';
import { Cookbook } from '../_models/cookbook';

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
    //debugger;    
    return this.http.get<Recipe[]>(this.baseUrl + 'users/' + user + '/recipes');
  }

  getUserCookbook(user:number){
    //debugger;    
    return this.http.get<Cookbook>(this.baseUrl + 'users/' + user + '/cookbook');
  }
}

