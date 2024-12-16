import { Component, Input, input } from '@angular/core';
import { NBPRates } from '../../models/NBPRates';
import { Rates } from '../../models/rates';

@Component({
  selector: 'app-calculator',
  standalone: true,
  imports: [],
  templateUrl: './calculator.component.html',
  styleUrl: './calculator.component.css'
})
export class CalculatorComponent {
  @Input() public rates!: Rates[];
}
