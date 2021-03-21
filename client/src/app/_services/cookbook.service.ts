import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cookbook} from '../_models/cookbook';

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
}