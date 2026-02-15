import { Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CalculatorService } from '../../services/calculator.service';
import { CalculationResult ,CalculationRequest, Operation } from '../../models/api.models';

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

  errorMessage = signal<string | null>(null);
  fieldA = signal<string >('');
  fieldB = signal<string >('');
  selectedOp = signal<string>('');

  ngOnInit() {
    this.isLoading.set(true);
    this.calcService.getOperations().subscribe({
      next: (ops) => {
        this.operations.set(ops);
        if (ops.length > 0)
          this.selectedOp.set(ops[0].key);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error(err);
        this.errorMessage.set('Failed to load operations');
        this.isLoading.set(false);
      }
    });
  }

  onCalculate() {
    const a = this.fieldA();
    const b = this.fieldB();
    const op = this.selectedOp();

    if (!a || !b || !op) {
        this.errorMessage.set('Please fill all fields');
        return;
    }

    this.isLoading.set(true);
    this.errorMessage.set(null);
    this.result.set(null);
    const calculationRequest: CalculationRequest = {
      fieldA: a,
      fieldB: b,
      action: op
    };
    this.calcService.calculate(calculationRequest).subscribe({
      next: (res) => {
        this.result.set(res);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error(err);
        let msg = 'אירעה שגיאה בביצוע החישוב';
        if (err.error && typeof err.error === 'object') {
          const serverError = err.error.error; 
          const serverMsg = err.error.message; 
          if (serverError && serverMsg) {
            msg = `${serverError}: ${serverMsg}`;
          } else if (serverMsg) {
            msg = serverMsg;
          }
        }
        this.errorMessage.set(msg);
        this.isLoading.set(false);
      }
    });
  }
}
