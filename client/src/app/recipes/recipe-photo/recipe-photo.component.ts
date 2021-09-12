import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Photo } from 'src/app/_models/photo';
import { Recipe } from 'src/app/_models/recipe';
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
  file: File = null; // Variable to store file
  //loading: boolean = false; // Flag variable
  bsModalRef: BsModalRef;

  constructor(private recipeService: RecipesService, private route: ActivatedRoute, private toastr: ToastrService, private modalService: BsModalService) { }

  ngOnInit(): void {
    this.loadRecipePhotos();
  }

  loadRecipePhotos(){
    //debugger;

    this.recipeId = Number(this.route.snapshot.paramMap.get('id'))
    console.log();
    this.recipeService.getRecipePhotos(this.recipeId).subscribe(recipePhotos=>{
      this.recipePhotos = recipePhotos;
      //debugger;
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

  // editDescription(photo: Photo){
  //   debugger;
  //   const initialState = {
  //     title: 'Edit photo description',
  //     closeBtnName: 'Cancel',
  //     submitBtnName: 'Confirm change',
  //     photoData: photo
  //   }
  //   this.bsModalRef = this.modalService.show(EditPhotoDescriptionComponent, {initialState})
  // }
  // changeMainPhoto(){
  //   debugger;
  //   const initialState = {
  //     title: 'Change main photo',
  //     closeBtnName: 'Cancel',
  //     submitBtnName: 'Confirm',
  //     list: this.recipePhotoShoppingLists,
  //   }
  //   this.bsModalRef = this.modalService.show(, {initialState})
     
  // }
  setAsMainPhoto(photoId: number){
    debugger;
    this.recipeService.updateMainRecipePhoto(photoId, this.recipeId)
      .subscribe(response => {
        this.toastr.success('Main photo changed successfully!');
        this.loadRecipePhotos(); 
      }, error => {
        console.log(error);                      
      })
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
