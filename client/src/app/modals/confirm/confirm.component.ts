import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RecipesService } from 'src/app/_services/recipes.service';
import { UsersService } from 'src/app/_services/users.service';
import { InformComponent } from 'src/app/modals/inform/inform.component';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

//TODO komponent jest używany? Jeśli nie to usun
@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.css']
})
export class ConfirmComponent implements OnInit {
  title: string;
  closeBtnName: string;
  submitBtnName: string;
  idToRemove: number;
  objectName: string;
  userId: number;
  
  message: string;

  constructor(public bsModalRef: BsModalRef, private toastr: ToastrService, private recipeService: RecipesService, private userService:UsersService, private modalService: BsModalService) { }

  ngOnInit(): void {    
  }
  
  confirm(): void {
    this.message = this.objectName+' successfully deleted!';

    if(this.objectName ==='Recipe'){
      this.recipeService.deleteRecipe(this.idToRemove)
      .subscribe(response => {      
        //this.toastr.success('Recipe removed successfully!');//to do do sprawdzenia   
        //jakieś kilka sekund opóźnienia? albo zamiast toast to modal potwierdzajacy?
        //window.location.href='http://localhost:4200/recipes'; 
        //window.location.reload();
        const initialState = {  
          message: 'Recipe removed successfully!',                
          objectName:'Recipe'
        };
        this.bsModalRef = this.modalService.show(InformComponent, {initialState});
      }, error => {
          console.log(error);
      })
    }    

    this.bsModalRef.hide();
    
  }
 
  decline(): void {         
    this.bsModalRef.hide()      
  }

}
