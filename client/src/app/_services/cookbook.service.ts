import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cookbook} from '../_models/cookbook';
import { map } from 'rxjs/operators';
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

    getCookbook(cookbookId: number): Observable<Cookbook> {   ////TODO
        return this.http.get<Cookbook>(this.baseUrl + 'cookbooks/' + cookbookId);
    }

    addRecipeToCookbook(model: any){
        return this.http.put(this.baseUrl + 'cookbooks', model);
    }
    
    removeRecipeFromCookbook(cookbookId: number, model: CookbookRecipe){
        return this.http.put(this.baseUrl + 'cookbooks/' + cookbookId, model).pipe(
            map((cookbook: Cookbook) => cookbook) //TODO
        )  
    }

    deleteCookbook(cookbookId: number){
        return this.http.delete(this.baseUrl + 'cookbooks/'+ cookbookId)
    }

    addCookbook(model: Cookbook){               
        return this.http.post(this.baseUrl + 'cookbooks', model).pipe(
            map((cookbook: Cookbook) => cookbook) //TODO
        )
    }
}