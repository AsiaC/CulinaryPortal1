import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Photo } from 'src/app/_models/photo';
import { Recipe } from 'src/app/_models/recipe';
import { RecipesService } from 'src/app/_services/recipes.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-recipe-photo',
  templateUrl: './recipe-photo.component.html',
  styleUrls: ['./recipe-photo.component.css']
})
export class RecipePhotoComponent implements OnInit {
  recipePhotos: Photo[];
  recipeId: number;
  file: File = null; // Variable to store file
  //loading: boolean = false; // Flag variable

  constructor(private recipeService: RecipesService, private route: ActivatedRoute, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadRecipePhotos();
  }

  loadRecipePhotos(){
    //debugger;

    this.recipeId = Number(this.route.snapshot.paramMap.get('id'))
    console.log();
    this.recipeService.getRecipePhotos(this.recipeId).subscribe(recipePhotos=>{
      this.recipePhotos = recipePhotos;
      debugger;
    }, error =>{       
      console.log(error);
    })
  }

  // On file Select
  onChange(event) {
    debugger;
    this.file = event.target.files[0];
  }
  // OnClick of button Upload
  onUpload() {
    debugger;
    //this.loading = !this.loading;
    console.log(this.file);
    const uploadData = new FormData();
    uploadData.append('upload', this.file);
    //uploadData.append('myFile', this.file, this.file.name);
    this.recipeService.addPhoto(this.recipeId,uploadData)
    .subscribe(response => {
      this.toastr.success('Photo added successfully!');
      this.loadRecipePhotos();
    }, error => {
      console.log(error);
    })
  }  

  editPhoto(photoId: number){

  }

  deletePhoto(photoId: number){
    this.recipeService.deletePhoto(photoId)
      .subscribe(response => {
        this.toastr.success('Photo removed successfully!');
        this.loadRecipePhotos(); 
      }, error => {
         console.log(error);                      
      })
  }
}
