export interface Operation {
  key: string;
  name: string;
  formula: string;
  symbol: string;
  isActive: boolean;
}

export interface CalculationRequest {
  fieldA: string;
  fieldB: string;
  action: string;
}

export interface CalculationResult {
  value: string;
  historyLog?: HistoryLogEntry[];
  monthlyUsage?: number;
}
export interface HistoryLogEntry {
  operationName: string;
  symbol: string;
  inputA: string;
  inputB: string;
  result: string;
}