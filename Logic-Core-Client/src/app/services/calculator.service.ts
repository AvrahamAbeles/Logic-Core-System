import { Injectable ,inject} from '@angular/core';
import { HttpClient } from '@angular/common/http'; // לשימוש עתידי
import { Observable, delay, of } from 'rxjs';
import { CalculationResult, Operation } from '../models/api.models';
@Injectable({
  providedIn: 'root'
})
export class CalculatorService {


  private apiUrl = 'https://localhost:7164/api/Calculator';
  constructor() { }

  private http = inject(HttpClient);


  getOperations(): Observable<Operation[]> {
   return this.http.get<Operation[]>(`${this.apiUrl}/operations`);
  }

  // מדמה חישוב (POST /api/calculate)
  calculate(a: number, b: number, action: string): Observable<CalculationResult> {
    
    const body = { fieldA: a, fieldB: b, action };
    return this.http.post<CalculationResult>(`${this.apiUrl}/calculate`, body);
  }

}
