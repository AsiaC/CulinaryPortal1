import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cookbook} from '../_models/cookbook';
import { map, repeat} from 'rxjs/operators';
import { CookbookRecipe } from '../_models/cookbookRecipe';

@Injectable({
    providedIn: 'root'
})
export class CookbookService {
    baseUrl = environment.apiUrl;

    constructor(private http: HttpClient){}

    getCookbooks() {
        return this.http.get<Cookbook[]>(this.baseUrl + 'cookbooks');
    }

    getCookbook(cookbook: number): Observable<Cookbook> {  
        return this.http.get<Cookbook>(this.baseUrl + 'cookbooks/' + cookbook);
    }

    addRecipeToCookbook(model: any){
        return this.http.put(this.baseUrl + 'cookbooks', model);
    }
    
    removeRecipeFromCookbook(cookbookId: number, model: CookbookRecipe){
        debugger;
        return this.http.put(this.baseUrl + 'cookbooks/' + cookbookId, model).pipe(
            map((cookbook: Cookbook) => { 
          })
         )  
    }

    deleteCookbook(cookbookId: number){
        debugger;
        const options = {   
          headers: new HttpHeaders({
            'Content-Type': 'application/json',
          }),     
          body: cookbookId,        
        };
        return this.http.delete(this.baseUrl + 'cookbooks', options)
      }

    addCookbook(model: Cookbook){
        debugger;
        return this.http.post(this.baseUrl + 'cookbooks', model).pipe(
            map((cookbook: Cookbook) => {

            })
        )
    }
}