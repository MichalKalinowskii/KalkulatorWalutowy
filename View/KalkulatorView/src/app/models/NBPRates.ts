import { Rates } from "./rates";

export interface NBPRates {
    table: string;
    no: string;
    tradingDate: Date;
    effectiveDate: Date;
    rates: Rates[];
}