import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { GuiaSap } from 'src/app/models/guiasap.models';
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
export class GuiaSapService {
  URL = environment.URL_API;
  estados :any [] = [];

  constructor(private http:HttpClient) { }

  get_movimiento(){
    return this.http.get( this.URL + 'Guia/GetMovimiento')
  }


  get_listarGuias( movimiento:string, fecha:string){
    let parametros = new HttpParams();
    parametros = parametros.append('movimiento', movimiento);
    parametros = parametros.append('fecha', fecha);
    return this.http.get( this.URL + 'Guia/GetGuias' , {params: parametros})
  }

  
  insertGuiaSap(model:GuiaSap) {
    return this.http.post(`${this.URL}/Guia/setInsertGuias`, model);
  }


}

