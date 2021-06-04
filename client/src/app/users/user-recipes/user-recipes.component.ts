import { Component, OnInit } from '@angular/core';
import { Recipe } from 'src/app/_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { UsersService } from 'src/app/_services/users.service';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-user-recipes',
  templateUrl: './user-recipes.component.html',
  styleUrls: ['./user-recipes.component.css']
})
export class UserRecipesComponent implements OnInit {
  userRecipes: Recipe[];
  user: User;
  addNewMode:boolean = false;

  constructor(private userService:UsersService, private accountService:AccountService) { 

    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadUserRecipes();
  }

  loadUserRecipes(){
//debugger;
    console.log(this.user);
    this.userService.getUserRecipes(this.user.id).subscribe(userRecipes=>{
      this.userRecipes = userRecipes;
      debugger;
    }, error =>{
      console.log(error);
    })
  }

  addRecipe(){
    //debugger;
    //this.router.navgateURL(['/recipes']);
    this.addNewMode = !this.addNewMode;
  }

}
