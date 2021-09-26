import { Customers } from "./customers";

export interface  Orders {
    orderId: number;
    customerId: number;
    customer: Customers,
    statusOrder: string;
    shippedDateStr: string;
    requiredDateStr: string;
    shippedDate: Date;
    requiredDate: Date;
    subtotalPrice: number;
    taxes: number;
    totalPrice: number;
}