import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { Customers } from 'src/app/models/customers';
import { Response } from '../../models/response';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private myBackendUrl = 'https://localhost:44390/';
  private myApiUrl = 'customer/';

  public list: Customers[] = [];
  private formUpdated = new BehaviorSubject<Customers>({} as any);

  constructor(private http: HttpClient) { }

  createCustomer(customer: Customers): Observable<Customers>{
    return this.http.post<Customers>(this.myBackendUrl + this.myApiUrl + 'create', customer);
  }
  deleteCustomer(customerId: number): Observable<Customers>  {
    return this.http.delete<Customers>(this.myBackendUrl + this.myApiUrl + 'logicdelete?id=' + customerId);
  }
  listCustomer() {
    this.http.get(this.myBackendUrl + this.myApiUrl + 'list')
      .toPromise().then(resp => {
        this.list = (resp as Response).data as Customers[]
      });
  }

  update(customer: Customers) {
    this.formUpdated.next(customer);
  }
  detail(): Observable<Customers> {
    return this.formUpdated.asObservable();
  }
  updateCustomer(customer: Customers): Observable<Customers> {
    return this.http.put<Customers>(this.myBackendUrl + this.myApiUrl + 'modify', customer);
  }
}
