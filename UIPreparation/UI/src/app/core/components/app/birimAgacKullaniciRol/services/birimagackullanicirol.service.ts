import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BirimAgacKullaniciRol } from '../models/BirimAgacKullaniciRol';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class BirimAgacKullaniciRolService {

  constructor(private httpClient: HttpClient) { }


  getBirimAgacKullaniciRolList(): Observable<BirimAgacKullaniciRol[]> {

    return this.httpClient.get<BirimAgacKullaniciRol[]>(environment.getApiUrl + '/birimAgacKullaniciRols/getall')
  }

  getBirimAgacKullaniciRolById(id: number): Observable<BirimAgacKullaniciRol> {
    return this.httpClient.get<BirimAgacKullaniciRol>(environment.getApiUrl + '/birimAgacKullaniciRols/getbyid?id='+id)
  }

  addBirimAgacKullaniciRol(birimAgacKullaniciRol: BirimAgacKullaniciRol): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/birimAgacKullaniciRols/', birimAgacKullaniciRol, { responseType: 'text' });
  }

  updateBirimAgacKullaniciRol(birimAgacKullaniciRol: BirimAgacKullaniciRol): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/birimAgacKullaniciRols/', birimAgacKullaniciRol, { responseType: 'text' });

  }

  deleteBirimAgacKullaniciRol(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/birimAgacKullaniciRols/', { body: { id: id } });
  }


}