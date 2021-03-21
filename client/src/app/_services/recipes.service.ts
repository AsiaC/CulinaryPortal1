import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Recipe } from '../_models/recipe';

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
}
