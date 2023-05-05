import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BirimYetkiIslevObje } from '../models/BirimYetkiIslevObje';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class BirimYetkiIslevObjeService {

  constructor(private httpClient: HttpClient) { }


  getBirimYetkiIslevObjeList(): Observable<BirimYetkiIslevObje[]> {

    return this.httpClient.get<BirimYetkiIslevObje[]>(environment.getApiUrl + '/birimYetkiIslevObjes/getall')
  }

  getBirimYetkiIslevObjeById(id: number): Observable<BirimYetkiIslevObje> {
    return this.httpClient.get<BirimYetkiIslevObje>(environment.getApiUrl + '/birimYetkiIslevObjes/getbyid?id='+id)
  }

  addBirimYetkiIslevObje(birimYetkiIslevObje: BirimYetkiIslevObje): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/birimYetkiIslevObjes/', birimYetkiIslevObje, { responseType: 'text' });
  }

  updateBirimYetkiIslevObje(birimYetkiIslevObje: BirimYetkiIslevObje): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/birimYetkiIslevObjes/', birimYetkiIslevObje, { responseType: 'text' });

  }

  deleteBirimYetkiIslevObje(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/birimYetkiIslevObjes/', { body: { id: id } });
  }


}