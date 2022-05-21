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
import { UserListComponent } from './users/user-list/user-list.component';
import { RecipePhotoComponent } from './recipes/recipe-photo/recipe-photo.component';
import { StatisticsComponent } from './statistics/statistics/statistics.component';
import { AdminGuard } from './_guards/admin.guard';

const routes: Routes = [
{path: '', component: HomeComponent},
{path: 'recipes', component: RecipeListComponent},
{path: 'recipes/:id', component: RecipeDetailComponent}, //TODO PreventUnsavedChangesGuard
{path: 'recipes/:id/photos', component: RecipePhotoComponent}, //TODO PreventUnsavedChangesGuard
{
  path: '',
  runGuardsAndResolvers: 'always',
  canActivate: [AuthGuard],
  children:
  [    
    {path: 'user/edit', component: UserEditComponent, canDeactivate: [PreventUnsavedChangesGuard]},
    {path: 'user/recipes', component: UserRecipesComponent},
    {path: 'user/cookbook', component: UserCookbookComponent},
    {path: 'user/shoppingLists', component: UserShoppingListsComponent},
    {path: 'users', component: UserListComponent, canActivate: [AdminGuard]},
    {path: 'statistics', component:StatisticsComponent, canActivate: [AdminGuard]},
    {path: 'shoppingLists', component: ShopingListListComponent, canActivate: [AdminGuard]},
    {path: 'cookbooks', component: CookbookListComponent, canActivate: [AdminGuard]}
  ]
},
{path:'**', component: HomeComponent, pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
