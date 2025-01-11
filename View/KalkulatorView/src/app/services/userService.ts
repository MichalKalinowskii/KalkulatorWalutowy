import { HttpClient, HttpResponse, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserCredentials } from '../models/userCredentials';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    private apiUrl = 'https://localhost:7037/user';

    constructor(private http: HttpClient) { }

    public login(userCredentials: UserCredentials): Observable<HttpResponse<HttpStatusCode>> {
        return this.http.post<any>(`${this.apiUrl}/login`, userCredentials);
    }

    public register(userCredentials: UserCredentials): Observable<HttpResponse<HttpStatusCode>>  {
        return this.http.post<any>(`${this.apiUrl}/register`, userCredentials);
    }
}