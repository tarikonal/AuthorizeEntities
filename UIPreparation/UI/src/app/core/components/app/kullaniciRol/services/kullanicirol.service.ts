import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { KullaniciRol } from '../models/KullaniciRol';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class KullaniciRolService {

  constructor(private httpClient: HttpClient) { }


  getKullaniciRolList(): Observable<KullaniciRol[]> {

    return this.httpClient.get<KullaniciRol[]>(environment.getApiUrl + '/kullaniciRols/getall')
  }

  getKullaniciRolById(id: number): Observable<KullaniciRol> {
    return this.httpClient.get<KullaniciRol>(environment.getApiUrl + '/kullaniciRols/getbyid?id='+id)
  }

  addKullaniciRol(kullaniciRol: KullaniciRol): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/kullaniciRols/', kullaniciRol, { responseType: 'text' });
  }

  updateKullaniciRol(kullaniciRol: KullaniciRol): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/kullaniciRols/', kullaniciRol, { responseType: 'text' });

  }

  deleteKullaniciRol(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/kullaniciRols/', { body: { id: id } });
  }


}