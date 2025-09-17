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
}
