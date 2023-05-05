import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Rol } from '../models/Rol';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class RolService {

  constructor(private httpClient: HttpClient) { }


  getRolList(): Observable<Rol[]> {

    return this.httpClient.get<Rol[]>(environment.getApiUrl + '/rols/getall')
  }

  getRolById(id: number): Observable<Rol> {
    return this.httpClient.get<Rol>(environment.getApiUrl + '/rols/getbyid?id='+id)
  }

  addRol(rol: Rol): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/rols/', rol, { responseType: 'text' });
  }

  updateRol(rol: Rol): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/rols/', rol, { responseType: 'text' });

  }

  deleteRol(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/rols/', { body: { id: id } });
  }


}