import { Component, OnInit } from '@angular/core';
import { Cookbook } from 'src/app/_models/cookbook';
import { CookbookService } from 'src/app/_services/cookbook.service'
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cookbook-list',
  templateUrl: './cookbook-list.component.html',
  styleUrls: ['./cookbook-list.component.css']
})
export class CookbookListComponent implements OnInit {
  cookbooks: Cookbook[];
  alertText: string;

  constructor(private cookbookService: CookbookService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.loadCookbooks();
  }

  loadCookbooks(){
    this.cookbookService.getCookbooks().subscribe(cookbooks => { 
      if(cookbooks?.length !== undefined){      
        this.cookbooks = cookbooks; 
      } else {
        this.router.navigateByUrl('/recipes'); 
        console.log(cookbooks);        
        this.toastr.error('An error occurred, please try again.');
      }
    }, error => {
      console.log(error);
      this.router.navigateByUrl('/recipes');     
      this.toastr.error('An error occurred, please try again.');
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
