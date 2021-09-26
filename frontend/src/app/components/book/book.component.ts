import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { BookService } from 'src/app/services/book/book.service';
import { Books } from '../../models/books';
import { Authors } from '../../models/authors';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss'],
  providers: [BookService]
})
export class BookComponent implements OnInit, OnDestroy {
  form: FormGroup;
  suscription: Subscription = new Subscription();
  displayedColumns: string[] = ['author', 'title', 'genre', 'stock', 'price', 'publicationDateStr', 'actions'];

  currentBook: Books | undefined;
  currentAuthor: Authors | undefined
  currentAuthorId = 0;
  currentBookId = 0;

  constructor(private formBuilder: FormBuilder,
    public bookService: BookService,
    private toastrService: ToastrService) {
    bookService.listBook();
    bookService.listAuthor();
    this.form = this.formBuilder.group({
      bookId: 0,
      //author: ['', [Validators.required]],
      authorId: [0, [Validators.required]],
      title: ['', [Validators.required]],
      genre: ['', [Validators.required]],
      stock: [0, [Validators.required]],
      price: [0, [Validators.required]],
      publicationDateStr: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.suscription = this.bookService.detail().subscribe(data => {
      console.log(this.form)
      this.form.patchValue({
        bookId: data.bookId,
        authorId: data.authorId,
        author: data.author,
        title: data.title,
        genre: data.genre,
        stock: data.stock,
        price: data.price,
        publicationDateStr: data.publicationDate
      });
      this.currentBookId = data.bookId;
      this.currentAuthor = data.author;
      this.currentAuthorId = data.authorId;
    });
  }

  ngOnDestroy() {
    this.suscription.unsubscribe();
  }

  saveBook() {
    const book: Books = {
      bookId: 0,
      authorId: this.form.get('authorId')?.value,
      author: {
        authorId: this.form.get('authorId')?.value,
        firstName: '',
        lastName: '',
        country: '',
        birthdateStr: '',
        deathDateStr: '',
        birthdate: new Date(),
        deathDate: new Date()
      },
      title: this.form.get('title')?.value,
      genre: this.form.get('genre')?.value,
      stock: this.form.get('stock')?.value,
      price: this.form.get('price')?.value,
      publicationDateStr: this.form.get('publicationDateStr')?.value,
      publicationDate: new Date(),
    };
    if (this.currentBookId === 0) {
      this.bookService.createBook(book).subscribe(data => {
        this.toastrService.success('Registro creado', 'El libro fue creado.')
        this.form.reset();
        this.currentBookId = 0;
        this.currentAuthor = undefined;
        this.currentAuthorId = 0;
        this.bookService.listBook();
      });
    }
    else {
      book.bookId = this.currentBookId;
      this.bookService.updateBook(book).subscribe(data => {
        this.toastrService.success('Registro actualizado', 'El libro fue actualizado.')
        this.form.reset();
        this.currentBookId = 0;
        this.currentAuthor = undefined;
        this.currentAuthorId = 0;
        this.bookService.listBook();
      });
    }
  }

  deleteBook(bookId: number) {
    if (confirm("¿Está seguro de eliminar el registro?")) {
      this.bookService.deleteBook(bookId).subscribe(data => {
        this.toastrService.warning('Registro eliminado', 'El libro fue eliminado.')
        this.bookService.listBook();
      });
    }
  }

  updateBook(book: Books) {
    this.bookService.update(book);
  }
}
