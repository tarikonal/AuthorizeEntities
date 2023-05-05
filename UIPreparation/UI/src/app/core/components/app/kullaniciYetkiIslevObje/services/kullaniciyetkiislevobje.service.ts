import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { KullaniciYetkiIslevObje } from '../models/KullaniciYetkiIslevObje';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class KullaniciYetkiIslevObjeService {

  constructor(private httpClient: HttpClient) { }


  getKullaniciYetkiIslevObjeList(): Observable<KullaniciYetkiIslevObje[]> {

    return this.httpClient.get<KullaniciYetkiIslevObje[]>(environment.getApiUrl + '/kullaniciYetkiIslevObjes/getall')
  }

  getKullaniciYetkiIslevObjeById(id: number): Observable<KullaniciYetkiIslevObje> {
    return this.httpClient.get<KullaniciYetkiIslevObje>(environment.getApiUrl + '/kullaniciYetkiIslevObjes/getbyid?id='+id)
  }

  addKullaniciYetkiIslevObje(kullaniciYetkiIslevObje: KullaniciYetkiIslevObje): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/kullaniciYetkiIslevObjes/', kullaniciYetkiIslevObje, { responseType: 'text' });
  }

  updateKullaniciYetkiIslevObje(kullaniciYetkiIslevObje: KullaniciYetkiIslevObje): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/kullaniciYetkiIslevObjes/', kullaniciYetkiIslevObje, { responseType: 'text' });

  }

  deleteKullaniciYetkiIslevObje(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/kullaniciYetkiIslevObjes/', { body: { id: id } });
  }


}