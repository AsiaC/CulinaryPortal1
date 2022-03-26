import { Component, OnInit } from '@angular/core';
import { Cookbook } from 'src/app/_models/cookbook';
import { CookbookService } from 'src/app/_services/cookbook.service'
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cookbook-list',
  templateUrl: './cookbook-list.component.html',
  styleUrls: ['./cookbook-list.component.css']
})
export class CookbookListComponent implements OnInit {
  cookbooks: Cookbook[];
  alertText: string;

  constructor(private cookbookService: CookbookService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadCookbooks();
  }

  loadCookbooks(){
    this.cookbookService.getCookbooks().subscribe( 
      cookbooks => { this.cookbooks = cookbooks; },
      error => {
      if(error.status === 401){
        this.alertText = "You do not have access to this content.";
      } else if(error.status === 404){
        this.alertText = "Users do not have any cookbooks yet."
      }
    })
  }

  deleteCookbook(cookbookId: number){
    this.cookbookService.deleteCookbook(cookbookId).subscribe(response => {
      this.loadCookbooks(); 
      if(response.status === 200){
        this.toastr.success('Cookbook removed successfully!');        
      } else {
        this.toastr.error('Error! Cookbook cannot be removed.'); 
      }
    }, error => {
      this.toastr.error('Error! Cookbook cannot be removed.'); 
      console.log(error);                      
    })
  }

}
