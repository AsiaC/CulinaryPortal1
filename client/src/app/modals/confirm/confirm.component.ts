import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ShoppingListService } from 'src/app/_services/shoppingList.service';

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
  objectName:string;

  message: string;

  constructor(public bsModalRef: BsModalRef, private toastr: ToastrService, private shoppingListService: ShoppingListService) { }

  ngOnInit(): void {
  }
  
 
  confirm(): void { debugger;
    this.message = this.objectName+' successfully deleted!';
//TO DO USUN
    if(this.objectName === 'Shopping list'){      
      this.shoppingListService.deleteShoppingList(this.idToRemove)
      .subscribe(response => {
        debugger;
        console.log(response);
        //this.toastr.success(this.message);//czy jest sens skoro to sie zaraz przeaduje?   
        window.location.reload();     
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
