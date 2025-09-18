import { Component } from '@angular/core';
import { Form } from "../../components/form/form";
import { Livros } from '../../models/livros';
import { Api } from '../../services/api';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [Form],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {

  title = 'Cadastrar Livro'
  button = 'Cadastrar'

  constructor(private api : Api, private router : Router) {}

  CadastrarLivro(livro : Livros) {
    this.api.CadastrarLivro(livro).subscribe(response => {
      this.router.navigate(['/'])
      alert('Livro Cadastrado')
    })
  }

}
