import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RolMenuIslevObje } from '../models/RolMenuIslevObje';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class RolMenuIslevObjeService {

  constructor(private httpClient: HttpClient) { }


  getRolMenuIslevObjeList(): Observable<RolMenuIslevObje[]> {

    return this.httpClient.get<RolMenuIslevObje[]>(environment.getApiUrl + '/rolMenuIslevObjes/getall')
  }

  getRolMenuIslevObjeById(id: number): Observable<RolMenuIslevObje> {
    return this.httpClient.get<RolMenuIslevObje>(environment.getApiUrl + '/rolMenuIslevObjes/getbyid?id='+id)
  }

  addRolMenuIslevObje(rolMenuIslevObje: RolMenuIslevObje): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/rolMenuIslevObjes/', rolMenuIslevObje, { responseType: 'text' });
  }

  updateRolMenuIslevObje(rolMenuIslevObje: RolMenuIslevObje): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/rolMenuIslevObjes/', rolMenuIslevObje, { responseType: 'text' });

  }

  deleteRolMenuIslevObje(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/rolMenuIslevObjes/', { body: { id: id } });
  }


}