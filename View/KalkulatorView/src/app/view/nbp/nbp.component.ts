import { ChangeDetectionStrategy, Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { NbpService } from '../../services/nbpService';
import { AsyncPipe, CommonModule, DatePipe } from '@angular/common';
import { NBPRates } from '../../models/NBPRates';
import { BehaviorSubject, never, Subject } from 'rxjs';
import {MatDatepicker, MatDatepickerInputEvent, MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {provideNativeDateAdapter} from '@angular/material/core';
import { CalculatorComponent } from "../calculator/calculator.component";
import { UserAuthComponent } from "../user-auth/user-auth.component";
import { FormsModule } from '@angular/forms';
import {MatIconModule} from '@angular/material/icon'


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
    CalculatorComponent,
    UserAuthComponent,
    FormsModule,
    MatIconModule
],
  templateUrl: './nbp.component.html',
  styleUrl: './nbp.component.css'
})

export class NbpComponent implements OnInit {
  public exchangeRates$: BehaviorSubject<NBPRates> = new BehaviorSubject<NBPRates>(null as never);
  public difrentDataLoaded = false;
  public datePickerValue: Date | null = new Date();
  public userLoggedIn: boolean = false;
  public showUserAuthModal: boolean = false;
  public closeModal: boolean = true;
  public loggedUserName: string = '';
  public savedRates: NBPRates[] = [];
  public selectedSavedRate: NBPRates = null as never;

  constructor(private nbpService: NbpService, private datePipe: DatePipe) {}

  ngOnInit(): void {
    this.getTodayExchangeRates();
  }

  setUserLoggedIn(value: string): void {
    this.userLoggedIn = true;
    this.loggedUserName = value;
    this.getSavedRates();
  }

  openUserAuthModal(): void {
    this.showUserAuthModal = true;
    this.closeModal = false;
  }

  closeModalClick() {
    this.closeModal = true;
  }

  getTodayExchangeRates(): void {
    this.difrentDataLoaded = false;
    this.nbpService.getTodayExchangeRates().subscribe({
      next: data => {
        if (data?.rates?.length > 0) {
          data.rates.unshift({ code: 'PLN', mid: 1, currency: 'złoty' });
        }
        this.exchangeRates$.next(data);
        
        if (data.effectiveDate.toString() !== this.datePipe.transform(new Date(), 'yyyy-MM-dd')) {
          this.difrentDataLoaded = true;
          this.datePickerValue = null;
        }

        this.datePickerValue = data.effectiveDate;
      },
      error: err => {
        console.log(err.error);
        this.exchangeRates$.next(null as never);
      }
    });
  }

  getExchangeRatesByDate(date: Date): void {
    this.difrentDataLoaded = false;
    const formattedDate = this.datePipe.transform(date, 'yyyy-MM-dd');
    if (formattedDate) {
      this.nbpService.getExchangeRatesByDate(formattedDate).subscribe({
        next: data => {
          if (data?.rates?.length > 0) {
            data.rates.unshift({ code: 'PLN', mid: 1, currency: 'złoty' });
          }
          this.exchangeRates$.next(data);
          
          if (data.effectiveDate.toString() !== formattedDate) {
            this.difrentDataLoaded = true;
            this.datePickerValue = null;
          }

          this.datePickerValue = data.effectiveDate;
        },
        error: err => {
          console.log(err.error);
          this.exchangeRates$.next(null as never);
        }
      });
    }
  }

  getSavedRates(): void {
    this.nbpService.getSavedRates(this.loggedUserName).subscribe({
      next: data => {
        this.savedRates = data;
      },
      error: err => {
        console.log('Get saved rates error', err);
      }
    });
  }

  onDateChange(event: MatDatepickerInputEvent<Date>): void {
    const selectedDate = event.value;
    if (selectedDate !== null) {
      this.getExchangeRatesByDate(selectedDate);
    }
  }

  saveNbpRates(): void {
    this.nbpService.saveNbpRates(this.exchangeRates$.value, this.loggedUserName).subscribe({
      next: data => {
        console.log('Saved');
        this.savedRates.push(this.exchangeRates$.value);
        this.savedRates = this.savedRates.sort((a, b) => new Date(a.effectiveDate).getTime() - new Date(b.effectiveDate).getTime());
      },
      error: err => {
        console.log('Save error', err);
      },
    });
  }

  onTodayButtonClick(): void {
    console.log('onTodayButtonClick');
    this.getTodayExchangeRates();
  }

  updateRates(rates: NBPRates): void {
    this.exchangeRates$.next(rates);
    this.datePickerValue = rates.effectiveDate;
  }

  onSavedRateChange() {
    this.updateRates(this.selectedSavedRate);
  }

  onAiButtonClick(): void 
  {
    
  }
}