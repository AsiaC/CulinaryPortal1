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
  getRecipes() : Observable<Recipe[]> {
    return this.http.get<Recipe[]>(this.baseUrl + 'recipes').pipe(
      catchError(this.handleError<Recipe[]>('getRecipes', [])));
  }

  getRecipe(recipeId: number): Observable<Recipe> {
    return this.http.get<Recipe>(this.baseUrl + 'recipes/' + recipeId).pipe(
      catchError(this.handleError<Recipe>('getRecipe recipeId = '+ recipeId)));
  }

  addRecipe(recipe: Recipe): Observable<Response>{
    return this.http.post(this.baseUrl + 'recipes', recipe, this.httpOptions).pipe(
      catchError(this.handleError<any>('addRecipe')));
  }

  updateRecipe(recipeId: number, recipe: Recipe): Observable<Response> { //todo id mi nie jest potrzebne bo w modelu je mogę miec 
    return this.http.put(this.baseUrl + 'recipes/' + recipeId, recipe, this.httpOptions).pipe(
      catchError(this.handleError<any>('updateRecipe')));
  }
  
  deleteRecipe(recipeId: number): Observable<Response>{
    return this.http.delete(this.baseUrl + 'recipes/'+ recipeId, this.httpOptions).pipe(
      catchError(this.handleError<any>('deleteRecipe')));
  }

  searchRecipes(model: any){
    var httpSearchUrl = 'recipes/search';
    if(model.name !== null || (model.categoryId !== null && !Number.isNaN(model.categoryId)) || (model.difficultyLevelId !== null && !Number.isNaN(model.difficultyLevelId)) || (model.preparationTimeId !== null && !Number.isNaN(model.preparationTimeId)) || model.userId !== null || model.top !== null){
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
      if(model.top !== null){
        if(!httpSearchUrl.endsWith('?')){
          httpSearchUrl = httpSearchUrl + '&';
        }
        httpSearchUrl = httpSearchUrl + 'top=' + model.top;
      }
    }
    return this.http.get<Recipe[]>(this.baseUrl + httpSearchUrl).pipe(
      catchError(this.handleError<any>('searchRecipes', [])));
  }
//#endregion

//#region Category
  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + 'categories').pipe(
      catchError(this.handleError<Category[]>('getCategories', [])));
  }
//#endregion

//#region Ingredient
  getIngredients(): Observable<Ingredient[]> {
    return this.http.get<Ingredient[]>(this.baseUrl + 'ingredients').pipe(
      catchError(this.handleError<Ingredient[]>('getIngredients', [])));
  }

  addIngredient(ingredient: Ingredient): Observable<Ingredient>{
    return this.http.post<Ingredient>(this.baseUrl + 'ingredients', ingredient).pipe(
     catchError(this.handleError<Ingredient>('addIngredient')));
  }
//#endregion

//#region Measure
  getMeasures(): Observable<Measure[]> {
    return this.http.get<Measure[]>(this.baseUrl + 'measures').pipe(
      catchError(this.handleError<Measure[]>('getMeasures', [])));
  }
//#endregion

//#region Photo  
  addPhoto(recipeId: number, upload: any): Observable<Response>{
    return this.http.post(this.baseUrl + 'recipes/'+ recipeId + '/photos', upload, {observe: 'response'}).pipe(
      catchError(this.handleError<any>('addPhoto')));
  }

  getRecipePhotos(recipeId: number): Observable<Photo[]>{
    return this.http.get<Photo[]>(this.baseUrl + 'recipes/' + recipeId + '/photos').pipe(
      catchError(this.handleError<Photo[]>('getRecipePhotos', [])));
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
