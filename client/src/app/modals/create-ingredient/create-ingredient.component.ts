import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Ingredient } from 'src/app/_models/ingredient';
import { RecipesService } from 'src/app/_services/recipes.service';

@Component({
  selector: 'app-create-ingredient',
  templateUrl: './create-ingredient.component.html',
  styleUrls: ['./create-ingredient.component.css']
})
export class CreateIngredientComponent implements OnInit {
  title: string;
  newIngredientName: string = null;
  closeBtnName: string;
  submitBtnName: string;

  constructor(public bsModalRef: BsModalRef, private recipeService: RecipesService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  confirmAddingNewIngredient(){
    debugger;
    var ingredientToCreate: Ingredient = {id: null, name: this.newIngredientName};
    this.recipeService.addIngredient(ingredientToCreate).subscribe(response => {
      debugger
      console.log(response)
      this.toastr.success('Ingredient added successfully!');
      //dodaj tą wartość do listy + ustaw
    }, error => {
      console.log(error);
    })
    this.bsModalRef.hide();
  }

  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }
  //dodaj jak cancel to ustaw na Choose.....
  cancelAddingNewIngredient(){
    this.bsModalRef.hide()
  }
}
