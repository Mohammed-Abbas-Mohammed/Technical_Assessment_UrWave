import { Product } from "./product";
import { EntityStatus } from "./Status";

export interface Category {
    id: string; 
    name: string;
    description: string;
    parentCategoryId: string;
    parentCategory: Category; 
    products: Product[];
    subCategories: Category[];
    createdDate?: Date;
    updatedDate?: Date;
    status: EntityStatus; 
  }