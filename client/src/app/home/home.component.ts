import { Component, OnInit } from '@angular/core';
import { Recipe } from '../_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode: boolean = false;
  users: any;
  recipes: Recipe[];

  constructor(private recipeService: RecipesService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.searchRecipes();
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }  

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }

  searchRecipes(){        
    var dict = { top: 6 };
    
    this.recipeService.searchRecipes(dict).subscribe(recipesResponse => {
      if(recipesResponse?.length !== undefined){
        this.recipes = recipesResponse;
      } else {    
        this.toastr.error('An error occurred, please try again.');  
        console.log(recipesResponse);
      }
    }, error => {
        this.toastr.error('An error occurred, please try again.');
        console.log(error);               
    })
  }
}
