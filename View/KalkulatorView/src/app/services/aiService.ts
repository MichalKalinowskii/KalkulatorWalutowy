import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AI_API_KEY, GENAI_API_KEY } from '../config/api-key';
import { map, Observable } from 'rxjs';

@Injectable({providedIn: 'root'})
export class AiService {
    
    private apiUrl = 'https://newsapi.org/v2/{replace}&apiKey=' + AI_API_KEY;
    private genaiUrl = 'https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=' + GENAI_API_KEY;

    constructor(private http: HttpClient) { }

    public getTopHeadLineNews(date: string): Observable<any> 
    {
        return this.http.get<any>(this.apiUrl.replace(
            '{replace}', 
            'everything?q=poland&categorry=busines&from='+date+'&to='+date+'&sortBy=popularity'
        ));
    }

    public gemini(prompt: string): Observable<any> {
        console.log(prompt);
        const body = {
        contents: [
            {
            parts: [
                {
                text: prompt,
                },
            ],
            },
        ],
        };

        const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        });

        return this.http
        .post<any>(this.genaiUrl, body, { headers })
        .pipe(map((resp) => resp));
    }
    

}