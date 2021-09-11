import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { RecipeListComponent } from './recipes/recipe-list/recipe-list.component';
import { RecipeDetailComponent } from './recipes/recipe-detail/recipe-detail.component';
import { RecipeCardComponent } from './recipes/recipe-card/recipe-card.component';
import { CookbookListComponent } from './cookbooks/cookbook-list/cookbook-list.component';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { ToastrModule } from 'ngx-toastr';
import { UserRecipesComponent } from './users/user-recipes/user-recipes.component';
import { UserCookbookComponent } from './users/user-cookbook/user-cookbook.component';
import { RecipeNewFormComponent } from './recipes/recipe-new-form/recipe-new-form.component';
import { UserShoppingListsComponent } from './users/user-shopping-lists/user-shopping-lists.component';
import { ShoppingListNewFormComponent } from './shopping-lists/shopping-list-new-form/shopping-list-new-form.component';
import { SelectShoppingListComponent } from './modals/select-shopping-list/select-shopping-list.component';
import { ConfirmComponent } from './modals/confirm/confirm.component';
import { ShopingListListComponent } from './shopping-lists/shoping-list-list/shoping-list-list.component';
import { InformComponent } from './modals/inform/inform.component';
import { CreateCookbookComponent } from './modals/create-cookbook/create-cookbook.component';
import { UserListComponent } from './users/user-list/user-list.component';
import { RecipePhotoComponent } from './recipes/recipe-photo/recipe-photo.component';

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
    RecipeNewFormComponent,
    UserShoppingListsComponent,
    ShoppingListNewFormComponent,
    SelectShoppingListComponent,
    ConfirmComponent,
    ShopingListListComponent,
    InformComponent,
    CreateCookbookComponent,
    UserListComponent,
    RecipePhotoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, 
    HttpClientModule, 
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
