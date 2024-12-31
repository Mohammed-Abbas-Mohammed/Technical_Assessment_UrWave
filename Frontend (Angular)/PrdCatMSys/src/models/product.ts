import { Category } from "./category";
import { EntityStatus } from "./Status";


export interface Product {
    id: string; 
    name: string;
    description: string;
    price: number;
    categoryId: string; 
    category: Category;
    imageUrl?: string; 
    stockQuantity: number;
    createdDate?: Date;
    updatedDate?: Date;
    status: EntityStatus; 
  }