import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Kod_RolSeviye } from '../models/Kod_RolSeviye';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class Kod_RolSeviyeService {

  constructor(private httpClient: HttpClient) { }


  getKod_RolSeviyeList(): Observable<Kod_RolSeviye[]> {

    return this.httpClient.get<Kod_RolSeviye[]>(environment.getApiUrl + '/kod_RolSeviyes/getall')
  }

  getKod_RolSeviyeById(id: number): Observable<Kod_RolSeviye> {
    return this.httpClient.get<Kod_RolSeviye>(environment.getApiUrl + '/kod_RolSeviyes/getbyid?id='+id)
  }

  addKod_RolSeviye(kod_RolSeviye: Kod_RolSeviye): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/kod_RolSeviyes/', kod_RolSeviye, { responseType: 'text' });
  }

  updateKod_RolSeviye(kod_RolSeviye: Kod_RolSeviye): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/kod_RolSeviyes/', kod_RolSeviye, { responseType: 'text' });

  }

  deleteKod_RolSeviye(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/kod_RolSeviyes/', { body: { id: id } });
  }


}