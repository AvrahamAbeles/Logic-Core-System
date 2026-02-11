import { Injectable ,inject} from '@angular/core';
import { HttpClient } from '@angular/common/http'; // לשימוש עתידי
import { Observable, delay, of } from 'rxjs';
import { CalculationResult, Operation } from '../models/api.models';
@Injectable({
  providedIn: 'root'
})
export class CalculatorService {

  constructor() { }

  private http = inject(HttpClient);


  // מדמה שליפת פעולות (GET /api/operations)
  getOperations(): Observable<Operation[]> {
    const mocks: Operation[] = [
      { key: 'add', displayName: 'חיבור (+)' },
      { key: 'sub', displayName: 'חיסור (-)' },
      { key: 'mul', displayName: 'כפל (*)' },
      { key: 'div', displayName: 'חילוק (/)' }
    ];
    return of(mocks).pipe(delay(500));
  }

  // מדמה חישוב (POST /api/calculate)
  calculate(a: number, b: number, action: string): Observable<CalculationResult> {
    
    // לוגיקה זמנית (Mock) עד שיהיה שרת C#
    let val = 0;
    if (action === 'add') val = a + b;
    if (action === 'sub') val = a - b;
    if (action === 'mul') val = a * b;
    if (action === 'div') val = a / b;

    return of({
      value: val,
      historyLog: [`${a} ${action} ${b} = ${val}`, 'חישוב קודם: 10'], 
      monthlyUsage: 42
    }).pipe(delay(800));
  }

}
