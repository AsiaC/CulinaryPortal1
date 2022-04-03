import { Component, Input, OnInit } from '@angular/core';
import { Recipe } from 'src/app/_models/recipe';

@Component({
  selector: 'app-recipe-card',
  templateUrl: './recipe-card.component.html',
  styleUrls: ['./recipe-card.component.css']
})
export class RecipeCardComponent implements OnInit {
  // Recipe received from the parent comnponent (recipe-list component)
  @Input() recipeFromList: Recipe;

  constructor() { }

  ngOnInit(): void {
  }
}
