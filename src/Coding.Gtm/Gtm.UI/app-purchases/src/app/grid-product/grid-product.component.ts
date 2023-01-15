import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductModel } from '../services/product/read-all/models/product-model';
import { ReadAllResponse } from '../services/product/read-all/models/response/read-all-response';
import { ReadAllService } from '../services/product/read-all/read-all-service';

@Component({
  selector: 'app-grid-product',
  templateUrl: './grid-product.component.html',
  styleUrls: ['./grid-product.component.scss']
})
export class GridProductComponent implements OnInit {
  displayedColumns: string[] = ['id', 'description'];
  dataSource = [];

  constructor(private router: Router, private readAllService: ReadAllService) { }

  ngOnInit(): void {
    this.readAllService.readAll().subscribe({
      next: (response: ReadAllResponse) => {
        var list = [];

        for(var i = 0; i < response.products.length;i++){          
          list.push(response.products[i]);
        }

        this.dataSource = list;
      },
      error: () => { alert("There was a problem with your request.") }
   })
  }

  row_click(product: ProductModel) {
    this.router.navigate(['/product-detail/' + product.id]);
  }

}
