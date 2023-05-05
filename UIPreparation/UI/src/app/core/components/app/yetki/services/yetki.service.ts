import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Yetki } from '../models/Yetki';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class YetkiService {

  constructor(private httpClient: HttpClient) { }


  getYetkiList(): Observable<Yetki[]> {

    return this.httpClient.get<Yetki[]>(environment.getApiUrl + '/yetkis/getall')
  }

  getYetkiById(id: number): Observable<Yetki> {
    return this.httpClient.get<Yetki>(environment.getApiUrl + '/yetkis/getbyid?id='+id)
  }

  addYetki(yetki: Yetki): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/yetkis/', yetki, { responseType: 'text' });
  }

  updateYetki(yetki: Yetki): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/yetkis/', yetki, { responseType: 'text' });

  }

  deleteYetki(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/yetkis/', { body: { id: id } });
  }


}