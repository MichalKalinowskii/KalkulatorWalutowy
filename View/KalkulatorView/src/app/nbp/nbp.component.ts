import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { NbpService } from '../services/nbpService';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-nbp',
  standalone: true,
  imports: [AsyncPipe],
  templateUrl: './nbp.component.html',
  styleUrl: './nbp.component.css'
})

export class NbpComponent implements OnInit {
  exchangeRates: any;

  constructor(private nbpService: NbpService) {}

  ngOnInit(): void {
    this.getExchangeRates();
  }

  getExchangeRates(): void {
    this.nbpService.getTodayExchangeRates().subscribe((data) => {
      this.exchangeRates = data;
    });
  }
}