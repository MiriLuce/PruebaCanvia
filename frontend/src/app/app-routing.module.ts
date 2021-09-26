import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { CustomerComponent } from './components/customer/customer.component';
import { AuthorComponent } from './components/author/author.component';
import { BookComponent } from './components/book/book.component';
import { OrderFormComponent } from './components/order/order-form/order-form.component';
import { OrderListComponent } from './components/order/order-list/order-list.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home', 
    pathMatch: 'full' 
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'clientes',
    component: CustomerComponent
  },
  {
    path: 'autores',
    component: AuthorComponent
  },
  {
    path: 'libros',
    component: BookComponent
  },
  {
    path: 'ordenes-registro',
    component: OrderFormComponent
  },
  {
    path: 'ordenes-listado',
    component: OrderListComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
