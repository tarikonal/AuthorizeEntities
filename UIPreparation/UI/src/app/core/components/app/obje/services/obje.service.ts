import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Obje } from '../models/Obje';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ObjeService {

  constructor(private httpClient: HttpClient) { }


  getObjeList(): Observable<Obje[]> {

    return this.httpClient.get<Obje[]>(environment.getApiUrl + '/objes/getall')
  }

  getObjeById(id: number): Observable<Obje> {
    return this.httpClient.get<Obje>(environment.getApiUrl + '/objes/getbyid?id='+id)
  }

  addObje(obje: Obje): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/objes/', obje, { responseType: 'text' });
  }

  updateObje(obje: Obje): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/objes/', obje, { responseType: 'text' });

  }

  deleteObje(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/objes/', { body: { id: id } });
  }


}