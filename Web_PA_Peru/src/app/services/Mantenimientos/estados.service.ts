import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const HttpUploadOptions = {
  headers: new HttpHeaders({ "Content-Type": "multipart/form-data" })
}

@Injectable({
  providedIn: 'root'
})

export class EstadosService {

  URL = environment.URL_API;
  estados:any[] = [];
  tipoOrdenTrabajo :any[] = [];
 

  constructor(private http:HttpClient) { }

  get_mostrar_estados(idEstado:number){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', String(idEstado));

    return this.http.get( this.URL + 'tblEstados' , {params: parametros});
  }

  get_verificar_estado(nombreArea:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '3');
    parametros = parametros.append('filtro', nombreArea);

    return this.http.get( this.URL + 'tblEstados' , {params: parametros}).toPromise();
  }

  set_save_estado(objMantenimiento:any){
    return this.http.post(this.URL + 'tblEstados', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_edit_area(objMantenimiento:any, id_area :number){
    return this.http.put(this.URL + 'tblEstados/' + id_area , JSON.stringify(objMantenimiento), httpOptions);
  }

  set_anular_estado(id_area : number){ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '2');
    parametros = parametros.append('filtro',  String(id_area));

    return this.http.get( this.URL + 'tblEstados' , {params: parametros});
  }


}
