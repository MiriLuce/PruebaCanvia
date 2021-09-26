import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { CustomerService } from 'src/app/services/customer/customer.service';
import { Customers } from '../../models/customers';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss'],
  providers: [CustomerService]
})
export class CustomerComponent implements OnInit, OnDestroy {
  form: FormGroup;
  suscription: Subscription = new Subscription();
  displayedColumns: string[] = ['name', 'phone', 'email', 'address', 'actions'];

  currentCustomer: Customers | undefined;
  currentCustomerId = 0;

  constructor(private formBuilder: FormBuilder, 
              public customerService: CustomerService,
              private toastrService: ToastrService) {
    this.form = this.formBuilder.group({
      customerId: 0,
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      phone: ['', [Validators.required]],
      email: ['', [Validators.required]],
      addressStreet: ['', [Validators.required]]
    });
    customerService.listCustomer();
   }

  ngOnInit(): void {
    this.suscription = this.customerService.detail().subscribe(data => {
      this.form.patchValue({
        customerId: data.customerId,
        firstName: data.firstName,
        lastName: data.lastName,
        phone: data.phone,
        email: data.email,
        addressStreet: data.addressStreet
      });
      this.currentCustomerId = data.customerId;
    });
  }

  ngOnDestroy(){
    this.suscription.unsubscribe();
  }

  saveCustomer() {
    const customer: Customers = {
      customerId: 0,
      firstName: this.form.get('firstName')?.value,
      lastName: this.form.get('lastName')?.value,
      phone: this.form.get('phone')?.value,
      email: this.form.get('email')?.value,
      addressStreet: this.form.get('addressStreet')?.value,
    };
    if (this.currentCustomerId === 0) {
      this.customerService.createCustomer(customer).subscribe(data => {
        this.toastrService.success('Registro creado', 'El cliente fue creado.')
        this.form.reset();
        this.currentCustomerId = 0;
        this.customerService.listCustomer();
      });
    } else {
      customer.customerId = this.currentCustomerId;
      this.customerService.updateCustomer(customer).subscribe(data => {
        this.toastrService.success('Registro actualizado', 'El cliente fue actualizado.')
        this.form.reset();
        this.currentCustomerId = 0;
        this.customerService.listCustomer();
      });
    }
  }

  deleteCustomer(customerId: number) {
    if (confirm("¿Está seguro de eliminar el registro?")){
      this.customerService.deleteCustomer(customerId).subscribe( data => {
        this.toastrService.warning('Registro eliminado', 'El cliente fue eliminado.')
        this.customerService.listCustomer();
      });
    }
  }

  updateCustomer(customer: Customers){
    this.customerService.update(customer);
  }

}