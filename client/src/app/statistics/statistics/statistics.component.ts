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
  numberOfRecipesByCategories: Map<string,Recipe[]>;
  //numberOfRecipesByCategories: Array<Record<string,Recipe[]>>;
  //numberOfRecipesByCategories2; 
  numberOfRecipesByCategories3;

  numberOfRecipesByUsers;

  constructor(private recipeService: RecipesService, private userService: UsersService) { }

  ngOnInit(): void {
    this.getTopRecipes();
    this.getNumberOfUsers();
    this.getRecipesByCategories();
    this.getRecipesByUsers();
  }

  getTopRecipes(){
    this.recipeService.getTopRecipes().subscribe(top5Recipes => {
      this.top5Recipes = top5Recipes;
    }, error => {
      console.log(error);
    })
  }

  getNumberOfUsers(){
    this.userService.getNumberOfUsers().subscribe(numberOfRegisteredUsers => {
      this.numberOfRegisteredUsers = numberOfRegisteredUsers;  
      
      

    }, error => {
      console.log(error);
    })
  }

  getRecipesByCategories(){
    this.recipeService.getRecipesByCategories().subscribe(response => {
      this.numberOfRecipesByCategories = response;
      debugger;
      this.numberOfRecipesByCategories3 = Array.from(this.numberOfRecipesByCategories.values())
      //this.numberOfRecipesByCategories2 = Array.of(this.numberOfRecipesByCategories);       
      debugger;
    }, error => {
      console.log(error);
    })
  }

  getRecipesByUsers(){
    // this.recipeService.getRecipesByUsers().subscribe(response => {
    //   this.numberOfRecipesByUsers = response;
    //   //debugger;
    // }, error => {
    //   console.log(error);
    // })
  }
}
