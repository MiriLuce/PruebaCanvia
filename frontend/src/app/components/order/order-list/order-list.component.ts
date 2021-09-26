import { Component, OnInit } from '@angular/core';
import { OrderListService } from '../../../services/order/order-list/order-list.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss']
})
export class OrderListComponent implements OnInit {

  displayedColumns: string[] = ['customer', 'status', 'shippedDate', 'total'];

  constructor(public orderListService: OrderListService) {
    orderListService.listOrders();
   }

  ngOnInit(): void {
  }

}
