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

  constructor(private cookbookService: CookbookService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadCookbooks();
  }

  loadCookbooks(){
    this.cookbookService.getCookbooks().subscribe(cookbooks => {
      this.cookbooks = cookbooks;
    }, error => {
      console.log(error);
    })
  }

  deleteCookbook(cookbookId: number){
    this.cookbookService.deleteCookbook(cookbookId)
    .subscribe(response => {
      console.log(response);
      this.toastr.success('Cookbook removed successfully!');  
      this.loadCookbooks(); 
    }, error => {
       console.log(error);                      
    })
  }

}
