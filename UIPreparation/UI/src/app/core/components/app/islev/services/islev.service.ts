import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Islev } from '../models/Islev';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class IslevService {

  constructor(private httpClient: HttpClient) { }


  getIslevList(): Observable<Islev[]> {

    return this.httpClient.get<Islev[]>(environment.getApiUrl + '/islevs/getall')
  }

  getIslevById(id: number): Observable<Islev> {
    return this.httpClient.get<Islev>(environment.getApiUrl + '/islevs/getbyid?id='+id)
  }

  addIslev(islev: Islev): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/islevs/', islev, { responseType: 'text' });
  }

  updateIslev(islev: Islev): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/islevs/', islev, { responseType: 'text' });

  }

  deleteIslev(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/islevs/', { body: { id: id } });
  }


}