import { Component, OnInit } from '@angular/core';
import { WebsocketService } from './services/sockets/websocket.service';
import { LoginService } from './services/login/login.service';
 
import { Subscription } from 'rxjs';
import { AlertasService } from './services/alertas/alertas.service';
import { RespuestaServer } from './models/respuestaServer.models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
 

  alertaSocket$: Subscription;
  idUserGlobal = 0;
  idPerfil =0;
  serviciosMenu :any[]=[]; 
  notificaciones :any[]=[]; 


  constructor( public websocketService : WebsocketService, private loginService: LoginService, private alertasService : AlertasService ){
    this.idUserGlobal = this.loginService.get_idUsuario(); 
    this.idPerfil = this.loginService.get_idPerfil(); 
    this.serviciosMenu = this.loginService.getServiciosMenu();
  }

  ngOnInit() { 
    this.recibirAlertasSocket();
    const x = setInterval(() => { this.enviarNotificacionSocket() }, 60000);
   }

    

   public recibirAlertasSocket(){
    this.alertaSocket$ = this.websocketService.escucharEventos('Alertas_movil_web_OT').subscribe((res:any)=>{
      console.log('entro lo que devuelve movil')
      console.log(res);
      this.notificaciones = JSON.parse(res);     

      for (const servicio of this.serviciosMenu) {
        for (const noti of this.notificaciones) {  
          if (servicio.id_servicio == noti.idServicio ) {
            this.alertasService.Swal_Success_Socket('Notificacion', noti.mensaje)
          }
        }
      }
 
    })
   }
 
   public enviarNotificacionSocket(){
    this.websocketService.get_mensajeAutomatico_vencimientoOT(this.idUserGlobal)
    .subscribe((res:RespuestaServer)=>{  
        if (res.ok==true) {    

          if (res.data.length > 0) {

            ////-- notificaciones Socket para el movil-----------            
            this.websocketService.NotificacionOT_WebSocket(res.data)
            .then( (res:any) =>{
              if (res.ok==true) {
                console.log(res.data);
              }else{
                this.alertasService.Swal_alert('Error Socket', JSON.stringify(res.data));
                alert(JSON.stringify(res.data));
              }
            }).catch((error)=>{
              this.alertasService.Swal_alert('Error Socket', JSON.stringify(error));
              alert(JSON.stringify(error));
            })  

            ////-- Fin de notificaciones Socket para el movil----
          }


        }else{
          this.alertasService.Swal_alert('error', JSON.stringify(res.data));
          alert(JSON.stringify(res.data));
        }
    })
   



   }
 
   ngOnDestroy(){
     this.alertaSocket$.unsubscribe();
   }


}
