import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Birim } from '../models/Birim';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class BirimService {

  constructor(private httpClient: HttpClient) { }


  getBirimList(): Observable<Birim[]> {

    return this.httpClient.get<Birim[]>(environment.getApiUrl + '/birims/getall')
  }

  getBirimById(id: number): Observable<Birim> {
    return this.httpClient.get<Birim>(environment.getApiUrl + '/birims/getbyid?id='+id)
  }

  addBirim(birim: Birim): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/birims/', birim, { responseType: 'text' });
  }

  updateBirim(birim: Birim): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/birims/', birim, { responseType: 'text' });

  }

  deleteBirim(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/birims/', { body: { id: id } });
  }


}