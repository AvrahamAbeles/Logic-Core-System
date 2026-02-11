export interface Operation {
  key: string;
  name: string;
  symbol: string;
}

export interface CalculationRequest {
  fieldA: number;
  fieldB: number;
  action: string;
}

export interface CalculationResult {
  value: number;
  historyLog?: HistoryLogEntry[];
  monthlyUsage?: number;
}
export interface HistoryLogEntry {
  operationName: string;
  symbol: string;
  inputA: number;
  inputB: number;
  result: number;
}