import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { OrderItems } from 'src/app/models/items';
import { Orders } from 'src/app/models/orders';
import { Books } from 'src/app/models/books';
import { BookService } from 'src/app/services/book/book.service';
import { OrderFormService } from 'src/app/services/order/order-form/order-form.service';

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.scss']
})
export class OrderFormComponent implements OnInit {
  formOrder: FormGroup;
  formOrderItems: FormGroup;
  displayedColumns: string[] = ['book', 'quantity', 'total'];
  currentOrderId = 0;

  constructor(private formBuilder: FormBuilder,
              public orderFormService: OrderFormService,
              private toastrService: ToastrService) {
    this.formOrder = this.formBuilder.group({
      customerId: 0,
      shippedDateStr: ['', [Validators.required]]
    });
    this.formOrderItems = this.formBuilder.group({
      bookId: 0,
      quantity: [0, [Validators.required]]
    });
  }

  ngOnInit(): void {
    
  }

  saveOrder() {
    const order: Orders = {
      orderId: 0,
      customerId: this.formOrder.get('customerId')?.value,
      statusOrder: this.formOrder.get('statusOrder')?.value,
      requiredDateStr: this.formOrder.get('requiredDateStr')?.value,
      shippedDateStr: this.formOrder.get('shippedDateStr')?.value,
      requiredDate: new Date(),
      shippedDate: new Date(),
      subtotalPrice: 0,
      taxes: 0,
      totalPrice: 0,
      customer: {
        customerId: this.formOrder.get('customerId')?.value,
        firstName: '',
        lastName: '',
        phone: '',
        email: '',
        addressStreet: ''
      }
    };
    this.orderFormService.createOrder(order).subscribe(resp => {
      this.toastrService.success('Registro creado', 'La ordern fue creado.')
      this.formOrderItems.reset();
      this.currentOrderId = (resp.data as Orders).orderId;
      this.orderFormService.listOrderItems(this.currentOrderId);
    });
  }

  saveOrderItems() {
    const orderItem: OrderItems ={
      orderItems: 0,
      orderId: this.currentOrderId,
      bookId: this.formOrderItems.get('bookId')?.value,
      book: {
        bookId: this.formOrderItems.get('bookId')?.value,
        title: '',
        genre: '',
        authorId: 0,
        author: {
          authorId: 0,
          firstName: '',
          lastName: '',
          country: '',
          birthdateStr: '',
          deathDateStr: '',
          birthdate: new Date(),
          deathDate: new Date()
        },
        stock: 0,
        price: 0,
        publicationDateStr: '',
        publicationDate: new Date()
      },
      quantity: this.formOrderItems.get('quantity')?.value,
      total: 0
    }
    this.orderFormService.createOrderItem(orderItem).subscribe(data => {
      this.toastrService.success('Registro creado', 'El libro fue agregado.')
      this.formOrderItems.reset();
      this.orderFormService.listOrderItems(this.currentOrderId);
      this.orderFormService.calculateOrder(this.currentOrderId);
    });
  }

}
