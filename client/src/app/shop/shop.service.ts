import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brands';
import { IPagination } from '../shared/models/pagination';
import { IType } from '../shared/models/productTypes';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts() {
    return this.http.get<IPagination>(this.baseUrl + 'products?search=green&sort=priceDesc');
  }
  getBrands(){
    return this.http.get<IBrand[]>(this.baseUrl+ 'products/brands');
  }
  getTypes(){
    return this.http.get<IType[]>(this.baseUrl+ 'products/types');
  }
}
