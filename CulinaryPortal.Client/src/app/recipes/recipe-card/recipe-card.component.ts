import { Component, Input, OnInit } from '@angular/core';
import { Recipe } from 'src/app/_models/recipe';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { DeleteRecipeComponent } from 'src/app/modals/delete-recipe/delete-recipe.component';

@Component({
  selector: 'app-recipe-card',
  templateUrl: './recipe-card.component.html',
  styleUrls: ['./recipe-card.component.css']
})
export class RecipeCardComponent implements OnInit {
  // Recipe received from the parent comnponent (recipe-list component)
  @Input() recipeFromList: Recipe;
  bsModalRef: BsModalRef;

  constructor(private modalService: BsModalService) { }

  ngOnInit(): void {
  }

  deleteRecipe(recipeId){
    const initialState = {  
      title: 'Are you sure that you would like to delete indicated recipe?',      
      idToRemove: recipeId,
      objectName:'Recipe'
    };
    this.bsModalRef = this.modalService.show(DeleteRecipeComponent, {initialState});
    this.bsModalRef.content.closeBtnName = 'Cancel'; 
    this.bsModalRef.content.submitBtnName = 'Confirm deleting recipe';    
  }
}
