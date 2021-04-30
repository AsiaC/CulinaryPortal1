import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { RecipeListComponent } from './recipes/recipe-list/recipe-list.component';
import { RecipeDetailComponent } from './recipes/recipe-detail/recipe-detail.component';
import { RecipeCardComponent } from './recipes/recipe-card/recipe-card.component';
import { CookbookListComponent } from './cookbooks/cookbook-list/cookbook-list.component';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import {ToastrModule} from 'ngx-toastr';
import { UserRecipesComponent } from './users/user-recipes/user-recipes.component';
import { UserCookbookComponent } from './users/user-cookbook/user-cookbook.component';
import { RecipeNewFormComponent } from './recipes/recipe-new-form/recipe-new-form.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    RegisterComponent,
    HomeComponent,
    RecipeListComponent,
    RecipeDetailComponent,
    RecipeCardComponent,
    CookbookListComponent,
    UserEditComponent,
    UserRecipesComponent,
    UserCookbookComponent,
    RecipeNewFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, 
    HttpClientModule, 
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
