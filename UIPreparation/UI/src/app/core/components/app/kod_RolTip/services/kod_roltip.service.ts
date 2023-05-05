import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Kod_RolTip } from '../models/Kod_RolTip';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class Kod_RolTipService {

  constructor(private httpClient: HttpClient) { }


  getKod_RolTipList(): Observable<Kod_RolTip[]> {

    return this.httpClient.get<Kod_RolTip[]>(environment.getApiUrl + '/kod_RolTips/getall')
  }

  getKod_RolTipById(id: number): Observable<Kod_RolTip> {
    return this.httpClient.get<Kod_RolTip>(environment.getApiUrl + '/kod_RolTips/getbyid?id='+id)
  }

  addKod_RolTip(kod_RolTip: Kod_RolTip): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/kod_RolTips/', kod_RolTip, { responseType: 'text' });
  }

  updateKod_RolTip(kod_RolTip: Kod_RolTip): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/kod_RolTips/', kod_RolTip, { responseType: 'text' });

  }

  deleteKod_RolTip(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/kod_RolTips/', { body: { id: id } });
  }


}