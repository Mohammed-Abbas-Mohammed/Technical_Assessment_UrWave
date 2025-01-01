
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from './components/category/category.component';
import { ProductComponent } from './components/product/product.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { OverviewComponent } from './components/overview/overview.component';

export const routes: Routes = [
  { path: 'categories', component: CategoryComponent },
  { path: 'products', component: ProductComponent },
  {path: 'dashboard',  component: DashboardComponent, 
    children: [
      { path: '', component: OverviewComponent }, // Default route to Overview
      { path: 'products', component: ProductComponent }, // Manage Products
      { path: 'categories', component: CategoryComponent }, // Manage Categories
    ]
  },
  { path: '', redirectTo: '/products', pathMatch: 'full' } // Default route
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

