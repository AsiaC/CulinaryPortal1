import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Recipe } from '../_models/recipe';
import {map, repeat} from 'rxjs/operators';
import { Category } from '../_models/category';
import { Ingredient } from '../_models/ingredient';
import { Measure } from '../_models/measure';
import { Photo } from '../_models/photo';

@Injectable({
  providedIn: 'root'
})
export class RecipesService {
  baseUrl=environment.apiUrl;

  constructor(private http: HttpClient) { }

  getRecipes() {
    return this.http.get<Recipe[]>(this.baseUrl + 'recipes');
  }

  getRecipe(recipe: number): Observable<Recipe> {
    return this.http.get<Recipe>(this.baseUrl + 'recipes/' + recipe);
  }

  addRecipe(model: any){
    return this.http.post(this.baseUrl + 'recipes', model).pipe(
      map((recipe: Recipe) => recipe)
    )    
  }

  getCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'categories');
  }

  getIngredients() {
    return this.http.get<Ingredient[]>(this.baseUrl + 'ingredients');
  }
  
  getMeasures() {
    return this.http.get<Measure[]>(this.baseUrl + 'measures');
  }

  updateRecipe(recipeId: string, model: any){ 
    return this.http.put(this.baseUrl + 'recipes/' + recipeId, model).pipe(
      map((recipe: Recipe) => recipe)
    )    
  }
  
  deleteRecipe(recipeId: number){
    return this.http.delete(this.baseUrl + 'recipes/'+ recipeId)
  }

  searchRecipes(model: any){
    return this.http.put<Recipe[]>(this.baseUrl + 'recipes/search', model)
  }

  addPhoto(recipeId: number, model: any){
    debugger;
    // Create form data
    //const formData = new FormData();         
    // Store form name as "file" with file data
    //formData.append("file", file, file.name);

    return this.http.post(this.baseUrl + 'recipes/'+ recipeId + '/photos', model)
    // .pipe(
    //   map((recipe: Recipe) => recipe)
    //)   
  }

  getRecipePhotos(recipeId: number){
    debugger;
    return this.http.get<Photo[]>(this.baseUrl + 'recipes/' + recipeId + '/photos');
  }
}
