import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Cookbook } from 'src/app/_models/cookbook';
import { CookbookRecipe } from 'src/app/_models/cookbookRecipe';
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

  confirmCreatingCookbook(){
    // Create cookbook and create the relationship    
    var recipes = [];
    if(this.currentRecipe != null){
      recipes.push(this.currentRecipe);  
    }
    var cookbookToCreate : Cookbook = {id: null, name: this.newCookbookName, userId: this.userId, cookbookRecipes: [] };
    
    this.cookbookService.addCookbook(cookbookToCreate).subscribe(userCookbook => {
      this.userCookbook = userCookbook;          
      if(userCookbook !== undefined){
        if(this.currentRecipe != null){
          this.currentRecipe.cookbookId = userCookbook.id;         
          this.cookbookService.updateCookbook(userCookbook.id, this.currentRecipe).subscribe(response => {
            if(response.status === 200 ){
              this.toastr.success('Cookbook created and recipe added successfully!');          
            } else {
              this.toastr.error('Error! Cookbook and recipe cannot be added.');
              console.log(response);
            }})
        } else {
          this.toastr.success('Cookbook created successfully!'); 
          window.location.reload();
        }
      } else {
        this.toastr.error('Error! Cookbook cannot be added.');
        console.log('Error! Cookbook cannot be added.');
      }
    })
    this.bsModalRef.hide();
  }

  cancelCreatingCookbook(){
    window.location.reload();
    this.bsModalRef.hide()
  }
 

  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }

}
