import { Component, OnInit } from '@angular/core';
import { Recipe } from 'src/app/_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { UsersService } from 'src/app/_services/users.service';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { DifficultyLevelEnum } from 'src/app/_models/difficultyLevelEnum';
import { PreparationTimeEnum } from 'src/app/_models/preparationTimeEnum';
import { ToastrService } from 'ngx-toastr';
import { SearchRecipe } from 'src/app/_models/searchRecipe';

@Component({
  selector: 'app-user-recipes',
  templateUrl: './user-recipes.component.html',
  styleUrls: ['./user-recipes.component.css']
})
export class UserRecipesComponent implements OnInit {
  userRecipes: Recipe[];
  user: User;
  addNewMode:boolean = false;
  searchByName: string = null; 
  selectOptionVal: any;
  allCategories: any[] = [];
  difficultyLevel = DifficultyLevelEnum;
  difficultyLevelKeys = [];
  preparationTime = PreparationTimeEnum;
  preparationTimeKeys = [];
  searchModel : SearchRecipe = null;
  isNoResults: boolean = false;
  selectedDifficultyLevel: any;
  selectedPreparationTime: any;

  constructor(private userService:UsersService, private accountService:AccountService, private toastr: ToastrService, private recipeService: RecipesService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    this.difficultyLevelKeys = Object.keys(this.difficultyLevel).filter(k => !isNaN(Number(k))).map(Number);
    this.preparationTimeKeys = Object.keys(this.preparationTime).filter(k => !isNaN(Number(k))).map(Number);       
  }

  ngOnInit(): void {
    this.loadUserRecipes();
    this.getAllCategories();
    console.log(this.userRecipes);
  }

  loadUserRecipes(){
    console.log(this.user);
    this.userService.getUserRecipes(this.user.id).subscribe(userRecipes=>{
      this.userRecipes = userRecipes;  
      console.log(this.userRecipes);
    }, error =>{ 
      if(error.status === 404){
        this.userRecipes = undefined;
      } 
    })
  }
  getAllCategories(){
    this.recipeService.getCategories().subscribe(allCategories => {
      this.allCategories = allCategories;
    }, error =>{
      console.log(error);
    })
  }

  addRecipe(){
    this.addNewMode = !this.addNewMode;
  }

  searchRecipes(){  
    this.searchModel = {name: this.searchByName, categoryId: Number(this.selectOptionVal), difficultyLevelId: Number(this.selectedDifficultyLevel), preparationTimeId: Number(this.selectedPreparationTime), userId: this.user.id, top: null}

    this.userService.searchUserRecipes(this.searchModel, this.user.id)
    .subscribe(response => {
      this.isNoResults = false; 
      this.userRecipes = response;
      this.toastr.success('Recipes filtered.');  
      }, error => {
        console.log(error);   
        this.isNoResults = true;                   
    })
  }
  clearSearch(){
    this.loadUserRecipes();
    this.isNoResults = false;        
  }
  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }
  onChange(event): number {
    return event;
  }

}
