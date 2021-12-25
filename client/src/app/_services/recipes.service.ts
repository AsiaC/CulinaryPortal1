import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Recipe } from '../_models/recipe';
import { map } from 'rxjs/operators';
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
  httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' })};

  constructor(private http: HttpClient) { }

  getRecipes() : Observable<Recipe[]> {
    return this.http.get<Recipe[]>(this.baseUrl + 'recipes').pipe(
      catchError(this.handleError<Recipe[]>('getRecipes', [])));
  }

  getRecipe(recipeId: number): Observable<Recipe> {
    return this.http.get<Recipe>(this.baseUrl + 'recipes/' + recipeId).pipe(
      catchError(this.handleError<Recipe>('getRecipe recipeId = '+ recipeId)));
  }

  addRecipe(recipe: Recipe): Observable<Recipe>{
    return this.http.post<Recipe>(this.baseUrl + 'recipes', recipe, this.httpOptions).pipe(
      catchError(this.handleError<Recipe>('addRecipe')));
  }

  updateRecipe(recipeId: string, recipe: Recipe): Observable<any> {//todo id mi nie jest potrzebne bo w modelu je mogę miec 
    return this.http.put(this.baseUrl + 'recipes/' + recipeId, recipe, this.httpOptions).pipe(
      catchError(this.handleError<any>('updateRecipe')));
  }
  
  deleteRecipe(recipeId: number): Observable<Recipe>{
    return this.http.delete<Recipe>(this.baseUrl + 'recipes/'+ recipeId, this.httpOptions).pipe(
      catchError(this.handleError<Recipe>('deleteRecipe')));
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + 'categories').pipe(
      catchError(this.handleError<Category[]>('getCategories', [])));
  }

  getIngredients(): Observable<Ingredient[]> {
    return this.http.get<Ingredient[]>(this.baseUrl + 'ingredients').pipe(
      catchError(this.handleError<Ingredient[]>('getIngredients', [])));
  }
  
  getMeasures(): Observable<Measure[]> {
    return this.http.get<Measure[]>(this.baseUrl + 'measures').pipe(
      catchError(this.handleError<Measure[]>('getMeasures', [])));
  }

  searchRecipes(model: any){
    return this.http.put<Recipe[]>(this.baseUrl + 'recipes/search', model)
  }

  addPhoto(recipeId: number, upload: any){
    return this.http.post(this.baseUrl + 'recipes/'+ recipeId + '/photos', upload) 
  }

  getRecipePhotos(recipeId: number): Observable<Photo[]>{
    return this.http.get<Photo[]>(this.baseUrl + 'recipes/' + recipeId + '/photos').pipe(
      catchError(this.handleError<Photo[]>('getRecipePhotos', [])));
  }

  deletePhoto(photoId: number): Observable<Photo> {
    return this.http.delete<Photo>(this.baseUrl + 'photos/' + photoId, this.httpOptions).pipe(
      catchError(this.handleError<Photo>('deletePhoto')));
  }

  updatePhoto(photoId: any, model: any){
    return this.http.put(this.baseUrl + 'photos/' + photoId, model).pipe(
      map((photo: Photo) => photo))   
  }

  updateMainRecipePhoto(photoId: number, recipeId: number){
    return this.http.put(this.baseUrl + 'recipes/' + recipeId + '/photos', photoId )   
  }  

  addRate(rate: Rate): Observable<Rate> {
    return this.http.post<Rate>(this.baseUrl + 'rates', rate, this.httpOptions).pipe(
      catchError(this.handleError<Rate>('addRate')));
  }  

  deleteRate(rateId: number): Observable<Rate>{      
    return this.http.delete<Rate>(this.baseUrl + 'rates/' + rateId, this.httpOptions).pipe(
      catchError(this.handleError<Rate>('deleteRate')));
  }

  addIngredient(ingredient: Ingredient): Observable<Ingredient>{
    return this.http.post<Ingredient>(this.baseUrl + 'ingredients', ingredient, this.httpOptions).pipe(
     catchError(this.handleError<Ingredient>('addIngredient')));
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
