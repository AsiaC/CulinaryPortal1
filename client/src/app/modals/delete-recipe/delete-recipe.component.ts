import { Component, OnInit } from '@angular/core';
import { RecipesService } from 'src/app/_services/recipes.service';
import { InformComponent } from 'src/app/modals/inform/inform.component';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-confirm',
  templateUrl: './delete-recipe.component.html',
  styleUrls: ['./delete-recipe.component.css']
})
export class DeleteRecipeComponent implements OnInit {
  title: string;
  closeBtnName: string;
  submitBtnName: string;
  idToRemove: number;
  objectName: string;
  userId: number;
  
  message: string;

  constructor(public bsModalRef: BsModalRef, private recipeService: RecipesService, private modalService: BsModalService) { }

  ngOnInit(): void {    
  }
  
  confirm(): void {
    this.message = this.objectName + ' successfully deleted!';

    if(this.objectName ==='Recipe'){
      this.recipeService.deleteRecipe(this.idToRemove)
      .subscribe(response => {    
        var initialState = {  
          message: '',                
          objectName:''
        };
        if(response.status === 200 ){           
          initialState = {  
            message: 'Recipe removed successfully!',                
            objectName:'Recipe'
          };          
        } else {
          initialState = {  
            message: 'Error! Recipe cannot be removed.',                
            objectName:'Recipe'
          };          
          console.log(response);          
        }    
        this.bsModalRef = this.modalService.show(InformComponent, {initialState});
      })
    }    
    this.bsModalRef.hide();    
  }
 
  decline(): void {         
    this.bsModalRef.hide()      
  }
}
