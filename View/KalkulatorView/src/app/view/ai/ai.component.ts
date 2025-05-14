import { Component, OnInit } from '@angular/core';
import { AiService } from '../../services/aiService';
import { CommonModule, DatePipe } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon'
import { Router } from '@angular/router';
import { provideNativeDateAdapter } from '@angular/material/core';
import { NbpService } from '../../services/nbpService';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { map } from 'rxjs';


@Component({
  selector: 'app-ai',
  providers: [provideNativeDateAdapter(), DatePipe],
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    FormsModule,
    MatIconModule
  ],
  templateUrl: './ai.component.html',
  styleUrl: './ai.component.css'
})
export class AiComponent implements OnInit {

  public datePickerValue: Date | null = new Date();
  public filteredDescriptions: string[] = [];
  public aiResponse: string = '';

  constructor(private aiService: AiService, private router: Router, private nbpService: NbpService, private datePipe: DatePipe) {}

  ngOnInit(): void {
    //this.callToApi();
    //this.callToGenAi();
  }

  predictTomorowCourse() {
    const formattedDate = this.datePipe.transform(this.datePickerValue, 'yyyy-MM-dd');
    this.callToApi(formattedDate!);
  }

  callToApi(date: string): void {
    this.aiService.getTopHeadLineNews(date)
      .pipe(
        map(resp => (resp.articles || [])),
        map(articles =>
          articles
            .filter((a: { content?: string; description?: string }) =>
              /polska|poland/i.test(
                a.content ?? a.description ?? ''
              )
            )
            .map((a: { description?: string }) => a.description ?? '')
        )
      )
      .subscribe(descriptions => {
        this.callToGenAi(descriptions);
      });
  }

  callToGenAi(descriptions: string[]) {
    // budujemy ponumerowaną listę opisów
    const numberedList = descriptions
      .map((desc, idx) => `${idx + 1}. ${desc}`)
      .join('\n');

    const prompt = [
      'Oszacuj na podstawie tych opisów czy wartość złotówki jutro wzrośnie czy zmaleje.',
      'WAŻNE: Określ o ILE liczbowo i wyjaśnij dlaczego podjęłaś taką decyzję.',
      'RÓWNIEŻ Nie umieszczaj zbęndnych objaśnień, MA być najpierw wartość liczbowa (z określeniem czy to jest wzrost czy spadek), a potem tylko kluczowe objaśnienia!!!',
      'Opisy:',
      numberedList
    ].join('\n');

    this.aiService.gemini(prompt)
      .pipe(
        map(resp => 
          resp?.candidates?.[0]?.content?.parts?.[0]?.text 
          ?? ''
        )
      )
      .subscribe(text => {
        this.aiResponse = text;
        console.log(this.aiResponse);
      });
  }

  onDateChange(event: MatDatepickerInputEvent<Date>): void {
    const selectedDate = event.value;
    if (selectedDate !== null) {
      this.getExchangeRatesByDate(selectedDate);
    }
  }

  public todayEuro: number = 1;
  public todayDollar: number = 1;
  public tomorowEuro: number = 1;
  public tomorowDollar: number = 1;

  public get differenceEuro(): number {
    return (this.todayEuro / this.tomorowEuro - 1) * 100;
  }
  public get differenceDollar(): number {
    return (this.todayDollar / this.tomorowDollar - 1) * 100;
  }

  getExchangeRatesByDate(date: Date): void {
    const formattedDate = this.datePipe.transform(date, 'yyyy-MM-dd');
    this.nbpService.getExchangeRatesByDate(formattedDate!).subscribe({
      next: data => {
        this.todayEuro = data.rates.find(x => x.code === 'EUR')!.mid;
        this.todayDollar = data.rates.find(x => x.code === 'USD')!.mid;      
      },
      error: err => {
        console.log(err.error);
      }
    });

    const tomorow = new Date(date);
    tomorow.setDate(tomorow.getDate() + 1);
    const tomorowDate = this.datePipe.transform(tomorow, 'yyyy-MM-dd');
    
    this.nbpService.getExchangeRatesByDate(tomorowDate!).subscribe({
        next: data => {
          this.tomorowEuro = data.rates.find(x => x.code === 'EUR')!.mid;
          this.tomorowDollar = data.rates.find(x => x.code === 'USD')!.mid;     
        },
        error: err => {
          console.log(err.error);
        }
      });

  }

  backToMainPage() {
    this.router.navigate(['']);
  }
}
