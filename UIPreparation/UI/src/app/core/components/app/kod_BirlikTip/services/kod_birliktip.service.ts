import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Kod_BirlikTip } from '../models/Kod_BirlikTip';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class Kod_BirlikTipService {

  constructor(private httpClient: HttpClient) { }


  getKod_BirlikTipList(): Observable<Kod_BirlikTip[]> {

    return this.httpClient.get<Kod_BirlikTip[]>(environment.getApiUrl + '/kod_BirlikTips/getall')
  }

  getKod_BirlikTipById(id: number): Observable<Kod_BirlikTip> {
    return this.httpClient.get<Kod_BirlikTip>(environment.getApiUrl + '/kod_BirlikTips/getbyid?id='+id)
  }

  addKod_BirlikTip(kod_BirlikTip: Kod_BirlikTip): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/kod_BirlikTips/', kod_BirlikTip, { responseType: 'text' });
  }

  updateKod_BirlikTip(kod_BirlikTip: Kod_BirlikTip): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/kod_BirlikTips/', kod_BirlikTip, { responseType: 'text' });

  }

  deleteKod_BirlikTip(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/kod_BirlikTips/', { body: { id: id } });
  }


}