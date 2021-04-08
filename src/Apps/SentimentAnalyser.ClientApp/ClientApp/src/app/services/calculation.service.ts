import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const baseUrl = 'https://localhost:5006/sentiment/calculation';

@Injectable({
  providedIn: 'root'
})
export class CalculationService {

  constructor(private http: HttpClient) { }

  calculateTextConnotation(data): Observable<any> {
    return this.http.post(`${baseUrl}`, data);
  }
}
