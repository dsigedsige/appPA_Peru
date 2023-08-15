import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertasService } from '../../../services/alertas/alertas.service';
import { RespuestaServer } from '../../../models/respuestaServer.models';
import { FuncionesglobalesService } from '../../../services/funciones/funcionesglobales.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from '../../../services/login/login.service';
import Swal from 'sweetalert2';
import { AreaService } from '../../../services/Mantenimientos/area.service';
 

declare const $:any;
@Component({
  selector: 'app-areas',
  templateUrl: './areas.component.html',
  styleUrls: ['./areas.component.css']
})
 
export class AreasComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false
  
  servicios :any[]=[]; 
  filtrarMantenimiento = "";
 
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService,private funcionGlobalServices : FuncionesglobalesService, private funcionesglobalesService : FuncionesglobalesService, private areaService : AreaService) {         
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
      id_Servicios: new FormControl('0'), 
      nombreServicio: new FormControl(''),
      id_Patron : new FormControl('0'),   
      tiempo_Servicio : new FormControl('0'),   
      estado : new FormControl('1'),   
      usuario_creacion : new FormControl('')
    }) 
 }  

 mostrarInformacion(){
      this.spinner.show();
      this.areaService.get_mostrar_area(this.formParamsFiltro.value.idEstado)
          .subscribe((res:RespuestaServer)=>{            
              this.spinner.hide();
              if (res.ok==true) {        
                  this.servicios = res.data;  
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
     if (this.formParams.value.id_Servicios == '' || this.formParams.value.id_Servicios == 0) {
       this.alertasService.Swal_alert('error','No se cargó el id del Servicio, por favor actulize su página');
       return 
     }   
  }
 
  if (this.formParams.value.nombreServicio == '' || this.formParams.value.nombreServicio == 0) {
    this.alertasService.Swal_alert('error','Por favor ingrese la descripcion del Servicio');
    return 
  }
  
  if (this.formParams.value.id_Patron == '' || this.formParams.value.id_Patron == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Patron');
    return 
  }
  if (this.formParams.value.tiempo_Servicio == '' || this.formParams.value.tiempo_Servicio == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Tiempo del Servicio');
    return 
  }
 
  this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });

  if ( this.flag_modoEdicion==false) { //// nuevo  

     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading();

     const  cargoPer  = await this.areaService.get_verificar_area(this.formParams.value.nombreServicio);
     if (cargoPer) {
      Swal.close();
      this.alertasService.Swal_alert('error','El servicio ya esta registrada, verifique..');
      return;
     } 
 
     this.areaService.set_save_area(this.formParams.value).subscribe((res:RespuestaServer)=>{
       Swal.close();    
       if (res.ok ==true) {     
         this.flag_modoEdicion = true;
         this.formParams.patchValue({ "id_Servicios" : Number(res.data[0].id_Servicios) });

         console.log(res.data[0].id_Servicios)
         this.servicios.push(res.data[0])
         this.alertasService.Swal_Success('Se agrego correctamente..');
       }else{
         this.alertasService.Swal_alert('error', JSON.stringify(res.data));
         alert(JSON.stringify(res.data));
       }
     })
     
   }else{ /// editar

     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Actualizando, espere por favor'  })
     Swal.showLoading();
     this.areaService.set_edit_area(this.formParams.value , this.formParams.value.id_Servicios).subscribe((res:RespuestaServer)=>{
       Swal.close(); 
       if (res.ok ==true) {   
         
         for (const obj of this.servicios) {
           if (obj.id_Servicios == this.formParams.value.id_Servicios ) {
             obj.nombreServicio = this.formParams.value.nombreServicio ;
             obj.id_Patron = this.formParams.value.id_Patron ;
             obj.tiempo_Servicio = this.formParams.value.tiempo_Servicio ;
             obj.estado = this.formParams.value.estado ;
             obj.descripcion_estado = this.formParams.value.estado == 0 ? "INACTIVO" : "ACTIVO";
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

 editar({   id_Servicios, nombreServicio, id_Patron , tiempo_Servicio ,estado}){

   this.flag_modoEdicion=true;

   this.formParams.patchValue({ "id_Servicios" : id_Servicios, "nombreServicio" :  nombreServicio , "id_Patron" : id_Patron,  "tiempo_Servicio" : tiempo_Servicio,  "estado" : estado, "usuario_creacion" : this.idUserGlobal });

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
       this.areaService.set_anular_area(objBD.id_Servicios ).subscribe((res:RespuestaServer)=>{
         Swal.close();        
         if (res.ok ==true) { 
           
           for (const user of this.servicios) {
             if (user.id_Servicios == objBD.id_Servicios ) {               
                 user.estado = 0;
                 user.descripcion_estado =  "INACTIVO" ;
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

