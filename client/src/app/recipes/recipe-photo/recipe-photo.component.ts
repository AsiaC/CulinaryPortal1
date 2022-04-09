import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Photo } from 'src/app/_models/photo';
import { RecipesService } from 'src/app/_services/recipes.service';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-recipe-photo',
  templateUrl: './recipe-photo.component.html',
  styleUrls: ['./recipe-photo.component.css']
})
export class RecipePhotoComponent implements OnInit {
  recipePhotos: Photo[];
  recipeId: number;
  file: File = null; // Variable to store the file
  bsModalRef: BsModalRef;
  alertText: string;

  constructor(private recipeService: RecipesService, private route: ActivatedRoute, private toastr: ToastrService, private modalService: BsModalService, private router: Router) { }

  ngOnInit(): void {
    this.loadRecipePhotos();
  }

  loadRecipePhotos(){
    this.recipeId = Number(this.route.snapshot.paramMap.get('id'))
    this.recipeService.getRecipePhotos(this.recipeId).subscribe(recipePhotosResponse => {
      if(recipePhotosResponse?.length !== undefined){
        this.recipePhotos = recipePhotosResponse;
      } else {        
        if(recipePhotosResponse.error.status === 401){
          this.toastr.error('You do not have access to this content.');  
        } else if(recipePhotosResponse.error.status === 404){
          this.toastr.error('No photos found.');          
        } else {               
          this.toastr.error('An error occurred, please try again.');  
        }
      }        
    })  
  }

  // On file Select
  onChange(event) {
    this.file = event.target.files[0];
  }

  // OnClick of button Upload
  onUpload() {
    const uploadData = new FormData();
    uploadData.append('upload', this.file);
    this.recipeService.addPhoto(this.recipeId, uploadData).subscribe(response => {
      if(response.status === 200 ){ 
        this.toastr.success('Photo added successfully!');
        this.loadRecipePhotos();
      } else {
        this.toastr.error('Error! Photo cannot be added.');
        console.log(response);
      }   
    })
  }  
  
  setAsMainPhoto(photoId: number){
    this.recipeService.updateMainRecipePhoto(photoId, this.recipeId).subscribe(response => {
      if(response.status === 200 ){ 
        this.toastr.success('Main photo changed successfully!');
        this.loadRecipePhotos();
      } else {
        this.toastr.error('Error! Main photo cannot be changed.');
        console.log(response);
      }   
    })
  }

  deletePhoto(photoId: number){
    this.recipeService.deletePhoto(photoId, this.recipeId).subscribe(response => {
      if(response.status === 200 ){ 
        this.toastr.success('Photo removed successfully!');
        this.loadRecipePhotos();
      } else {
        this.toastr.error('Error! Photo cannot be removed.');
        console.log(response);
      }               
    })
  }

  back(){
    this.router.navigateByUrl('/recipes/'+ this.recipeId);
  }
}
