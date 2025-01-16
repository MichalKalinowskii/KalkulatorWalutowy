import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpStatusCode } from '@angular/common/http';
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

    public saveNbpRates(nbpData: NBPRates, userName: string): Observable<HttpResponse<HttpStatusCode>> {
        const saveRates = { nbpData, userName };
        return this.http.post<any>(`${this.apiUrl}/save`, saveRates);
    }

    public getSavedRates(userName: string): Observable<NBPRates[]> 
    {
        return this.http.get<any>(`${this.apiUrl}/usersaves?userName=${userName}`);
    }
}