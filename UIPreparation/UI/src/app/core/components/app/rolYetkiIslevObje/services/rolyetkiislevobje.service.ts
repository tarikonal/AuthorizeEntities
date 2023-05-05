import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RolYetkiIslevObje } from '../models/RolYetkiIslevObje';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class RolYetkiIslevObjeService {

  constructor(private httpClient: HttpClient) { }


  getRolYetkiIslevObjeList(): Observable<RolYetkiIslevObje[]> {

    return this.httpClient.get<RolYetkiIslevObje[]>(environment.getApiUrl + '/rolYetkiIslevObjes/getall')
  }

  getRolYetkiIslevObjeById(id: number): Observable<RolYetkiIslevObje> {
    return this.httpClient.get<RolYetkiIslevObje>(environment.getApiUrl + '/rolYetkiIslevObjes/getbyid?id='+id)
  }

  addRolYetkiIslevObje(rolYetkiIslevObje: RolYetkiIslevObje): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/rolYetkiIslevObjes/', rolYetkiIslevObje, { responseType: 'text' });
  }

  updateRolYetkiIslevObje(rolYetkiIslevObje: RolYetkiIslevObje): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/rolYetkiIslevObjes/', rolYetkiIslevObje, { responseType: 'text' });

  }

  deleteRolYetkiIslevObje(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/rolYetkiIslevObjes/', { body: { id: id } });
  }


}