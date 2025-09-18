import { Component, OnInit } from '@angular/core';
import { Form } from "../../components/form/form";
import { Api } from '../../services/api';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Livros } from '../../models/livros';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit',
  standalone: true,
  imports: [Form, RouterModule, CommonModule],
  templateUrl: './edit.html',
  styleUrl: './edit.css'
})
export class Edit implements OnInit {

  title = 'Editar Livro';
  button = 'Editar';
  livro!: Livros;

  constructor(private api: Api, private route: ActivatedRoute, private router: Router) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) return;

    this.api.SelecionarLivrosPorId(id).subscribe(response => {
      this.livro = response.data;
    });
  }

  editarLivro(livro: Livros) {
  if (!livro.id) return;

  this.api.EditarLivro(livro.id, livro).subscribe({
    next: () => this.router.navigate(['/']),
  });
}
}
