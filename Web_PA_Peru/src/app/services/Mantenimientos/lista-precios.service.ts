import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const HttpUploadOptions = {
  headers: new HttpHeaders({ "Content-Type": "multipart/form-data" })
}
@Injectable({
  providedIn: 'root'
})
export class ListaPreciosService {

  URL = environment.URL_API;
  estados:any[] = [];
  tipoOrdenTrabajo :any[] = [];

  tipoPrecios :any[]=[];
  tipoMateriales :any[]=[]; 
  prioridades :any[]=[]; 

  empresas :any[]=[]; 
  tiposMaterialPrecio :any[]=[]; 
  baremos :any[]=[]; 

  constructor(private http:HttpClient) { }





  get_estados(){
    if (this.estados.length > 0) {
      return of( this.estados )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '1');
      parametros = parametros.append('filtro', '');
  
      return this.http.get( this.URL + 'tblPersonal' , {params: parametros})
                 .pipe(map((res:any)=>{
                       this.estados = res.data;
                       return res.data;
                  }) );
    }
  }

  get_tipoOrdenTrabajo(){
    if (this.tipoOrdenTrabajo.length > 0) {
      return of( this.tipoOrdenTrabajo )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '6');
      parametros = parametros.append('filtro', '2');
  
      return this.http.get( this.URL + 'tblPersonal' , {params: parametros})
                 .pipe(map((res:any)=>{
                       this.tipoOrdenTrabajo = res.data;
                       return res.data;
                  }) );
    }
  } 

  get_tipoPrecios(){
    if (this.tipoPrecios.length > 0) {
      return of( this.tipoPrecios )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '6');
      parametros = parametros.append('filtro', '7');
  
      return this.http.get( this.URL + 'tblPersonal' , {params: parametros})
                 .pipe(map((res:any)=>{
                       this.tipoPrecios = res.data;
                       return res.data;
                  }) );
    }
  } 

  get_tiposMateriales(){
    if (this.tipoMateriales.length > 0) {
      return of( this.tipoMateriales )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '6');
      parametros = parametros.append('filtro', '4');
  
      return this.http.get( this.URL + 'tblPersonal' , {params: parametros})
                 .pipe(map((res:any)=>{
                       this.tipoMateriales = res.data;
                       return res.data;
                  }) );
    }
  } 

  get_prioridades(){
    if (this.prioridades.length > 0) {
      return of( this.prioridades )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '6');
      parametros = parametros.append('filtro', '8');
  
      return this.http.get( this.URL + 'tblPersonal' , {params: parametros})
                 .pipe(map((res:any)=>{
                       this.prioridades = res.data;
                       return res.data;
                  }) );
    }
  } 



  get_mostrarPrecio_general(idtipoOrdenT:number, idEstado:number){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', idtipoOrdenT + '|' +  idEstado );

    return this.http.get( this.URL + 'tblPrecios' , {params: parametros});
  }

  get_verificarDni(nroDoc:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '5');
    parametros = parametros.append('filtro', nroDoc);

    return this.http.get( this.URL + 'tblPrecios' , {params: parametros}).toPromise();
  }
  
  set_savePrecio(objPrecio:any){
    return this.http.post(this.URL + 'tblPrecios', JSON.stringify(objPrecio), httpOptions);
  }

  set_editPrecio(objPrecio:any, id_Precio :number){
    return this.http.put(this.URL + 'tblPrecios/' + id_Precio , JSON.stringify(objPrecio), httpOptions);
  }

  set_anularPrecio(id_Precio : number){ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '2');
    parametros = parametros.append('filtro',  String(id_Precio));

    return this.http.get( this.URL + 'tblPrecios' , {params: parametros});
  }
  
  //----------------------------
  //----PRECIO POR EMPRESA------
  //----------------------------


  get_empresas(){
    if (this.empresas.length > 0) {
      return of( this.empresas )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '3');
      parametros = parametros.append('filtro', '');
  
      return this.http.get( this.URL + 'tblPrecios' , {params: parametros})
                 .pipe(map((res:any)=>{
                       this.empresas = res.data;
                       return res.data;
                  }) );
    }
  }

  get_tiposMaterial_precioEmpresa(){
    if (this.tiposMaterialPrecio.length > 0) {
      return of( this.tiposMaterialPrecio )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '4');
      parametros = parametros.append('filtro', '');
  
      return this.http.get( this.URL + 'tblPrecios' , {params: parametros})
                 .pipe(map((res:any)=>{
                       this.tiposMaterialPrecio = res.data;
                       return res.data;
                  }) );
    }
  }

  get_baremos(){
    if (this.baremos.length > 0) {
      return of( this.baremos )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '5');
      parametros = parametros.append('filtro', '');
  
      return this.http.get( this.URL + 'tblPrecios' , {params: parametros})
                 .pipe(map((res:any)=>{
                       this.baremos = res.data;
                       return res.data;
                  }) );
    }
  }  

  get_mostrarPrecio_empresa(idEmpresa:number, idEstado:number){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '6');
    parametros = parametros.append('filtro', idEmpresa + '|' +  idEstado );

    return this.http.get( this.URL + 'tblPrecios' , {params: parametros});
  }

  set_savePrecioUpdate_empresa(objPrecio:any){
    console.log(JSON.stringify(objPrecio))
    return this.http.post(this.URL + 'PrecioMaterial/post_precioMaterial', JSON.stringify(objPrecio), httpOptions);
  }

  set_editPrecio_empresa(objPrecio:any, id_Precio :number){
    return this.http.put(this.URL + 'tblPrecios/' + id_Precio , JSON.stringify(objPrecio), httpOptions);
  }

  get_verificar_empresa_tipoMaterial_baremo(id_empresa:number, id_TipoMaterial:number, id_Baremo:number){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '7');
    parametros = parametros.append('filtro', id_empresa + '|' +  id_TipoMaterial + '|' +  id_Baremo );

    return this.http.get( this.URL + 'tblPrecios' , {params: parametros}).toPromise();
  }

  get_mostrarPrecio_empresaDetalle(idEmpresa:number ){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '8');
    parametros = parametros.append('filtro', String(idEmpresa) );

    return this.http.get( this.URL + 'tblPrecios' , {params: parametros});
  }

  set_anularPrecio_Empresa(id_precioMaterial : number){ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '9');
    parametros = parametros.append('filtro',  String(id_precioMaterial));

    return this.http.get( this.URL + 'tblPrecios' , {params: parametros});
  }

  set_save_agregarMantenimiento( {codigo , descripcion, precio, tipoMantenimiento, usuario_creacion} ){
 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '10');
    parametros = parametros.append('filtro',  String(codigo)+ '|' + descripcion + '|' + precio + '|' + tipoMantenimiento + '|' + usuario_creacion );

    return this.http.get( this.URL + 'tblPrecios' , {params: parametros});

  }




}
