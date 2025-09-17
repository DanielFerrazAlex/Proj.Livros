import { Component, OnInit } from '@angular/core';
import { Api } from '../../services/api';
import { Livros } from '../../models/livros';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  imports: [CommonModule, FormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit {

  livros : Livros[] = [];
  termoBusca: string = '';

  constructor(private api: Api ){

  }
  ngOnInit(): void {
    this.api.SelecionarLivros().subscribe(response => {
      this.livros = response.data;
    })
  }

  buscar(termo: string){

    this.api.SelecionarLivrosPorTermo(termo).subscribe({
        next: (response) => {
          console.log('Resultado da busca:', response.data);
          this.livros = response.data;
      },
      error: (err) => console.error('Erro ao buscar livros:', err)
    });
  }

  emprestar(active: boolean) {
    console.log('Emprestar livro:', active);
  }

  editar(id: string) {
    console.log('Editar livro:', id);
  }

  excluir(id: string) {
    console.log('Excluir livro:', id);
  }
}
