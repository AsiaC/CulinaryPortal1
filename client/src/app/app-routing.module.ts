import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CookbookListComponent } from './cookbooks/cookbook-list/cookbook-list.component';
import { HomeComponent } from './home/home.component';
import { RecipeDetailComponent } from './recipes/recipe-detail/recipe-detail.component';
import { RecipeListComponent } from './recipes/recipe-list/recipe-list.component';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { UserRecipesComponent} from './users/user-recipes/user-recipes.component';
import { UserCookbookComponent } from './users/user-cookbook/user-cookbook.component';
import { UserShoppingListsComponent} from './users/user-shopping-lists/user-shopping-lists.component';
import { ShopingListListComponent } from './shopping-lists/shoping-list-list/shoping-list-list.component';

const routes: Routes = [
{path:'', component: HomeComponent},
{
  path: '',
  runGuardsAndResolvers: 'always',
  canActivate: [AuthGuard],
  children:
  [
    {path:'recipes', component: RecipeListComponent},
    {path:'recipes/:id', component: RecipeDetailComponent},
    {path:'cookbooks', component: CookbookListComponent},
    {path: 'user/edit', component: UserEditComponent, canDeactivate: [PreventUnsavedChangesGuard]},
    {path: 'user/recipes', component: UserRecipesComponent},
    {path: 'user/cookbook', component: UserCookbookComponent},
    {path: 'user/shoppingLists', component: UserShoppingListsComponent},
    {path: 'shoppingLists', component: ShopingListListComponent},
  ]
},
{path:'**', component: HomeComponent, pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
