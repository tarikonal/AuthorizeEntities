import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Proje } from '../models/proje';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ProjeService {

  constructor(private httpClient: HttpClient) { }


  getProjeList(): Observable<Proje[]> {

    return this.httpClient.get<Proje[]>(environment.getApiUrl + '/projes/getall')
  }

  getProjeById(id: number): Observable<Proje> {
    return this.httpClient.get<Proje>(environment.getApiUrl + '/projes/getbyid?id='+id)
  }

  addProje(proje: Proje): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/projes/', proje, { responseType: 'text' });
  }

  updateProje(proje: Proje): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/projes/', proje, { responseType: 'text' });

  }

  deleteProje(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/projes/', { body: { id: id } });
  }


}