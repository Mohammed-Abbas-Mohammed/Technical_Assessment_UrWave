
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from './components/category/category.component';
import { ProductComponent } from './components/product/product.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

import { HomeComponent } from './components/home/home.component';
import { OverviewComponent } from './components/dashboard/overview/overview.component';
import { ProductsComponent } from './components/dashboard/products/products.component';
import { CategoriesComponent } from './components/dashboard/categories/categories.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'categories', component: CategoryComponent },
  { path: 'products', component: ProductComponent },
  {path: 'dashboard',  component: DashboardComponent, 
    children: [
      { path: '', component: OverviewComponent }, // Default route to Overview
      { path: 'productsManage', component: ProductsComponent }, // Manage Products
      { path: 'categoriesManage', component: CategoriesComponent }, // Manage Categories
    ]
  },
  { path: '', redirectTo: 'home', pathMatch: 'full' } // Default route
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

