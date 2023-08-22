import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const HttpUploadOptions = {
  headers: new HttpHeaders({ "Content-Type": "multipart/form-data" })
}


@Injectable({
  providedIn: 'root'
})
export class GuiaSap {
  URL = environment.URL_API;
  estados :any [] = [];

  constructor(private http:HttpClient) { }

  insertGuiaSap(model:GuiaSap) {
    return this.http.post(`${this.URL}/Guia/setInsertGuias`, model);
  }


}

