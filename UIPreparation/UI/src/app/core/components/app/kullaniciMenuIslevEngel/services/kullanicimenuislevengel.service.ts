import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { KullaniciMenuIslevEngel } from '../models/KullaniciMenuIslevEngel';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class KullaniciMenuIslevEngelService {

  constructor(private httpClient: HttpClient) { }


  getKullaniciMenuIslevEngelList(): Observable<KullaniciMenuIslevEngel[]> {

    return this.httpClient.get<KullaniciMenuIslevEngel[]>(environment.getApiUrl + '/kullaniciMenuIslevEngels/getall')
  }

  getKullaniciMenuIslevEngelById(id: number): Observable<KullaniciMenuIslevEngel> {
    return this.httpClient.get<KullaniciMenuIslevEngel>(environment.getApiUrl + '/kullaniciMenuIslevEngels/getbyid?id='+id)
  }

  addKullaniciMenuIslevEngel(kullaniciMenuIslevEngel: KullaniciMenuIslevEngel): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/kullaniciMenuIslevEngels/', kullaniciMenuIslevEngel, { responseType: 'text' });
  }

  updateKullaniciMenuIslevEngel(kullaniciMenuIslevEngel: KullaniciMenuIslevEngel): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/kullaniciMenuIslevEngels/', kullaniciMenuIslevEngel, { responseType: 'text' });

  }

  deleteKullaniciMenuIslevEngel(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/kullaniciMenuIslevEngels/', { body: { id: id } });
  }


}