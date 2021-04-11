import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../_models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  baseUrl = 'https://localhost:5001/api/';
  products: Product = { name: 'laptop', description: 'asus tuf gaming', quantity: 5 };

  constructor(private http: HttpClient) { }

  getProducts() {
    return this.products;
  }
}
