import { ChangeDetectionStrategy, Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { NbpService } from '../../services/nbpService';
import { AsyncPipe, CommonModule, DatePipe } from '@angular/common';
import { NBPRates } from '../../models/NBPRates';
import { Subject } from 'rxjs';
import {MatDatepicker, MatDatepickerInputEvent, MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {provideNativeDateAdapter} from '@angular/material/core';
import { CalculatorComponent } from "../calculator/calculator.component";


@Component({
  selector: 'app-nbp',
  standalone: true,
  providers: [provideNativeDateAdapter(), DatePipe],
  imports: [
    CommonModule,
    AsyncPipe,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    CalculatorComponent
],
  templateUrl: './nbp.component.html',
  styleUrl: './nbp.component.css'
})

export class NbpComponent implements OnInit {
  public exchangeRates$: Subject<NBPRates> = new Subject<NBPRates>();
  public todayDate = new Date();

  constructor(private nbpService: NbpService, private datePipe: DatePipe) {}

  ngOnInit(): void {
    this.getTodayExchangeRates();
  }

  getTodayExchangeRates(): void {
    this.nbpService.getTodayExchangeRates().subscribe(data => 
      {
        if (data?.rates?.length > 0) {
          data.rates.unshift({ code: 'PLN', mid: 1, currency: 'złoty' });
        }
        this.exchangeRates$.next(data)
      });
    this.todayDate = new Date();
  }

  getExchangeRatesByDate(date: Date): void {
    const formattedDate = this.datePipe.transform(date, 'yyyy-MM-dd');
    if (formattedDate) {
      this.nbpService.getExchangeRatesByDate(formattedDate).subscribe(data => {
        if (data?.rates?.length > 0) {
          data.rates.unshift({ code: 'PLN', mid: 1, currency: 'złoty' });
        }
        this.exchangeRates$.next(data)
      });
    }
  }

  onDateChange(event: MatDatepickerInputEvent<Date>): void {
    const selectedDate = event.value;
    if (selectedDate !== null) {
      this.getExchangeRatesByDate(selectedDate);
    }
  }

  onTodayButtonClick(): void {
    console.log('onTodayButtonClick');
    this.getTodayExchangeRates();
  }

  public saveNbpRates(): void {
    this.nbpService.saveNbpRates(this.todayDate.toString()).subscribe(() => {});
  }
}