import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RecipesService } from 'src/app/_services/recipes.service';
import {Category} from 'src/app/_models/category';

@Component({
  selector: 'app-recipe-new-form',
  templateUrl: './recipe-new-form.component.html',
  styleUrls: ['./recipe-new-form.component.css']
})
export class RecipeNewFormComponent implements OnInit {

  model: any = {};
  allCategories: Category[];

  constructor(private recipesService: RecipesService) { }

  ngOnInit(): void {
    this.getAllCategories();
  }

  getAllCategories(){debugger;
    this.recipesService.getCategories().subscribe(allCategories => {
      this.allCategories = allCategories;
      debugger;
      console.log(allCategories);
    }, error =>{
      console.log(error);
    })
  }

  createNewRecipe(){
    debugger;
    console.log("create new recipe");
    // this.recipesService.addRecipe(this.model).subscribe(response => {
    //   console.log(response);
    //   this.cancel();  
    // })
  }

  //pod przyciskiem cancel
  cancel(){
    console.log("cancel");
    //debugger;
    //this.cancelRegister.emit(false);
  }

}
