import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  // Get Methods
  public getAllUsers(): Observable<any> {
    return this.http.get(`${environment.base_url}/users`);
  }

  public getUserById(userId: any): Observable<any> {
    return this.http.get(`${environment.base_url}/user/${userId}`);
  }
}
