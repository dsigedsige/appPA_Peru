import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertasService } from '../../../services/alertas/alertas.service';
import { RespuestaServer } from '../../../models/respuestaServer.models';
import { FuncionesglobalesService } from '../../../services/funciones/funcionesglobales.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from '../../../services/login/login.service';
import Swal from 'sweetalert2';
import { EstadosService } from '../../../services/Mantenimientos/estados.service';
declare const $:any;

@Component({
  selector: 'app-estados',
  templateUrl: './estados.component.html',
  styleUrls: ['./estados.component.css']
})

export class EstadosComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false
  
  estados :any[]=[]; 
  filtrarMantenimiento = "";
 
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService,private funcionesglobalesService : FuncionesglobalesService, private estadosService : EstadosService) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
  }
 
 ngOnInit(): void {
 
   this.inicializarFormularioFiltro();
   this.inicializarFormulario();
 }

 inicializarFormularioFiltro(){ 
    this.formParamsFiltro= new FormGroup({
      idEstado : new FormControl('1')
     }) 
 }

 inicializarFormulario(){ 
    this.formParams= new FormGroup({
      id_Estado : new FormControl('0'), 
      abreviatura_estado : new FormControl(''), 
      descripcion_estado :  new FormControl(''), 
      orden_estado :  new FormControl(''), 
      Backcolor_estado :  new FormControl('#ffffff'), 
      forecolor_estado :  new FormControl('#000000'), 
      estado :  new FormControl('0'), 
    }) 
 }  

 mostrarInformacion(){
      this.spinner.show();
      this.estadosService.get_mostrar_estados(this.formParamsFiltro.value.idEstado)
          .subscribe((res:RespuestaServer)=>{            
              this.spinner.hide();
              if (res.ok==true) {        
                  this.estados = res.data;  
              }else{
                this.alertasService.Swal_alert('error', JSON.stringify(res.data));
                alert(JSON.stringify(res.data));
              }
      })
 }   
  
 cerrarModal(){
    setTimeout(()=>{ // 
      $('#modal_mantenimiento').modal('hide');  
    },0); 
 }

 nuevo(){
    this.flag_modoEdicion = false;

    setTimeout(()=>{ // 
      $('#modal_mantenimiento').modal('show');
    },0);
    this.inicializarFormulario();    
 
 }


 async saveUpdate(){
  if ( this.flag_modoEdicion==true) { //// nuevo
     if (this.formParams.value.id_Estado == '' || this.formParams.value.id_Estado == 0) {
       this.alertasService.Swal_alert('error','No se cargó el id Estado, por favor actulize su página');
       return 
     }   
  }
 
  if (this.formParams.value.abreviatura_estado == '' || this.formParams.value.abreviatura_estado == 0) {
    this.alertasService.Swal_alert('error','Por favor ingrese el Nombre del Estado');
    return 
  }   
 
  if (this.formParams.value.orden_estado == '' || this.formParams.value.orden_estado == 0) {
    this.alertasService.Swal_alert('error','Por favor ingrese la orden');
    return 
  }
 
  this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });

  if ( this.flag_modoEdicion==false) { //// nuevo  

     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading();

     const  codEstado  = await this.estadosService.get_verificar_estado(this.formParams.value.abreviatura_estado);
     if (codEstado) {
      Swal.close();
      this.alertasService.Swal_alert('error','El estado ya esta registrada, verifique..');
      return;
     } 
 
     this.estadosService.set_save_estado(this.formParams.value).subscribe((res:RespuestaServer)=>{
       Swal.close();    
       if (res.ok ==true) {     
         this.flag_modoEdicion = true;
         this.formParams.patchValue({ "id_Estado" : Number(res.data[0].id_Estado) });

         console.log(res.data[0].id_Estado)
         this.estados.push(res.data[0])
         this.alertasService.Swal_Success('Se agrego correctamente..');
       }else{
         this.alertasService.Swal_alert('error', JSON.stringify(res.data));
         alert(JSON.stringify(res.data));
       }
     })
     
   }else{ /// editar

     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Actualizando, espere por favor'  })
     Swal.showLoading();
     this.estadosService.set_edit_area(this.formParams.value , this.formParams.value.id_Estado).subscribe((res:RespuestaServer)=>{
       Swal.close(); 
       if (res.ok ==true) {         
 
         for (const obj of this.estados) {
           if (obj.id_Estado == this.formParams.value.id_Estado ) {

             obj.abreviatura_estado = this.formParams.value.abreviatura_estado ;
             obj.descripcion_estado = this.formParams.value.descripcion_estado ;
             obj.orden_estado = this.formParams.value.orden_estado ;
             obj.Backcolor_estado = this.formParams.value.Backcolor_estado ;
             obj.forecolor_estado = this.formParams.value.forecolor_estado ;

             obj.estado = this.formParams.value.estado ;
             obj.descripcionEstado = this.formParams.value.estado == 0 ? "INACTIVO" : "ACTIVO";
             break;
           }
         }
         this.alertasService.Swal_Success('Se actualizo correctamente..');  
       }else{
         this.alertasService.Swal_alert('error', JSON.stringify(res.data));
         alert(JSON.stringify(res.data));
       }
     })
   }

 } 

 editar({  id_Estado , abreviatura_estado , descripcion_estado , orden_estado, Backcolor_estado , forecolor_estado, estado     }){

   this.flag_modoEdicion=true;

   this.formParams.patchValue({ "id_Estado" : id_Estado, "abreviatura_estado" :  abreviatura_estado , "descripcion_estado" : descripcion_estado,  "orden_estado" : orden_estado, 
                                "Backcolor_estado" : Backcolor_estado, "forecolor_estado" : forecolor_estado, "estado" : estado, "usuario_creacion" : this.idUserGlobal });

   setTimeout(()=>{ // 
    $('#modal_mantenimiento').modal('show');
  },0); 

 } 

 anular(objBD:any){

   if (objBD.estado ===0 || objBD.estado =='0') {      
     return;      
   }

   this.alertasService.Swal_Question('Sistemas', 'Esta seguro de anular ?')
   .then((result)=>{
     if(result.value){

       Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
       Swal.showLoading();
       this.estadosService.set_anular_estado(objBD.id_Estado ).subscribe((res:RespuestaServer)=>{
         Swal.close();        
         if (res.ok ==true) { 
           
           for (const user of this.estados) {
             if (user.id_Estado == objBD.id_Estado ) {               
                 user.estado = 0;
                 user.descripcionEstado =  "INACTIVO" ;
                 break;
             }
           }
           this.alertasService.Swal_Success('Se anulo correctamente..')  

         }else{
           this.alertasService.Swal_alert('error', JSON.stringify(res.data));
           alert(JSON.stringify(res.data));
         }
       })
        
     }
   }) 

 }
  

}
