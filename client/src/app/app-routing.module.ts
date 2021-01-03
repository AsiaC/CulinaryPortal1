import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CookbookListComponent } from './cookbook/cookbook-list/cookbook-list.component';
import { HomeComponent } from './home/home.component';
import { RecipeDetailComponent } from './recipes/recipe-detail/recipe-detail.component';
import { RecipeListComponent } from './recipes/recipe-list/recipe-list.component';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { AuthGuard } from './_guards/auth.guard';


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
    {path:'cookbook', component: CookbookListComponent},
    {path: 'user/edit', component: UserEditComponent}
  ]
},
{path:'**', component: HomeComponent, pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
