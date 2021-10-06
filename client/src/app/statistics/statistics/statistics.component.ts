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
  top5Recipes: Recipe[]; 
  allCategories: Category[];
  users: User[];

  constructor(private recipeService: RecipesService, private userService: UsersService) { }

  ngOnInit(): void {    
    this.getRecipes();
    this.getCategories();
    this.getUsers();
  }  

  getRecipes(){
    this.recipeService.getRecipes().subscribe(recipes => {     
      var sortRecipes = recipes.sort(function(a, b) {
        return a.totalScore - b.totalScore;
      });
      var reverseRecipes = sortRecipes.reverse();
      this.top5Recipes = reverseRecipes.slice(0,5);
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
