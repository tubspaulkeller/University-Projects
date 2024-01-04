import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  private payload: any = null;

  public SetToken(token: any) {
    if (token) {
      localStorage.setItem('token', token);
    }
  }

  public GetToken() {
    return localStorage.getItem('token');
  }

  public DeleteToken() {
    localStorage.removeItem('token');
  }

  public GetPayload() {
    const token = this.GetToken();
    if (token) {
      const temp = token.split('.')[1];
      this.payload = JSON.parse(window.atob(temp));
    }
    return this.payload.data;
  }
}
