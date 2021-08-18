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

  newCookbookName: string = null; 

  constructor(public bsModalRef: BsModalRef, private toastr: ToastrService, private cookbookService:CookbookService) { }

  ngOnInit(): void {
  }

  confirmAddingIngredients(){
    debugger;
    //create cookbook and do relations
    //this.cookbookService //czy tu powinna byc jakaś metoda create popatrz nak w przypadku listy zrobiłam
    var recipes = [];
    recipes.push(this.currentRecipe);
    var cookbookToCreate : Cookbook = {id: null, name: this.newCookbookName, description: null, userId: this.userId, cookbookRecipes: recipes};
    this.cookbookService.addCookbook(cookbookToCreate)
    .subscribe(response => {
      this.toastr.success('Cookbook created and rcipe added successfully!');
    }, error => {
      console.log(error);
    })
    this.bsModalRef.hide();
  }

  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }

}
