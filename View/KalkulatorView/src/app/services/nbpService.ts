import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse  } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NBPRates } from '../models/NBPRates';

@Injectable({
    providedIn: 'root'
})
export class NbpService {

    private apiUrl = 'https://localhost:7037/nbp';

    constructor(private http: HttpClient) { }

    public getTodayExchangeRates(): Observable<NBPRates> {
        return this.http.get<any>(`${this.apiUrl}/today`);
    }

    public getExchangeRatesByDate(date: String): Observable<NBPRates> {
        return this.http.get<any>(`${this.apiUrl}/date?date=${date}`);
    }
}