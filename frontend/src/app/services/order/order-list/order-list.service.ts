import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Orders } from '../../../models/orders';
import { Response } from 'src/app/models/response';

@Injectable({
  providedIn: 'root'
})
export class OrderListService {
  private myBackendUrl = 'https://localhost:44390/';
  private myApiUrl = 'order/';
  public list: Orders[] = [];

  constructor(private http: HttpClient) { }

  listOrders() {
    this.http.get(this.myBackendUrl + this.myApiUrl + 'list')
      .toPromise().then(resp => {
        this.list = (resp as Response).data as Orders[]
      });
  }
}
