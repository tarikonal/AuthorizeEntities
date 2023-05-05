import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { KullaniciMenuIslevObje } from '../models/KullaniciMenuIslevObje';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class KullaniciMenuIslevObjeService {

  constructor(private httpClient: HttpClient) { }


  getKullaniciMenuIslevObjeList(): Observable<KullaniciMenuIslevObje[]> {

    return this.httpClient.get<KullaniciMenuIslevObje[]>(environment.getApiUrl + '/kullaniciMenuIslevObjes/getall')
  }

  getKullaniciMenuIslevObjeById(id: number): Observable<KullaniciMenuIslevObje> {
    return this.httpClient.get<KullaniciMenuIslevObje>(environment.getApiUrl + '/kullaniciMenuIslevObjes/getbyid?id='+id)
  }

  addKullaniciMenuIslevObje(kullaniciMenuIslevObje: KullaniciMenuIslevObje): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/kullaniciMenuIslevObjes/', kullaniciMenuIslevObje, { responseType: 'text' });
  }

  updateKullaniciMenuIslevObje(kullaniciMenuIslevObje: KullaniciMenuIslevObje): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/kullaniciMenuIslevObjes/', kullaniciMenuIslevObje, { responseType: 'text' });

  }

  deleteKullaniciMenuIslevObje(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/kullaniciMenuIslevObjes/', { body: { id: id } });
  }


}