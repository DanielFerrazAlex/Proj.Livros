import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Livros } from '../models/livros';
import { Response } from '../models/response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Api {
  
  ApiURL = environment.UrlAPI

  constructor(private https: HttpClient) {}

  SelecionarLivros() :Observable<Response<Livros[]>> {
    return this.https.get<Response<Livros[]>>(`${this.ApiURL}`);
  }

  SelecionarLivrosPorTermo(termo: string): Observable<Response<Livros[]>> {
    return this.https.get<Response<Livros[]>>(`${this.ApiURL}/${termo}`);
  }

    SelecionarLivrosPorId(id: string): Observable<Response<Livros>> {
    return this.https.get<Response<Livros>>(`${this.ApiURL}/${id}`);
  }

  CadastrarLivro(livro: Livros): Observable<Response<Livros>> {
    return this.https.post<Response<Livros>>(`${this.ApiURL}`, livro);
  }

EditarLivro(id : string ,livro: Livros): Observable<Response<Livros>> {
  return this.https.put<Response<Livros>>(`${this.ApiURL}/${id}`, livro, { headers: { 'Content-Type': 'application/json' } });
}

  DeletarLivro(id: string): Observable<Response<Livros[]>> {
    return this.https.delete<Response<Livros[]>>(`${this.ApiURL}/${id}`);
  }
}
