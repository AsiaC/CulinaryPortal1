import { Component, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Cookbook } from 'src/app/_models/cookbook';
import { CookbookRecipe } from 'src/app/_models/cookbookRecipe';
import { Recipe } from 'src/app/_models/recipe';
import { CookbookService } from 'src/app/_services/cookbook.service';


@Component({
  selector: 'app-create-cookbook',
  templateUrl: './create-cookbook.component.html',
  styleUrls: ['./create-cookbook.component.css']
})
export class CreateCookbookComponent implements OnInit {
  title: string;
  userId: number;
  closeBtnName: string;
  submitBtnName: string;
  currentRecipe: CookbookRecipe;
  userCookbook: Cookbook;

  newCookbookName: string = null; 

  constructor(public bsModalRef: BsModalRef, private toastr: ToastrService, private cookbookService:CookbookService) { }

  ngOnInit(): void {
  }

  confirmAddingIngredients(){
    //create cookbook and do relations
    //this.cookbookService //czy tu powinna byc jakaś metoda create popatrz nak w przypadku listy zrobiłam
    var recipes = [];
    recipes.push(this.currentRecipe);
    //var cookbookToCreate : Cookbook = {id: null, name: this.newCookbookName, description: null, userId: this.userId, cookbookRecipes: recipes};
    var cookbookToCreate : Cookbook = {id: null, name: this.newCookbookName, userId: this.userId, cookbookRecipes: [], userName: '', isRecipeAdded: true};
    
    this.cookbookService.addCookbook(cookbookToCreate)
    .subscribe(userCookbook => {
      this.userCookbook = userCookbook;          
       debugger;
      //this.currentRecipe.cookbookId = userCookbook.id;    
      var cookbookToUpdate : Cookbook = {id: userCookbook.id, name: this.newCookbookName, userId: this.userId, cookbookRecipes: recipes, userName: '', isRecipeAdded: true};
      debugger;
      this.cookbookService.updateCookbook(userCookbook.id, cookbookToUpdate)
      //this.cookbookService.updateCookbook(this.currentRecipe.cookbookId, this.currentRecipe) //TODO check if IsAdded: true is needed or not 
      .subscribe(response => {
        debugger;
        this.toastr.success('Cookbook created and recipe added successfully!');
        })
    }, error => {      
      console.log(error);
    })
    this.bsModalRef.hide();
  }

  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }

}
