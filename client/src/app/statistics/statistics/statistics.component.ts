import { Component, OnInit } from '@angular/core';
import { Recipe } from 'src/app/_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { User } from 'src/app/_models/user';
import { UsersService } from 'src/app/_services/users.service';
import { Category } from 'src/app/_models/category';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css']
})
export class StatisticsComponent implements OnInit {
  numberOfRegisteredUsers: number;
  top6Recipes: Recipe[]; 
  allCategories: Category[];
  users: User[];

  constructor(private recipeService: RecipesService, private userService: UsersService) { }

  ngOnInit(): void {    
    this.searchRecipes();
    this.getCategories();
    this.getUsers();
  }  
  
  searchRecipes(){
    var searchModel = {name: null, categoryId: null, difficultyLevelId: null, preparationTimeId: null, userId: null, top: 6}
    this.recipeService.searchRecipes(searchModel)
    .subscribe(response => {
      this.top6Recipes = response;
      }, error => {
        console.log(error);               
    })
  }

  getCategories(){
    this.recipeService.getCategories().subscribe(allCategories => {
      this.allCategories = allCategories;
    }, error =>{
      console.log(error);
    })
  }

  getUsers(){
    this.userService.getUsers().subscribe(users => {
      this.users = users;
      this.numberOfRegisteredUsers = users.length;
    }, error => {
      console.log(error);
    })
  }
}
