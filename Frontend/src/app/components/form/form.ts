import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Livros } from '../../models/livros';

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [RouterModule, FormsModule, ReactiveFormsModule],
  templateUrl: './form.html',
  styleUrl: './form.css'
})
export class Form implements OnInit {
  @Input() button! : string
  @Input() title! : string
  @Input() data : Livros | null = null
  @Output() onSubmit = new EventEmitter<Livros>();

  Form!: FormGroup;

  ngOnInit(): void {
    this.Form = new FormGroup({
      id : new FormControl(this.data ? this.data.id : ''),
      NomeLivro: new FormControl(this.data ? this.data.livro : ''),
      NomeAutor: new FormControl(this.data ? this.data.autor : ''),
      genero: new FormControl(this.data ? this.data.genero : '')
    });
  }

  submit(){ 
    this.onSubmit.emit(this.Form.value) 
  }
}
