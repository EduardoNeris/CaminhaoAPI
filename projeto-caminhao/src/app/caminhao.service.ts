import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Caminhao } from './caminhao';

@Injectable({
  providedIn: 'root'
})
export class CaminhaoService {
  private apiUrl = 'https://localhost:44379/api/caminhao';

  constructor(private http: HttpClient) { }

  getCaminhoes(): Observable<Caminhao[]> {
    return this.http.get<Caminhao[]>(this.apiUrl);
  }

  getCaminhao(id: number): Observable<Caminhao> {
    return this.http.get<Caminhao>(`${this.apiUrl}/${id}`);
  }

  createCaminhao(caminhao: Caminhao): Observable<Caminhao> {
    return this.http.post<Caminhao>(this.apiUrl, caminhao);
  }

  updateCaminhao(caminhao: Caminhao): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${caminhao.id}`, caminhao);
  }

  deleteCaminhao(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
