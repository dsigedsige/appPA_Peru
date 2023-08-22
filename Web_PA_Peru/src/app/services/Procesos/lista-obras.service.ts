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
export class ListaObrasService {
  URL = environment.URL_API;
  estados :any [] = [];

  constructor(private http:HttpClient) { }

  get_area(){
    // debugger;
      return this.http.get( this.URL + 'ListaObras/GetArea')
  }

  get_cuadrilla(area:string){
    let parametros = new HttpParams();
    parametros = parametros.append('area', area);
    return this.http.get( this.URL + 'ListaObras/GetCuadrilla', { params: parametros })
  }

  get_estado(){
    return this.http.get( this.URL + 'ListaObras/GetEstado')
  }

  get_listarObras_todos( servicio:string, cuadrilla:string, fechaInicio:string,fechaFin:string,estado:string){
    let parametros = new HttpParams();
    parametros = parametros.append('servicio', servicio);
    parametros = parametros.append('cuadrilla', cuadrilla);
    parametros = parametros.append('fechaInicio', fechaInicio);
    parametros = parametros.append('fechaFin', fechaFin);
    parametros = parametros.append('estado', estado);
    return this.http.get( this.URL + 'ListaObras/GetListaObras' , {params: parametros})
  }


  get_listarObras_fotos( GesObraCodigo:string, Usuario:string){
    let parametros = new HttpParams();
    parametros = parametros.append('GesObraCodigo', GesObraCodigo);
    parametros = parametros.append('Usuario', Usuario.toString());
    return this.http.get( this.URL + 'ListaObras/GetFotosObras' , {params: parametros})
  }

  set_anular_fotos(IdObraEjecucion:string, Usuario:string){
    debugger;
    let parametros = new HttpParams();
    parametros = parametros.append('IdObraEjecucion',IdObraEjecucion.toString());
    parametros = parametros.append('Usuario', Usuario.toString());
    return this.http.get( this.URL + 'ListaObras/setAnularFotosObras' , {params: parametros})
  }


  get_descargarFotosObra_todos( GesObraCodigo:string, Usuario:string){
    debugger;
    let parametros = new HttpParams();
    parametros = parametros.append('GesObraCodigo', GesObraCodigo);
    parametros = parametros.append('Usuario', Usuario.toString());
    return this.http.get( this.URL + 'ListaObras/descargarFotosObrasTodo' , {params: parametros})
  }



  upload_adjuntarArchivoObras(file:any, GesObraCodigo:string, LatitudFoto : string,LongitudFoto:string,Usuario:string) {
    const formData = new FormData();
    formData.append('file', file);
    const filtro = GesObraCodigo + '|' + LatitudFoto + '|' + LongitudFoto + '|' + Usuario;
    return this.http.post(this.URL + 'ListaObras/PostAdjuntarObraFoto_v2?filtros=' + filtro, formData);

  }


}


