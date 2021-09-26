import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { AuthorService } from 'src/app/services/author/author.service';
import { Authors } from '../../models/authors';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.scss'],
  providers: [AuthorService]
})
export class AuthorComponent implements OnInit, OnDestroy {
  form: FormGroup;
  suscription: Subscription = new Subscription();
  displayedColumns: string[] = ['name', 'country', 'birthdateStr', 'deathDateStr','actions'];

  currentAuthor: Authors | undefined;
  currentAuthorId = 0;

  constructor(private formBuilder: FormBuilder,
              public authorService: AuthorService,
              private toastrService: ToastrService) {
    this.form = this.formBuilder.group({
      authorId: 0,
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      country: ['', [Validators.required]],
      birthdateStr: ['', [Validators.required]],
      deathDateStr: ['', [Validators.required]]
    });
    authorService.listAuthor();
  }

  ngOnInit(): void {
    this.suscription = this.authorService.detail().subscribe(data => {
      this.form.patchValue({
        authorId: data.authorId,
        firstName: data.firstName,
        lastName: data.lastName,
        country: data.country,
        birthdateStr: data.birthdate,
        deathDateStr: data.deathDate
      });
      this.currentAuthorId = data.authorId;
    });
  }

  ngOnDestroy() {
    this.suscription.unsubscribe();
  }

  saveAuthor() {
    const customer: Authors = {
      authorId: 0,
      firstName: this.form.get('firstName')?.value,
      lastName: this.form.get('lastName')?.value,
      country: this.form.get('country')?.value,
      birthdateStr: this.form.get('birthdateStr')?.value,
      deathDateStr: this.form.get('deathDateStr')?.value,
      birthdate: new Date(),
      deathDate: new Date()
    };
    if (this.currentAuthorId === 0) {
      this.authorService.createAuthor(customer).subscribe(data => {
        this.toastrService.success('Registro creado', 'El autor fue creado.')
        this.form.reset();
        this.currentAuthorId = 0;
        this.authorService.listAuthor();
      });
    }
    else {
      customer.authorId = this.currentAuthorId;
      this.authorService.updateAuthor(customer).subscribe(data => {
        this.toastrService.success('Registro actualizado', 'El autor fue actualizado.')
        this.form.reset();
        this.currentAuthorId = 0;
        this.authorService.listAuthor();
      });
    }
  }

  deleteAuthor(authorId: number) {
    if (confirm("¿Está seguro de eliminar el registro?")) {
      this.authorService.deleteAuthor(authorId).subscribe(data => {
        this.toastrService.warning('Registro eliminado', 'El autor fue eliminado.')
        this.authorService.listAuthor();
      });
    }
  }

  updateAuthor(author: Authors) {
    this.authorService.update(author);
  }
}