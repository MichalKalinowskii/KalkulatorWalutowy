import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpStatusCode  } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
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

    public saveNbpRates(date: String): Observable<HttpResponse<HttpStatusCode>> {
        return this.http.post<any>(`${this.apiUrl}/save`, date, { observe: 'response' });
    }
}