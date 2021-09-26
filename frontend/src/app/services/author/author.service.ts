import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { Authors } from 'src/app/models/authors';
import { Response } from '../../models/response';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  private myBackendUrl = 'https://localhost:44390/';
  private myApiUrl = 'author/';

  public list: Authors[] = [];
  private formUpdated = new BehaviorSubject<Authors>({} as any);

  constructor(private http: HttpClient) { }

  createAuthor(author: Authors): Observable<Authors> {
    return this.http.post<Authors>(this.myBackendUrl + this.myApiUrl + 'create', author);
  }
  deleteAuthor(authorId: number): Observable<Authors> {
    return this.http.delete<Authors>(this.myBackendUrl + this.myApiUrl + 'logicdelete?id=' + authorId);
  }
  listAuthor() {
    this.http.get(this.myBackendUrl + this.myApiUrl + 'list')
      .toPromise().then(resp => {
        this.list = (resp as Response).data as Authors[]
      });
  }

  update(author: Authors) {
    this.formUpdated.next(author);
  }
  detail(): Observable<Authors> {
    return this.formUpdated.asObservable();
  }
  updateAuthor(author: Authors): Observable<Authors> {
    return this.http.put<Authors>(this.myBackendUrl + this.myApiUrl + 'modify', author);
  }
}
