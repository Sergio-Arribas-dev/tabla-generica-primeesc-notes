import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ECSPrimengTableHttpService } from '@eternalcodestudio/primeng-table';

@Injectable({ providedIn: 'root' })
export class TableHttpAdapterService extends ECSPrimengTableHttpService {
  private readonly apiUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {
    super();
  }

  handleHttpGetRequest<T>(
    servicePoint: string,
    responseType: 'json' | 'blob' = 'json'
  ): Observable<HttpResponse<T>> {
    return this.http.get<T>(`${this.apiUrl}${servicePoint}`, {
      observe: 'response'
    });
  }

  handleHttpPostRequest<T>(
    servicePoint: string,
    data: any,
    httpOptions: any = null,
    responseType: 'json' | 'blob' = 'json'
  ): Observable<HttpResponse<T>> {
    return this.http.post<T>(`${this.apiUrl}${servicePoint}`, data, {
      headers: httpOptions,
      observe: 'response'
    });
  }
}
