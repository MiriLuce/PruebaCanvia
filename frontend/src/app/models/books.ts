import { Authors } from './authors';

export interface Books {
    bookId: number;
    authorId: number;
    author: Authors;
    title: string;
    genre: string;
    stock: number;
    price: number;
    publicationDateStr: string;
    publicationDate: Date;
}