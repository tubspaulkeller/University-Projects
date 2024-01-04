import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root',
})
export class ImageService {
  constructor(private http: HttpClient) {}

  public AddImage(id: any, image: any): Observable<any> {
    return this.http.post(`${environment.base_url}/upload-image`, { user: id, img: image });
  }

  public SetProfileImg(id: any, imageId: any, imageVersion: any): Observable<any> {
    return this.http.post(`${environment.base_url}/set-profile-image/${imageId}/${imageVersion}`, {
      user: id,
    });
  }
}
