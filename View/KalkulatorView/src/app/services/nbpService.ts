import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse  } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class NbpService {

    private apiUrl = 'https://localhost:7037/nbp';

    constructor(private http: HttpClient) { }

    public getTodayExchangeRates(): Observable<any> {
        return this.http.get<any>(`${this.apiUrl}/today`);
    }

    public getExchangeRatesByDate(date: Date): Observable<any> {
        return this.http.get<any>(`${this.apiUrl}/date/${date}`);
    }
}