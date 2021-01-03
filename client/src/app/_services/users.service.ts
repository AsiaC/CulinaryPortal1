import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { AccountService } from './account.service';

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
}

