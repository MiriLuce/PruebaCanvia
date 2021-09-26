import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { Books } from 'src/app/models/books';
import { Authors } from 'src/app/models/authors';
import { Response } from '../../models/response';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private myBackendUrl = 'https://localhost:44390/';
  private myApiUrl = 'book/';

  public list: Books[] = [];
  public authorList: Authors[] = [];
  private formUpdated = new BehaviorSubject<Books>({} as any);

  constructor(private http: HttpClient) { }

  createBook(book: Books): Observable<Books> {
    return this.http.post<Books>(this.myBackendUrl + this.myApiUrl + 'create', book);
  }
  deleteBook(bookId: number): Observable<Books> {
    return this.http.delete<Books>(this.myBackendUrl + this.myApiUrl + 'logicdelete?id=' + bookId);
  }
  listBook() {
    this.http.get(this.myBackendUrl + this.myApiUrl + 'list')
      .toPromise().then(resp => {
        this.list = (resp as Response).data as Books[]
      });
  }
  listAuthor() {
    this.http.get(this.myBackendUrl + 'author/list')
      .toPromise().then(resp => {
        this.authorList = (resp as Response).data as Authors[]
      });
  }

  update(book: Books) {
    this.formUpdated.next(book);
  }
  detail(): Observable<Books> {
    return this.formUpdated.asObservable();
  }
  updateBook(book: Books): Observable<Books> {
    return this.http.put<Books>(this.myBackendUrl + this.myApiUrl + 'modify', book);
  }
}
