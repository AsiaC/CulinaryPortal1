import { Component, OnInit } from '@angular/core';
import { Cookbook } from 'src/app/_models/cookbook';
import { CookbookService } from 'src/app/_services/cookbook.service'

@Component({
  selector: 'app-cookbook-list',
  templateUrl: './cookbook-list.component.html',
  styleUrls: ['./cookbook-list.component.css']
})
export class CookbookListComponent implements OnInit {
  cookbooks: Cookbook[];

  constructor(private cookbookService: CookbookService) { }

  ngOnInit(): void {
    this.loadCookbooks();
  }

  loadCookbooks(){
    debugger;
    this.cookbookService.getCookbooks().subscribe(cookbooks => {
      this.cookbooks = cookbooks;
    }, error => {
      console.log(error);
    })
  }

}
