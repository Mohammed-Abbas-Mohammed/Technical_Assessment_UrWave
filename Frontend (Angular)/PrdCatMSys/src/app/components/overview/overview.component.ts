import { Component } from '@angular/core';
import { StatiticsService } from '../../../services/statictics/statitics.service';

@Component({
  selector: 'app-overview',
  imports: [],
  templateUrl: './overview.component.html',
  styleUrl: './overview.component.css'
})
export class OverviewComponent {
  totalProducts: number = 0;
  totalCategories: number = 0;
  constructor(private statisticsService: StatiticsService) { }

  ngOnInit(): void {
    this.fetchCounts();
  }

  fetchCounts(): void {
    this.statisticsService.getTotalProductsCount().subscribe(
      (count) => {
        this.totalProducts = count;
      },
      (error) => {
        console.error('Error fetching product count:', error);
      }
    );

    this.statisticsService.getTotalCategoriesCount().subscribe(
      (count) => {
        this.totalCategories = count;
      },
      (error) => {
        console.error('Error fetching category count:', error);
      }
    );
  }
}
