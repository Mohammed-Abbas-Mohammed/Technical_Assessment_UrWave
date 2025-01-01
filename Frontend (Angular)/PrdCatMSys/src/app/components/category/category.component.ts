import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css'],

})
export class CategoryComponent implements OnInit {
  categories: any[] = [];

  constructor() {}

  ngOnInit(): void {
   // this.loadCategories();
  }

  // loadCategories(): void {
  //   this.categoryService.getCategories().subscribe((data) => {
  //     this.categories = data;
  //   });
  // }
}
