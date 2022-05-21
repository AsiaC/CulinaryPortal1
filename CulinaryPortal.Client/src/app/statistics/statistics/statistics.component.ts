import { Component, OnInit } from '@angular/core';
import { Recipe } from 'src/app/_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { User } from 'src/app/_models/user';
import { UsersService } from 'src/app/_services/users.service';
import { Category } from 'src/app/_models/category';
import { ToastrService } from 'ngx-toastr';

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

  constructor(private recipeService: RecipesService, private userService: UsersService, private toastr: ToastrService) { }

  ngOnInit(): void {    
    this.searchRecipes();
    this.getCategories();
    this.getUsers();
  }  
  
  searchRecipes(){    
    var dict = { top: 6 };    

    this.recipeService.searchRecipes(dict).subscribe(recipesResponse => {
      if(recipesResponse?.length !== undefined){
        this.top6Recipes = recipesResponse;
      } else {                      
        this.toastr.error('An error occurred, please try again.');  
        console.log(recipesResponse);
      }
    })
  }

  getCategories(){
    this.recipeService.getCategories().subscribe(allCategoriesResponse => {
      if(allCategoriesResponse?.length !== undefined){
        this.allCategories = allCategoriesResponse;
      } else {        
        if(allCategoriesResponse.error.status === 401){
          this.toastr.error('You do not have access to this content.');  
        } else if(allCategoriesResponse.error.status === 404){
          this.toastr.error('No categories found.');          
        } else {               
          this.toastr.error('An error occurred, please try again.');  
        }
      }
    })
  }

  getUsers(){
    this.userService.getUsers().subscribe(usersResponse => {
      if(usersResponse?.length !== undefined){
        this.users = usersResponse;
        this.numberOfRegisteredUsers = this.users.length;
      } else { 
        this.toastr.error('An error occurred while loading users, please try again.');     
        console.log(usersResponse.error.status);      
      }
    })
  }
}
