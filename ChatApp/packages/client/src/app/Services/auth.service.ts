import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}
  // TODO: replace type any
  public registerUser(body: any): Observable<any> {
    return this.http.post(`${environment.base_url}/register`, body);
  }

  public loginUser(body: any): Observable<any> {
    return this.http.post(`${environment.base_url}/login`, body);
  }
}
