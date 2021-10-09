import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Recipe } from '../_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode: boolean = false;
  users: any;
  recipes: Recipe[];

  constructor(private http: HttpClient, private recipeService: RecipesService) { }

  ngOnInit(): void {
    this.getUsers();
    this.searchRecipes();
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
    // console.log(this.registerMode);
    // this.registerMode = true;
    // console.log(this.registerMode);
  }
  getUsers(){
    this.http.get('http://localhost:50725/api/users').subscribe( users => {
      this.users = users;
    }, error => {
      console.log(error);
    })
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }

  searchRecipes(){
    var searchModel = {name: null, categoryId: null, difficultyLevelId: null, preparationTimeId: null, userId: null, top: 6}
    this.recipeService.searchRecipes(searchModel)
    .subscribe(response => {
      this.recipes = response;
      }, error => {
        console.log(error);               
    })

  }
  //   this.recipeService.getTopRecipes().subscribe(recipes => {
  //     this.recipes = recipes;
  //   }, error => {
  //     console.log(error);
  //   })
  // }
}
