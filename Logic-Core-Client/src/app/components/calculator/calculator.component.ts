import { Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CalculatorService } from '../../services/calculator.service';
import { CalculationResult, Operation } from '../../models/api.models';

@Component({
  selector: 'app-calculator',
  imports: [FormsModule],
  templateUrl: './calculator.component.html',
  styleUrl: './calculator.component.scss'
})
export class CalculatorComponent implements OnInit {


private calcService = inject(CalculatorService);

operations = signal<Operation[]>([]);
isLoading = signal<boolean>(false);
result = signal<CalculationResult | null>(null);

fieldA = signal<number | null>(null);
fieldB = signal<number | null>(null);
selectedOp = signal<string>('');

ngOnInit() {
    this.isLoading.set(true);
    this.calcService.getOperations().subscribe(ops => {
      this.operations.set(ops);
      if (ops.length > 0) 
        this.selectedOp.set(ops[0].key);
      this.isLoading.set(false);
    });
  }
  
onCalculate() {
    const a = this.fieldA();
    const b = this.fieldB();
    const op = this.selectedOp();

    if (a === null || b === null || !op) return;

    this.isLoading.set(true);
    
    this.calcService.calculate(a, b, op).subscribe({
      next: (res) => {
        this.result.set(res);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error(err);
        this.isLoading.set(false);
      }
    });
  }
}
