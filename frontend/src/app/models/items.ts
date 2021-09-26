import { Books } from "./books";

export interface OrderItems {
    orderId: Number;
    orderItems: number;
    bookId: number;
    book: Books,
    quantity: number;
    total: number;
}