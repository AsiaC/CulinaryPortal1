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
    httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' })};

    constructor(private http: HttpClient){}

    getCookbooks(): Observable<Cookbook[]> { 
        return this.http.get<Cookbook[]>(this.baseUrl + 'cookbooks').pipe(
            catchError(this.handleError<Cookbook[]>('getCookbooks', [])));
    }

    getCookbook(cookbookId: number): Observable<Cookbook> { //to jest nie uzywane a powinno byc?
        return this.http.get<Cookbook>(this.baseUrl + 'cookbooks/' + cookbookId).pipe(
            catchError(this.handleError<Cookbook>('getCookbook cookbookId = ' + cookbookId)));
    }

    addCookbook(cookbook: Cookbook): Observable<Cookbook> {       debugger;        
        return this.http.post<Cookbook>(this.baseUrl + 'cookbooks', cookbook, this.httpOptions).pipe(
            catchError(this.handleError<Cookbook>('addCookbook')));
    }

    deleteCookbook(cookbookId: number): Observable<Cookbook> {
        return this.http.delete<Cookbook>(this.baseUrl + 'cookbooks/'+ cookbookId, this.httpOptions).pipe(
            catchError(this.handleError<Cookbook>('deleteCookbook')));
    }

    updateCookbook(cookbookId: number, cookbook: CookbookRecipe): Observable<any>{
        return this.http.put(this.baseUrl + 'cookbooks/' + cookbookId, cookbook, this.httpOptions).pipe(
            catchError(this.handleError<any>('updateCookbook')));  
    }

    private handleError<T> (operation = 'operation',result?:T){
        return (error: any): Observable<T> => {
            console.log(operation + ' has error.');
            console.log(error);
            return of(result as T);
        }
    }
}