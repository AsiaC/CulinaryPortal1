import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient) { }

  login(model: any){ 
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((user: User) => {  
        if (user){
          this.setCurrentUser(user);                   
        }
      })
    ).pipe(catchError(this.handleError<User>('login')));
  }

  register(model: any){
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if(user){
          this.setCurrentUser(user);          
        }
      })
    ).pipe(catchError(this.handleError<User>('register')));
  }

  setCurrentUser(user: User){   
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;    
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles); //change role to the array of roles, even if user has only 1 role
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);    
  }

  logout(){ 
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  getDecodedToken(token) {
    return JSON.parse(atob(token.split('.')[1]));
  }

  private handleError<T> (operation = 'operation',result?:T){
    return (error: any): Observable<T> => {
        console.log(operation + ' has error.');
        console.log(error);
        return of(result as T);
    }
  }
}
