import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Orders } from '../../../models/orders';
import { Response } from '../../../models/response';
import { OrderItems } from '../../../models/items';

@Injectable({
  providedIn: 'root'
})
export class OrderFormService {
  private myBackendUrl = 'https://localhost:44390/';
  private myApiUrl = 'order/';
  public list: OrderItems[] = [];

  constructor(private http: HttpClient) { }

  createOrder(order: Orders): Observable<Response> {
    return this.http.post<Response>(this.myBackendUrl + this.myApiUrl + 'create', order);
  }
  createOrderItem(orderItem: OrderItems): Observable<OrderItems> {
    return this.http.post<OrderItems>(this.myBackendUrl + 'orderitem/create', orderItem);
  }
  calculateOrder(orderId: number): Observable<Orders> {
    return this.http.get<Orders>(this.myBackendUrl + this.myApiUrl + 'calculate?orderId=' + orderId,);
  }
  listOrderItems(orderId: number) {
    this.http.get(this.myBackendUrl + 'orderitem/list?orderId=' + orderId)
      .toPromise().then(resp => {
        this.list = (resp as Response).data as OrderItems[]
      });
  }
}
