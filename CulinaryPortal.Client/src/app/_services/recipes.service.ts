import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Recipe } from '../_models/recipe';
import { Category } from '../_models/category';
import { Ingredient } from '../_models/ingredient';
import { Measure } from '../_models/measure';
import { Photo } from '../_models/photo';
import { Rate } from '../_models/rate';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RecipesService {
  baseUrl = environment.apiUrl;

  httpOptions = {    
    headers: { 'Content-Type': 'application/json' },    
    observe: 'response' as const    
  }   

  constructor(private http: HttpClient) { }

//#region Recipe
  getRecipes() : Observable<any> {
    return this.http.get(this.baseUrl + 'recipes').pipe(
      catchError(this.handleError<any>('getRecipes')));
  }

  getRecipe(recipeId: number): Observable<Recipe> {
    return this.http.get<Recipe>(this.baseUrl + 'recipes/' + recipeId).pipe(
      catchError(this.handleError<Recipe>('getRecipe recipeId = '+ recipeId)));
  }

  addRecipe(recipe: Recipe): Observable<Response>{
    return this.http.post(this.baseUrl + 'recipes', recipe, this.httpOptions).pipe(
      catchError(this.handleError<any>('addRecipe')));
  }

  updateRecipe(recipeId: number, recipe: Recipe): Observable<Response> { 
    return this.http.put(this.baseUrl + 'recipes/' + recipeId, recipe, this.httpOptions).pipe(
      catchError(this.handleError<any>('updateRecipe')));
  }
  
  deleteRecipe(recipeId: number): Observable<Response>{
    return this.http.delete(this.baseUrl + 'recipes/'+ recipeId, this.httpOptions).pipe(
      catchError(this.handleError<any>('deleteRecipe')));
  }

  searchRecipes(dict: any){
    var httpSearchUrl = 'recipes/search?';

    for (const key of Object.keys(dict)) {
      if(!httpSearchUrl.endsWith('?')){
          httpSearchUrl = httpSearchUrl + '&';
      }
      httpSearchUrl += key +"="+dict[key]        
    }     

    return this.http.get<Recipe[]>(this.baseUrl + httpSearchUrl).pipe(
      catchError(this.handleError<Recipe[]>('searchRecipes', [])));
  }
//#endregion

//#region Category
  getCategories(): Observable<any> {
    return this.http.get(this.baseUrl + 'categories').pipe(
      catchError(this.handleError<any>('getCategories')));
  }
//#endregion

//#region Ingredient
  getIngredients(): Observable<any> {
    return this.http.get(this.baseUrl + 'ingredients').pipe(
      catchError(this.handleError<any>('getIngredients')));
  }

  addIngredient(ingredient: Ingredient): Observable<Ingredient>{
    return this.http.post<Ingredient>(this.baseUrl + 'ingredients', ingredient).pipe(
     catchError(this.handleError<Ingredient>('addIngredient')));
  }
//#endregion

//#region Measure
  getMeasures(): Observable<any> {
    return this.http.get(this.baseUrl + 'measures').pipe(
      catchError(this.handleError<any>('getMeasures')));
  }
//#endregion

//#region Photo  
  addPhoto(recipeId: number, upload: any): Observable<Response>{
    return this.http.post(this.baseUrl + 'recipes/'+ recipeId + '/photos', upload, {observe: 'response'}).pipe(
      catchError(this.handleError<any>('addPhoto')));
  }

  getRecipePhotos(recipeId: number): Observable<any>{
    return this.http.get(this.baseUrl + 'recipes/' + recipeId + '/photos').pipe(
      catchError(this.handleError<any>('getRecipePhotos')));
  }

  deletePhoto(photoId: number, recipeId: number): Observable<Response> {
    return this.http.delete(this.baseUrl + 'recipes/' + recipeId + '/photos/' + photoId, this.httpOptions).pipe(
      catchError(this.handleError<any>('deletePhoto')));
  }

  updateMainRecipePhoto(photoId: number, recipeId: number): Observable<Response>{
    return this.http.put(this.baseUrl + 'recipes/' + recipeId + '/photos', photoId, this.httpOptions).pipe(
      catchError(this.handleError<any>('updateMainRecipePhoto')));     
  }  
//#endregion

//#region Rate
  addRate(rate: Rate): Observable<Response> {
    return this.http.post(this.baseUrl + 'rates', rate, this.httpOptions).pipe(
      catchError(this.handleError<any>('addRate')));
  }  

  deleteRate(rateId: number): Observable<Response>{      
    return this.http.delete(this.baseUrl + 'rates/' + rateId, this.httpOptions).pipe(
      catchError(this.handleError<any>('deleteRate')));
  }
//#endregion

  private handleError<T> (operation = 'operation',result?:T){
    return (error: any): Observable<T> => {
        console.log(operation + ' has error.');
        console.log(error);
        console.log('result = ' + result);
        return of(error);
    }
  }
}
