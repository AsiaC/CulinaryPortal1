import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cookbook} from '../_models/cookbook';
import { catchError } from 'rxjs/operators';
import { CookbookRecipe } from '../_models/cookbookRecipe';

@Injectable({
    providedIn: 'root'
})
export class CookbookService {
    baseUrl = environment.apiUrl;
    httpOptions = {    
        headers: { 'Content-Type': 'application/json' },    
        observe: 'response' as const    
      } 

    constructor(private http: HttpClient){}

    getCookbooks(): Observable<Cookbook[]> { 
        return this.http.get<Cookbook[]>(this.baseUrl + 'cookbooks').pipe(
            catchError(this.handleError<Cookbook[]>('getCookbooks', [])));
    }

    addCookbook(cookbook: Cookbook): Observable<Cookbook> {
        return this.http.post<Cookbook>(this.baseUrl + 'cookbooks', cookbook).pipe(
            catchError(this.handleError<Cookbook>('addCookbook')));        
    }

    deleteCookbook(cookbookId: number): Observable<Response> {
        return this.http.delete(this.baseUrl + 'cookbooks/'+ cookbookId, this.httpOptions).pipe(
            catchError(this.handleError<any>('deleteCookbook')));
    }
    
    updateCookbook(cookbookId: number, cookbook: CookbookRecipe): Observable<Response>{
        return this.http.put(this.baseUrl + 'cookbooks/' + cookbookId, cookbook, this.httpOptions).pipe(
            catchError(this.handleError<any>('updateCookbook')));  
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