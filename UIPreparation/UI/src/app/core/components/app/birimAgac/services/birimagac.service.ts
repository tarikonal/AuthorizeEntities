import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BirimAgac } from '../models/BirimAgac';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class BirimAgacService {

  constructor(private httpClient: HttpClient) { }


  getBirimAgacList(): Observable<BirimAgac[]> {

    return this.httpClient.get<BirimAgac[]>(environment.getApiUrl + '/birimAgacs/getall')
  }

  getBirimAgacById(id: number): Observable<BirimAgac> {
    return this.httpClient.get<BirimAgac>(environment.getApiUrl + '/birimAgacs/getbyid?id='+id)
  }

  addBirimAgac(birimAgac: BirimAgac): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/birimAgacs/', birimAgac, { responseType: 'text' });
  }

  updateBirimAgac(birimAgac: BirimAgac): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/birimAgacs/', birimAgac, { responseType: 'text' });

  }

  deleteBirimAgac(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/birimAgacs/', { body: { id: id } });
  }


}