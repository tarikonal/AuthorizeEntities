import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { KullaniciYetkiIslevEngel } from '../models/KullaniciYetkiIslevEngel';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class KullaniciYetkiIslevEngelService {

  constructor(private httpClient: HttpClient) { }


  getKullaniciYetkiIslevEngelList(): Observable<KullaniciYetkiIslevEngel[]> {

    return this.httpClient.get<KullaniciYetkiIslevEngel[]>(environment.getApiUrl + '/kullaniciYetkiIslevEngels/getall')
  }

  getKullaniciYetkiIslevEngelById(id: number): Observable<KullaniciYetkiIslevEngel> {
    return this.httpClient.get<KullaniciYetkiIslevEngel>(environment.getApiUrl + '/kullaniciYetkiIslevEngels/getbyid?id='+id)
  }

  addKullaniciYetkiIslevEngel(kullaniciYetkiIslevEngel: KullaniciYetkiIslevEngel): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/kullaniciYetkiIslevEngels/', kullaniciYetkiIslevEngel, { responseType: 'text' });
  }

  updateKullaniciYetkiIslevEngel(kullaniciYetkiIslevEngel: KullaniciYetkiIslevEngel): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/kullaniciYetkiIslevEngels/', kullaniciYetkiIslevEngel, { responseType: 'text' });

  }

  deleteKullaniciYetkiIslevEngel(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/kullaniciYetkiIslevEngels/', { body: { id: id } });
  }


}