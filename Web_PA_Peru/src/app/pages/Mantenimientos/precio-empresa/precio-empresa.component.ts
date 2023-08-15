
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertasService } from '../../../services/alertas/alertas.service';
import { RespuestaServer } from '../../../models/respuestaServer.models';
import { FuncionesglobalesService } from '../../../services/funciones/funcionesglobales.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from '../../../services/login/login.service';
import { from, combineLatest } from 'rxjs';
import Swal from 'sweetalert2';
 
import { UploadService } from '../../../services/Upload/upload.service';
import { ListaPreciosService } from '../../../services/Mantenimientos/lista-precios.service';

declare var $:any;

@Component({
  selector: 'app-precio-empresa',
  templateUrl: './precio-empresa.component.html',
  styleUrls: ['./precio-empresa.component.css']
})
 
export class PrecioEmpresaComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;
  formParamsAgregar: FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false
  empresas :any[]=[];   
  tiposMaterial :any[]=[];   
  baremos :any[]=[];   

  preciosEmpresas  :any[]=[];   
  preciosEmpresas_Detalle  :any[]=[];   
 
  filtrarMantenimiento = "";
  flagCubiculo =false;
  tipoMantenimiento_Modal:number = 0;
 
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService,private funcionGlobalServices : FuncionesglobalesService, private funcionesglobalesService : FuncionesglobalesService, private listaPreciosService : ListaPreciosService, private uploadService : UploadService) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
  }
 
 ngOnInit(): void {
   this.getCargarCombos();
   this.inicializarFormularioFiltro();
   this.inicializarFormulario();
   this.inicializarFormularioAgregar();
 }

 inicializarFormularioFiltro(){ 
    this.formParamsFiltro= new FormGroup({
      idEmpresa : new FormControl('0'),
      idEstado : new FormControl('1')
     }) 
 }

 inicializarFormulario(){
    this.formParams= new FormGroup({
      id_precioMaterial: new FormControl('0'),
      id_empresa: new FormControl('0'),
      id_servicio: new FormControl('0'),
      id_tipoTrabajo: new FormControl('0'),
      id_TipoMaterial: new FormControl('0'),
      precio: new FormControl(''),
      tipo: new FormControl(''),
      id_Baremo: new FormControl('0'),
      estado: new FormControl('1'),
      usuario_creacion: new FormControl('0'),
    }) 
 } 

 inicializarFormularioAgregar(){ 
  this.formParamsAgregar= new FormGroup({
      codigo : new FormControl(''),
      descripcion : new FormControl(''),
      precio : new FormControl('0')
   }) 
 } 

 getCargarCombos(){ 
    this.spinner.show();
    combineLatest([this.listaPreciosService.get_empresas(),  this.listaPreciosService.get_tiposMaterial_precioEmpresa(), this.listaPreciosService.get_baremos()  ])
      .subscribe( ([ _empresas,  _tiposMaterial, _baremos ])=>{
      this.empresas = _empresas;
      this.tiposMaterial = _tiposMaterial; 
      this.baremos = _baremos; 
      this.spinner.hide(); 
    })
 }

 mostrarInformacion(){
      this.spinner.show();
      this.listaPreciosService.get_mostrarPrecio_empresa(this.formParamsFiltro.value.idEmpresa, this.formParamsFiltro.value.idEstado)
          .subscribe((res:RespuestaServer)=>{            
              this.spinner.hide();
              if (res.ok==true) {        
                  this.preciosEmpresas = res.data; 
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
      $('#formContenido').removeClass('disabledForm');
    },0);
    this.inicializarFormulario();  
    
    setTimeout(()=>{ // 
      this.mostrarInformacion_detalle();     
   },100);
 
 }

 nuevoDet(){
  this.flag_modoEdicion = false;
  this.inicializarFormulario();  
  setTimeout(()=>{ // 
    $('#formContenido').removeClass('disabledForm');
    this.mostrarInformacion_detalle();
 },100);

}

 get_verificarRuc(rucEmpresa:string){ 
    const listProveedor = this.preciosEmpresas.find(u=> u.ruc_Empresa.toUpperCase() === rucEmpresa.toUpperCase());
    return listProveedor;
 }

 async saveUpdate(){
   
  if (this.formParams.value.id_TipoMaterial == '' || this.formParams.value.id_TipoMaterial == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Tipo de Material');
    return 
  } 
  if (this.formParams.value.id_Baremo == '' || this.formParams.value.id_Baremo == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Baremo');
    return 
  }  

  if (this.formParams.value.precio == '' || this.formParams.value.precio == 0) {
    this.alertasService.Swal_alert('error','Por favor ingrese el precio');
    return 
  }
 
   this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });

    Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
    Swal.showLoading();
    
    if ( this.flag_modoEdicion==false) { //// nuevo  

      const { descripcion } = this.baremos.find(b=> b.id == this.formParams.value.id_Baremo );
      this.formParams.patchValue({ "tipo" : descripcion });

      const verifyRegister = await this.listaPreciosService.get_verificar_empresa_tipoMaterial_baremo(this.formParams.value.id_empresa , this.formParams.value.id_TipoMaterial , this.formParams.value.id_Baremo  );     
      console.log(verifyRegister)
      
      if (verifyRegister ) {
        Swal.close();
        this.alertasService.Swal_alert('error','El precio para esta Empresa, Material y Baremo ya se agrego, verifique..');
        return;
      } 
    }
  
    this.listaPreciosService.set_savePrecioUpdate_empresa(this.formParams.value).subscribe((res:RespuestaServer)=>{
      Swal.close();    
      if (res.ok ==true) {     

        this.alertasService.Swal_Success( this.flag_modoEdicion==false? 'Se agrego correctamente..' : 'Se actualizo correctamente' );
        setTimeout(()=>{ // 
            this.mostrarInformacion_detalle();
            this.mostrarInformacion();
        },100);

      }else{
        this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        alert(JSON.stringify(res.data));
      }
    })   

 } 

 editar({  id_precioMaterial, id_empresa,  id_servicio, id_tipoTrabajo, id_TipoMaterial,  precio, id_Baremo, tipo, estado }){

   this.flag_modoEdicion=true;
   setTimeout(()=>{ // 
       $('#modal_mantenimiento').modal('show');          
       $('#formContenido').addClass('disabledForm');
       
       this.formParams.patchValue({ "id_precioMaterial" : id_precioMaterial, "id_empresa" :  id_empresa  , "id_servicio" : id_servicio ,"id_tipoTrabajo" : id_tipoTrabajo,  "id_TipoMaterial" : id_TipoMaterial, 
         "precio" : precio, "usuario_creacion" : this.idUserGlobal,"id_Baremo" : id_Baremo, "tipo" : tipo,  "estado" : estado  }
       );    
      this.mostrarInformacion_detalle();
  },0);
 
 } 

 editarDet({  id_precioMaterial, id_empresa,  id_servicio, id_tipoTrabajo, id_TipoMaterial,  precio, id_Baremo, tipo, estado }){
  this.flag_modoEdicion=true;
  setTimeout(()=>{ //  
    $('#formContenido').addClass('disabledForm');
      this.formParams.patchValue({ "id_precioMaterial" : id_precioMaterial, "id_empresa" :  id_empresa  , "id_servicio" : id_servicio ,"id_tipoTrabajo" : id_tipoTrabajo,  "id_TipoMaterial" : id_TipoMaterial, 
        "precio" : precio, "usuario_creacion" : this.idUserGlobal,"id_Baremo" : id_Baremo, "tipo" : tipo,  "estado" : estado  }
      );    
  },0);
} 

 anular(objBD:any, opcion:string){

   if (objBD.estado ===0 || objBD.estado =='0') {      
     return;      
   }

   this.alertasService.Swal_Question('Sistemas', 'Esta seguro de anular ?')
   .then((result)=>{
     if(result.value){

       Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
       Swal.showLoading();
       this.listaPreciosService.set_anularPrecio_Empresa( objBD.id_precioMaterial ).subscribe((res:RespuestaServer)=>{
         Swal.close();        
         if (res.ok ==true) {
           
          if (opcion == 'Cabecera' ) {
            for (const user of this.preciosEmpresas) {
              if (user.id_precioMaterial == objBD.id_precioMaterial ) {               
                  user.estado = 0;
                  user.descripcionEstado =  "ANULADO" ;
                  break;
              }
            }
          }
          if (opcion == 'Detalle' ) {
            for (const user of this.preciosEmpresas_Detalle) {
              if (user.id_precioMaterial == objBD.id_precioMaterial ) {               
                  user.estado = 0;
                  user.descripcionEstado =  "ANULADO" ;
                  break;
              }
            }

            for (const user of this.preciosEmpresas) {
              if (user.id_precioMaterial == objBD.id_precioMaterial ) {               
                  user.estado = 0;
                  user.descripcionEstado =  "ANULADO" ;
                  break;
              }
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

 onChangeBaremo(obj:any){
 
    const idBaremo = obj.target.value;
    if (idBaremo == '0' || idBaremo == 0 ) {
      this.formParams.patchValue({ "precio" : '0' } );
    }else{
      const listBaremo = this.baremos.find(b => b.id == idBaremo);
      this.formParams.patchValue({ "precio" : listBaremo['precio'] } );
    }
 }

   mostrarInformacion_detalle(){
    this.spinner.show();
    this.listaPreciosService.get_mostrarPrecio_empresaDetalle( this.formParams.value.id_empresa )
        .subscribe((res:RespuestaServer)=>{            
            this.spinner.hide();
            if (res.ok==true) {        
                this.preciosEmpresas_Detalle = res.data; 
            }else{
              this.alertasService.Swal_alert('error', JSON.stringify(res.data));
              alert(JSON.stringify(res.data));
            }
    })
  }

    
 cerrarModal_agregar(){
  setTimeout(()=>{ // 
    $('#modal_agregar').modal('hide');  
  },0); 
}
  
  agregarCombo(tipoMantenimiento : number){

    //--tipoMantenimiento = 1 tipo material, 2 baremos
    this.tipoMantenimiento_Modal = tipoMantenimiento;
    this.inicializarFormularioAgregar();

    setTimeout(()=>{ // 
      $('#modal_agregar').modal('show');
    },0);

  }


  
 async save_agregarMantenimiento(){   
 
    if (this.formParamsAgregar.value.codigo == '' || this.formParamsAgregar.value.codigo == null) {
      this.alertasService.Swal_alert('error','Por favor agregue un codigo');
      return 
    }  
    if (this.formParamsAgregar.value.descripcion == '' || this.formParamsAgregar.value.descripcion == null) {
      this.alertasService.Swal_alert('error','Por favor ingrese la descripcion');
      return 
    }
  
    if ( this.tipoMantenimiento_Modal == 2) {
      if (this.formParamsAgregar.value.precio == '' || this.formParamsAgregar.value.precio == 0) {
        this.alertasService.Swal_alert('error','Por favor ingrese el precio');
        return 
      } 
      if ( this.formParamsAgregar.value.precio < 0) {
        this.alertasService.Swal_alert('error','Por favor ingrese el precio con un valor positivo');
        return 
      } 
    } 

    Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
    Swal.showLoading();  
    this.listaPreciosService.set_save_agregarMantenimiento({...this.formParamsAgregar.value, tipoMantenimiento : this.tipoMantenimiento_Modal , usuario_creacion : this.idUserGlobal })
    .subscribe((res:RespuestaServer)=>{
      Swal.close();    
      if (res.ok ==true) {     

        this.alertasService.Swal_Success('Se agrego correctamente..');
        setTimeout(()=>{ // 
            this.cerrarModal_agregar();
            if ( this.tipoMantenimiento_Modal == 1) {
              this.spinner.show();

              setTimeout(()=>{ // 
                this.spinner.show();
                this.listaPreciosService.get_tiposMaterial_precioEmpresa()
                    .subscribe(_tiposMaterial=>{
                      this.spinner.hide(); 
                      this.tiposMaterial = _tiposMaterial;   
                })
              },2000);

            }else{
              setTimeout(()=>{ // 
                this.spinner.show();
                this.listaPreciosService.get_baremos()
                    .subscribe(_tiposMaterial=>{
                      this.spinner.hide(); 
                      this.baremos = _tiposMaterial;   
                })
              },2000);
            }
        },100);

      }else{
        this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        alert(JSON.stringify(res.data));
      }
    })   

 } 


  

}
