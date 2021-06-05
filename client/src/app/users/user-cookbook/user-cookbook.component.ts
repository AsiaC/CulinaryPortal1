import { Component, OnInit } from '@angular/core';
import { Cookbook } from 'src/app/_models/cookbook';
import { UsersService } from 'src/app/_services/users.service';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { Recipe } from 'src/app/_models/recipe';
import { CookbookService } from 'src/app/_services/cookbook.service';
import { ToastrService } from 'ngx-toastr';
import { DecimalPipe } from '@angular/common';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-user-cookbook',
  templateUrl: './user-cookbook.component.html',
  styleUrls: ['./user-cookbook.component.css']
})
export class UserCookbookComponent implements OnInit {
  userCookbook: Cookbook;
  user: User;
  cookbookRecipe: any = {recipeId: null, userId: null};  

  constructor(private userService:UsersService, private accountService:AccountService,  private cookbookService:CookbookService, private toastr: ToastrService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadUserCookbook();
  }

  loadUserCookbook(){
    this.userService.getUserCookbook(this.user.id).subscribe(userCookbook => {
      this.userCookbook = userCookbook;
      console.log(this.userCookbook);
    }, error => {
      console.log(error);
    })
  }

  removeFromCookbook(recipeId){    debugger;
    this.cookbookRecipe.recipeId = recipeId;
    this.cookbookRecipe.userId = this.user.id;

    this.cookbookService.removeRecipeFromCookbook(this.cookbookRecipe)
    .subscribe(response => {
      console.log("success");
      this.toastr.success('Recipe removed successfully');
      this.loadUserCookbook();
    }, error => {
        console.log(error);
    })
  }

}
