import { Injectable } from '@angular/core';
import { Socket } from 'ngx-socket-io';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';

 
@Injectable({
  providedIn: 'root'
})
export class WebsocketService {

  public socketStatus = false;
  URL = environment.URL_API;

  constructor(private socket: Socket ,private http:HttpClient) { 
    this.checkStatus();
  }

  checkStatus(){
    this.socket.on('connect', ()=>{
      console.log('conectado al servidor');
      this.socketStatus= true;
    })
 
    this.socket.on('disconnect', ()=>{
      console.log('desconectado del servidor');
      this.socketStatus= false;
    })
  }

  emitirEventos(evento:string, payload ?:any, callback?:Function){  /// emit
      this.socket.emit(evento,payload, callback);
  }

  escucharEventos(evento:string){   /// on 
    return this.socket.fromEvent(evento);
  }
 
  NotificacionOT_WebSocket( obj_data :any){
    //como no sabemos cuando termina el socket lo metemos dentro de una promesa
    return new Promise((resolve, reject) => {      
      this.emitirEventos('Notificacion_web_OT', JSON.stringify(obj_data) , (resp)=>{ 
        if (resp.ok==true) {
          resolve(resp);
        }else{
          reject(resp);
        }  
      })
    })
  }


  get_mensajeAutomatico_vencimientoOT(idUsuario:number){  
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '17');
    parametros = parametros.append('filtro', String(idUsuario) );
 
    return this.http.get( this.URL + 'OrdenTrabajo' , {params: parametros});
  }
  


}
