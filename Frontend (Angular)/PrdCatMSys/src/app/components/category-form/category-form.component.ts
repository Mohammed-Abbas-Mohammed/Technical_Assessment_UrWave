import { Component,Input, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Category } from '../../../models/category';
import { EntityStatus } from '../../../models/Status';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-category-form',
  imports: [CommonModule, FormsModule ],
  templateUrl: './category-form.component.html',
  styleUrl: './category-form.component.css'
})
export class CategoryFormComponent {
  @Input() category: Category | null = null; // Input for the category to edit
  @Output() save = new EventEmitter<Category>();
  @Output() cancel = new EventEmitter<void>();

  formCategory: Category = {
    id: '',
    name: '',
    description: '',
    parentCategoryId: '',
    parentCategory: null!,
    products: [],
    subCategories: [],
    status: EntityStatus.Active,
  };

  ngOnChanges(): void {
    if (this.category) {
      this.formCategory = { ...this.category }; // Clone the category for the form
    }
  }

  onSubmit(): void {
    this.save.emit(this.formCategory); // Emit the save event with form data
  }
}
