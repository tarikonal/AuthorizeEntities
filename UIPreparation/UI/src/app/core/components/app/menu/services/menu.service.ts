import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Menu } from '../models/Menu';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(private httpClient: HttpClient) { }


  getMenuList(): Observable<Menu[]> {

    return this.httpClient.get<Menu[]>(environment.getApiUrl + '/menus/getall')
  }

  getMenuById(id: number): Observable<Menu> {
    return this.httpClient.get<Menu>(environment.getApiUrl + '/menus/getbyid?id='+id)
  }

  addMenu(menu: Menu): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/menus/', menu, { responseType: 'text' });
  }

  updateMenu(menu: Menu): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/menus/', menu, { responseType: 'text' });

  }

  deleteMenu(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/menus/', { body: { id: id } });
  }


}