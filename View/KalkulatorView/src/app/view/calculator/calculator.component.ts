import { AfterViewInit, Component, Input, OnChanges } from '@angular/core';
import { Rates } from '../../models/rates';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-calculator',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './calculator.component.html',
  styleUrl: './calculator.component.css'
})
export class CalculatorComponent implements AfterViewInit, OnChanges {
  @Input() public rates!: Rates[];

  public leftInput!: number;
  public rightInput!: number;

  public leftRate!: Rates;
  public rightRate!: Rates;
  
  ngAfterViewInit(): void {
    this.rates.unshift( { code: 'PLN', mid: 1, currency: "złoty" } as Rates );
    this.leftRate = this.rates[0];
    this.rightRate = this.rates.find(rate => rate.code === 'EUR') as Rates;
  }

  ngOnChanges(): void {
    this.rates.unshift( { code: 'PLN', mid: 1, currency: "złoty" } as Rates );
    this.leftRate = this.rates[0];
    this.rightRate = this.rates.find(rate => rate.code === 'EUR') as Rates;
    this.onLeftRateChange(this.leftRate);
  }

  public onLeftRateChange(rate: Rates): void {
    if (this.leftInput != null) 
    {
      if (this.rightInput == null) {
        this.rightInput = 1;
      }

      this.rightInput = parseFloat((rate.mid / this.rightRate.mid * this.leftInput).toFixed(4));
    }
    else {
      this.rightInput = 0;
    }
  }

  public onRightRateChange(rate: Rates): void {
    if (this.rightInput != null) 
    {
      if (this.leftInput == null) {
        this.leftInput = 1;
      }

      this.leftInput = parseFloat((rate.mid / this.leftRate.mid * this.rightInput).toFixed(4));
    }
    else {
      this.leftInput = 0;
    }
  }
}
