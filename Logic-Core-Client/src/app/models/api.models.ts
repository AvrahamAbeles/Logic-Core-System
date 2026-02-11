export interface Operation {
  key: string;
  displayName: string;
}

export interface CalculationRequest {
  fieldA: number;
  fieldB: number;
  action: string;
}

export interface CalculationResult {
  value: number;
  historyLog?: string[]; 
  monthlyUsage?: number;
}