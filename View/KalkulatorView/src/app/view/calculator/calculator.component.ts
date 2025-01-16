import { AfterViewInit, Component, Input, OnChanges } from '@angular/core';
import { Rates } from '../../models/rates';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-calculator',
    imports: [FormsModule],
    templateUrl: './calculator.component.html',
    styleUrl: './calculator.component.css'
})
export class CalculatorComponent implements OnChanges {
  @Input() public rates!: Rates[];

  public leftInput: number | null = null;
  public rightInput: number | null = null;

  public leftRate!: Rates;
  public rightRate!: Rates;

  ngOnChanges(): void {
    if (this.leftRate != null) {
      this.leftRate = this.rates.find(rate => rate.code === this.leftRate.code) as Rates;
    }
    else {
      this.leftRate = this.rates[0];
    }

    if (this.rightRate != null) {
      this.rightRate = this.rates.find(rate => rate.code === this.rightRate.code) as Rates;
    } 
    else {
      this.rightRate = this.rates.find(rate => rate.code === 'EUR') as Rates;
    }
    
    this.onLeftRateChange(this.leftRate);
  }

  public onLeftRateChange(rate: Rates): void {
    if (this.leftInput != null) 
    {
      if (this.rightInput == null) {
        this.rightInput = 1;
      }

      this.rightInput = parseFloat((rate.mid / this.rightRate.mid * this.leftInput)
        .toFixed(4));
    }
    else {
      this.rightInput = null;
    }
  }

  public onRightRateChange(rate: Rates): void {
    if (this.rightInput != null) 
    {
      if (this.leftInput == null) {
        this.leftInput = 1;
      }

      this.leftInput = parseFloat((rate.mid / this.leftRate.mid * this.rightInput)
        .toFixed(4));
    }
    else {
      this.leftInput = null;
    }
  }
}
