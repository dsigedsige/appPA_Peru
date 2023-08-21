
import { Component, OnInit, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertasService } from '../../../services/alertas/alertas.service';
import { RespuestaServer } from '../../../models/respuestaServer.models';
import { FuncionesglobalesService } from '../../../services/funciones/funcionesglobales.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from '../../../services/login/login.service';
import { from, combineLatest } from 'rxjs';
import Swal from 'sweetalert2';

import { ListaPreciosService } from '../../../services/Mantenimientos/lista-precios.service';
import { OrdenTrabajoService } from '../../../services/Procesos/orden-trabajo.service';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { AprobacionOTService } from '../../../services/Procesos/aprobacion-ot.service';
import { InputFileI } from 'src/app/models/inputFile.models';
import { UploadService } from '../../../services/Upload/upload.service';



declare var $:any;
@Component({
  selector: 'app-aprobacion-ot',
  templateUrl: './aprobacion-ot.component.html',
  styleUrls: ['./aprobacion-ot.component.css']
})

export class AprobacionOTComponent implements OnInit,AfterViewInit {

  formParamsFiltro : FormGroup;
  formParamsDatosG : FormGroup;
  formParamsDet : FormGroup;
  formParamsFile: FormGroup;
  formParamsObservacion : FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false

  servicios :any[]=[];
  distritos :any[]=[];
  proveedor :any[]=[];
  estados :any[]=[];
  tipoOrdenTrabajo :any[]=[];
  ordenTrabajoCab :any[]=[];

  medidasDetalle :any[]=[];
  desmonteDetalle :any[]=[];
  fotosDetalle :any[]=[];

  filtrarMantenimiento = "";
  opcionModal = "";
  tituloModal = "";
  objFotoAgrandar = {};
  urlFotoAgrandar = '';
  tipoTrabajo_OTOrigenGlobal = '';
  files:InputFileI[] = [];

  checkeadoAll=false;
  datepiekerConfig:Partial<BsDatepickerConfig>;

  detalleOT:any;
   /// configuracion google maps
    @ViewChild('mapa', {static: false}) mapaElement: ElementRef;
    map : google.maps.Map;
    marker :google.maps.Marker;


  //-TAB control
  tabControlDetalle: string[] = ['DATOS GENERALES','MEDIDAS','DESMONTE','COMENTARIOS'];
  selectedTabControlDetalle :any;

  flagMedidas =  true;
  id_OTGlobal = 0;
  id_tipoOTGlobal = 0;
  id_estadoOTGlobal = 0;

  nroObraParteDiario_Global = '';
  fechaHora_Global = '';

  totalGlobal =0;
  totalGlobal14 =0;
  totalGlobal15 =0;

  idPerfil_global =0;

  id_OTDet_Global  = 0;
  opcionEditar_Global = '';
  labelAdjuntar = 'Adjuntar Archivo';
  nombreArchivoAdjuntar = '';
  urlArchivoAdjuntar = '';

  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService, private listaPreciosService : ListaPreciosService,
             private ordenTrabajoService : OrdenTrabajoService, private aprobacionOTService : AprobacionOTService , private funcionGlobalServices : FuncionesglobalesService, private uploadService : UploadService ) {
      this.idUserGlobal = this.loginService.get_idUsuario();
      this.idPerfil_global = this.loginService.get_idPerfil();
  }

 ngOnInit(): void {
  this.selectedTabControlDetalle = this.tabControlDetalle[0];
  this.getCargarCombos();
  this.inicializarFormularioFiltro();
  this.inicializarFormularioDatosG();
  this.inicializarFormularioDet();
  this.inicializarFormulario_file();
  this.inicializarFormulario_observacion();
 }

 ngAfterViewInit() {
  this.InicializarMapa()
}

 inicializarFormularioFiltro(){

  let fechaIni = new Date();
  let fechaFin = new Date();

  fechaIni.setDate(fechaIni.getDate() - 2);
  fechaFin.setDate(fechaFin.getDate() - 1);

    this.formParamsFiltro= new FormGroup({
      idServicio : new FormControl('0'),
      idTipoOT : new FormControl('0'),
      idDistrito : new FormControl('0'),
      idProveedor : new FormControl('0'),
      idEstado : new FormControl('6'),
      fecha_ini : new FormControl(fechaIni),
      fecha_fin : new FormControl(fechaFin)
     })
 }

 inicializarFormularioDatosG(){
  this.formParamsDatosG= new FormGroup({
    direccion : new FormControl(''),
    idDistrito : new FormControl('0'),
    referencia : new FormControl(''),
    descripcionTrabajo : new FormControl(''),
    idEstado : new FormControl('0'),
    observacion  : new FormControl(''),
   })
}

  inicializarFormularioDet(){
    this.formParamsDet= new FormGroup({
        largo  : new FormControl('0'),
        ancho  : new FormControl('0'),
        altura  : new FormControl('0'),
        total  : new FormControl('0')
     });
   }

   inicializarFormulario_file(){
    this.formParamsFile = new FormGroup({
      file : new FormControl('')
     })
  }


  inicializarFormulario_observacion(){
    this.formParamsObservacion = new FormGroup({
      observarcionGestor : new FormControl(''),
      estatus : new FormControl('')
     })
  }

 getCargarCombos(){
    this.spinner.show();
    combineLatest([this.ordenTrabajoService.get_servicio(this.idUserGlobal), this.listaPreciosService.get_tipoOrdenTrabajo(), this.ordenTrabajoService.get_Distritos(), this.ordenTrabajoService.get_Proveedor(), this.aprobacionOTService.get_estados() ]).subscribe( ([ _servicios, _tipoOrdenTrabajo, _distritos, _proveedor,_estados ])=>{
        this.servicios = _servicios;
        this.tipoOrdenTrabajo = _tipoOrdenTrabajo;
        this.distritos = _distritos;
        this.proveedor = _proveedor;
        this.estados = _estados;
      this.spinner.hide();
    },(error)=>{
      this.spinner.hide();
      alert(error);
    })

 }

 InicializarMapa() {
  const latLng = new google.maps.LatLng(-12.046374, -77.0427934 );
  const  mapaOption : google.maps.MapOptions = {
    center : latLng,
    zoom : 13,
    mapTypeControl: true,
  }
  this.map = new google.maps.Map(this.mapaElement.nativeElement, mapaOption);
  this.marker = new google.maps.Marker({
    position: latLng
  });
  this.marker.setMap(this.map);
 };

 mostrarInformacion(){
      if (this.formParamsFiltro.value.idServicio == '' || this.formParamsFiltro.value.idServicio == 0) {
        this.alertasService.Swal_alert('error','Por favor seleccione el servicio');
        return
      }

      if (this.formParamsFiltro.value.idTipoOT == '' || this.formParamsFiltro.value.idTipoOT == 0) {
        this.alertasService.Swal_alert('error','Por favor seleccione el Tipo de Orden Trabajo');
        return
      }

      // if (this.formParamsFiltro.value.idDistrito == '' || this.formParamsFiltro.value.idDistrito == 0) {
      //   this.alertasService.Swal_alert('error','Por favor seleccione un Distrito');
      //   return
      // }

      // if (this.formParamsFiltro.value.idProveedor == '' || this.formParamsFiltro.value.idProveedor == 0) {
      //   this.alertasService.Swal_alert('error','Por favor seleccione un Proveedor');
      //   return
      // }

      if (this.formParamsFiltro.value.fecha_ini == '' || this.formParamsFiltro.value.fecha_ini == null) {
        this.alertasService.Swal_alert('error','Por favor seleccione la fecha inicial');
        return
      }
      if (this.formParamsFiltro.value.fecha_fin == '' || this.formParamsFiltro.value.fecha_fin == null) {
        this.alertasService.Swal_alert('error','Por favor seleccione la fecha final');
        return
      }


      if (this.formParamsFiltro.value.idEstado == '' || this.formParamsFiltro.value.idEstado == 0) {
        this.alertasService.Swal_alert('error','Por favor seleccione un Estado');
        return
      }

      const fechaIni = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_ini);
      const fechaFin = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_fin);

      this.spinner.show();
      this.aprobacionOTService.get_mostrarAprobarOTCab_general(this.formParamsFiltro.value, fechaIni, fechaFin, this.idUserGlobal)
          .subscribe((res:RespuestaServer)=>{
              this.spinner.hide();
              if (res.ok==true) {
                this.checkeadoAll =false ;
                  this.ordenTrabajoCab = res.data;
                  console.log( this.ordenTrabajoCab)
              }else{
                this.alertasService.Swal_alert('error', JSON.stringify(res.data));
                alert(JSON.stringify(res.data));
              }
      })
 }

 marcarTodos(){
  if (this.ordenTrabajoCab.length<=0) {
    return;
  }
  for (const obj of this.ordenTrabajoCab) {
    if (this.checkeadoAll) {
      obj.checkeado = false;
    }else{
      obj.checkeado = true;
    }
  }
 }

 validacionCheckMarcado(){
  let CheckMarcado = false;
  // CheckMarcado = this.verificarCheckMarcado();
  CheckMarcado = this.funcionGlobalServices.verificarCheck_marcado(this.ordenTrabajoCab);

  if (CheckMarcado ==false) {
    this.alertasService.Swal_alert('error','Por favor debe marcar un elemento de la Tabla');
    return false;
  }else{
    return true;
  }
}

abrirModalMapa(latitud:string, longitud :string){
  if (latitud.length ==0) {
    return;
  }

  const lat = Number(latitud);
  const lng = Number(longitud);
  const latLng = new google.maps.LatLng(lat, lng );

  $('#modalMapa').modal('show');

  //// limpiando el anterior marker
  this.marker.setMap(null);

  /// asignando el nuevo markeral mapa
  this.marker = new google.maps.Marker({
    position: latLng
  });
  this.marker.setMap(this.map);

  $("#location-map").css("width", "100%");
  $("#id_mapa").css("width", "100%");

  google.maps.event.trigger(this.map, "resize");
  this.map.setCenter(latLng);
}

cerrarModal_OT(){
  setTimeout(()=>{ //
    $('#modal_OT').modal('hide');
  },0);
}

abrirModal_OT( {id_OT,nroObra,fechaHora,direccion, id_Distrito, referencia, descripcion_OT, id_tipoTrabajo,id_estado, tipoTrabajo_OTOrigen,observacion , observacionGestor_OT, estatus_OT }){

  // //----- Datos Generales  -----
  this.id_OTGlobal = id_OT;
  this.id_tipoOTGlobal = id_tipoTrabajo;
  this.nroObraParteDiario_Global = nroObra;
  this.fechaHora_Global = fechaHora;
  this.id_estadoOTGlobal = id_estado;
  this.tipoTrabajo_OTOrigenGlobal = tipoTrabajo_OTOrigen;

  this.totalGlobal =0;
  this.totalGlobal14 =0;
  this.totalGlobal15 =0;
  this.tituloModal = 'Tipo Trabajo No-Definido';



  if (id_tipoTrabajo == 3 ||  id_tipoTrabajo == 4 ) {  // rotura

    this.tituloModal = 'REGISTRO DE ROTURA O REPARACION'
    this.flagMedidas =true;
    this.get_medidasOT();
    this.get_desmonteOT();

  }
  else if (id_tipoTrabajo == 5 ) {  // desmonte

    this.tituloModal = 'REGISTRO DE DESMONTE'
    this.flagMedidas =false;
    this.medidasDetalle = [];
    this.get_desmonteOT();

  }else{
    this.flagMedidas =false;
    this.medidasDetalle = [];
    this.desmonteDetalle = [];
  }

  this.formParamsDatosG.patchValue({"direccion": direccion , "idDistrito": id_Distrito, "referencia": referencia, "descripcionTrabajo": descripcion_OT , "idEstado": id_estado , "observacion" : observacion });
  this.formParamsObservacion.patchValue({"observarcionGestor": observacionGestor_OT , "estatus": estatus_OT });

  this.selectedTabControlDetalle = this.tabControlDetalle[0];

  setTimeout(()=>{ //
    $('#modal_OT').modal('show');
  },0);
}

  Aprobar_OT(){

    if ( this.id_OTGlobal == 0 || this.id_OTGlobal == null)  {
      this.alertasService.Swal_alert('error', 'No se cargo la informacion del ID de la Orden Trabajo, actualize su pagina.');
      return
    }

    if ( this.formParamsDatosG.value.idEstado == 0 || this.formParamsDatosG.value.idEstado == null)  {
      this.alertasService.Swal_alert('error', 'Por favor seleccione un Estado.');
      return
    }

    if ( this.formParamsDatosG.value.idEstado == 10 || this.formParamsDatosG.value.idEstado == '10')  {
      if ( this.formParamsDatosG.value.observacion == null || this.formParamsDatosG.value.observacion == '')  {
        this.alertasService.Swal_alert('error', 'Por favor ingrese  la observacion.');
        return
      }
    }

    const observacion = (this.formParamsDatosG.value.idEstado ==  10) ?  this.formParamsDatosG.value.observacion : '';

    Swal.fire({
      icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'
    })
    Swal.showLoading();
    this.aprobacionOTService.set_aprobarOT( this.id_OTGlobal, this.formParamsDatosG.value.idEstado  , this.idUserGlobal,  observacion ).subscribe((res:RespuestaServer)=>{
      Swal.close();
      if (res.ok) {
          this.alertasService.Swal_Success('OT aprobada correctamente');
          this.mostrarInformacion();
      }else{
        this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        alert(JSON.stringify(res.data));
      }
    })

  }

  get_medidasOT(){
    this.aprobacionOTService.get_medidasOT(this.id_OTGlobal, this.id_tipoOTGlobal, this.idUserGlobal).subscribe((res:RespuestaServer)=>{
     if (res.ok) {
       this.medidasDetalle = res.data;
       this.calculoTotalMedidas();
     }else{
       this.alertasService.Swal_alert('error', JSON.stringify(res.data));
       alert(JSON.stringify(res.data));
     }
    })
  }

  calculoTotalMedidas(){
    let importeTotal =0;
    for (const iterator of  this.medidasDetalle) {
     importeTotal += iterator.total;
    }
    this.totalGlobal = importeTotal;

    // if (this.id_tipoOTGlobal == 3 ) {
    //   this.totalGlobal = (importeTotal * 10 );
    // }else{
    //   this.totalGlobal = importeTotal;
    // }
  }

  get_desmonteOT(){
    this.aprobacionOTService.get_mesmonteOT(this.id_OTGlobal, this.id_tipoOTGlobal, this.idUserGlobal).subscribe((res:RespuestaServer)=>{
     if (res.ok) {
       this.desmonteDetalle = res.data;
       this.calculoTotalDesmonte();

     }else{
       this.alertasService.Swal_alert('error', JSON.stringify(res.data));
       alert(JSON.stringify(res.data));
     }
    })
  }

  calculoTotalDesmonte(){
    let importeTotal14 =0;
    let importeTotal15 =0;

    for (const iterator of  this.desmonteDetalle) {

       if (iterator.id_TipoMaterial == '14' ) {
         console.log('entroo 14')
         importeTotal14 += iterator.total;
       }
       if (iterator.id_TipoMaterial == '15' ) {
         console.log('entroo 15')
         importeTotal15 += iterator.total;
       }

    }

    this.totalGlobal14 = importeTotal14;
    this.totalGlobal15 = importeTotal15;
  }

  cerrarModal_visor(){
    $('#modal_visorFotos').modal('hide');
  }


  abrirModal_visorFotos(objData:any){

    this.detalleOT = objData;

    setTimeout(()=>{ //
      $('#modal_visorFotos').modal('show');
    },0);

    Swal.fire({
      icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Obteniendo Fotos, Espere por favor'
    })
    Swal.showLoading();
    this.aprobacionOTService.get_fotosOT(objData.id_OTDet, this.id_tipoOTGlobal, this.idUserGlobal).subscribe((res:RespuestaServer)=>{
      Swal.close();
      if (res.ok) {
        this.fotosDetalle = res.data;
      }else{
        this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        alert(JSON.stringify(res.data));
      }
     })

  }


  anulandoFoto(objFoto:any){

  this.alertasService.Swal_Question('Sistemas', 'Esta seguro de anular ?')
  .then((result)=>{
    if(result.value){

      Swal.fire({
        icon: 'info',
        allowOutsideClick: false,
        allowEscapeKey: false,
        text: 'Anulando la foto, espere por favor'
      })
      Swal.showLoading();
      this.aprobacionOTService.set_anular_Fotos(objFoto.id_OTDet_Foto).subscribe((res:RespuestaServer)=>{
        Swal.close();

        if (res.ok) {
          var index = this.fotosDetalle.indexOf( objFoto );
           this.fotosDetalle.splice( index, 1 );

           if (this.fotosDetalle.length ==0) {
             this.cerrarModal_visor();
           }

        }else{
          this.alertasService.Swal_alert('error', JSON.stringify(res.data));
          alert(JSON.stringify(res.data));
        }
      })

    }
  })
 }


  descargarGrilla(){
    // if (this.formParamsFiltro.value.idServicio == '' || this.formParamsFiltro.value.idServicio == 0) {
    //   this.alertasService.Swal_alert('error','Por favor seleccione el servicio');
    //   return
    // }

    // if (this.formParamsFiltro.value.idTipoOT == '' || this.formParamsFiltro.value.idTipoOT == 0) {
    //   this.alertasService.Swal_alert('error','Por favor seleccione el Tipo de Orden Trabajo');
    //   return
    // }

    // if (this.formParamsFiltro.value.idDistrito == '' || this.formParamsFiltro.value.idDistrito == 0) {
    //   this.alertasService.Swal_alert('error','Por favor seleccione un Distrito');
    //   return
    // }

    // if (this.formParamsFiltro.value.idProveedor == '' || this.formParamsFiltro.value.idProveedor == 0) {
    //   this.alertasService.Swal_alert('error','Por favor seleccione un Proveedor');
    //   return
    // }

    // if (this.formParamsFiltro.value.idEstado == '' || this.formParamsFiltro.value.idEstado == 0) {
    //   this.alertasService.Swal_alert('error','Por favor seleccione un Estado');
    //   return
    // }

    if (this.formParamsFiltro.value.idServicio == '' || this.formParamsFiltro.value.idServicio == 0) {
      this.alertasService.Swal_alert('error','Por favor seleccione el servicio');
      return
    }

    if (this.formParamsFiltro.value.idTipoOT == '' || this.formParamsFiltro.value.idTipoOT == 0) {
      this.alertasService.Swal_alert('error','Por favor seleccione el Tipo de Orden Trabajo');
      return
    }

    // if (this.formParamsFiltro.value.idDistrito == '' || this.formParamsFiltro.value.idDistrito == 0) {
    //   this.alertasService.Swal_alert('error','Por favor seleccione un Distrito');
    //   return
    // }

    // if (this.formParamsFiltro.value.idProveedor == '' || this.formParamsFiltro.value.idProveedor == 0) {
    //   this.alertasService.Swal_alert('error','Por favor seleccione un Proveedor');
    //   return
    // }

    if (this.formParamsFiltro.value.fecha_ini == '' || this.formParamsFiltro.value.fecha_ini == null) {
      this.alertasService.Swal_alert('error','Por favor seleccione la fecha inicial');
      return
    }
    if (this.formParamsFiltro.value.fecha_fin == '' || this.formParamsFiltro.value.fecha_fin == null) {
      this.alertasService.Swal_alert('error','Por favor seleccione la fecha final');
      return
    }


    if (this.formParamsFiltro.value.idEstado == '' || this.formParamsFiltro.value.idEstado == 0) {
      this.alertasService.Swal_alert('error','Por favor seleccione un Estado');
      return
    }


    const fechaIni = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_ini);
    const fechaFin = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_fin);


    this.spinner.show();
    this.aprobacionOTService.get_descargarAprobarOTCab_general(this.formParamsFiltro.value, fechaIni, fechaFin, this.idUserGlobal)
        .subscribe((res:RespuestaServer)=>{
            this.spinner.hide();
            console.log(res.data);
            if (res.ok==true) {
              window.open(String(res.data),'_blank');
            }else{
              this.alertasService.Swal_alert('error', JSON.stringify(res.data));
              alert(JSON.stringify(res.data));
            }
    })
  }


  cerrarModal_agrandarFoto(){
    $('#modal_agrandarFoto').modal('hide');
  }

  open_modalAgrandarFoto(objFoto:any){

    this.urlFotoAgrandar = objFoto.urlFoto;
    this.objFotoAgrandar = objFoto;

    setTimeout(()=>{ //
      $('#modal_agrandarFoto').modal('show');
    },0);

  }

  aprobarMasivo(){

    if (this.formParamsFiltro.value.idServicio == '' || this.formParamsFiltro.value.idServicio == 0) {
      this.alertasService.Swal_alert('error','Por favor seleccione el servicio');
      return
    }

    if (this.validacionCheckMarcado()==false){
      return;
    }
    const codigosIdOT = this.funcionGlobalServices.obtenerCheck_IdPrincipal(this.ordenTrabajoCab,'id_OT');

    this.alertasService.Swal_Question('Sistemas', 'Esta seguro de aprobar los registros marcados. ?')
    .then((result)=>{
      if(result.value){

        Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
        Swal.showLoading();
        this.ordenTrabajoService.set_aprobarOT_masivo(codigosIdOT.join() , this.idUserGlobal, this.formParamsFiltro.value.idServicio ).subscribe((res:RespuestaServer)=>{
          Swal.close();
          if (res.ok ==true) {
            //-----listando la informacion
            this.mostrarInformacion();
            this.alertasService.Swal_Success('Proceso de Aprobación realizado correctamente..');
          }else{
            this.alertasService.Swal_alert('error', JSON.stringify(res.data));
            alert(JSON.stringify(res.data));
          }
        })

      }
    })


  }


 anular(objBD:any){

  if (objBD.id_estado === 13  || objBD.id_estado =='13') {
    return;
  }

  this.alertasService.Swal_Question('Sistemas', 'Esta seguro de anular ?')
  .then((result)=>{
    if(result.value){

      Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
      Swal.showLoading();
      this.ordenTrabajoService.set_anular_aprobacionOT(objBD.id_OT , this.idUserGlobal  ).subscribe((res:RespuestaServer)=>{
        Swal.close();
        if (res.ok ==true) {

          this.mostrarInformacion();
          this.alertasService.Swal_Success('Se anulo correctamente..')

        }else{
          this.alertasService.Swal_alert('error', JSON.stringify(res.data));
          alert(JSON.stringify(res.data));
        }
      })

    }
  })
 }


 modificarNroOrden(){

  // validando solo por perfil Coordinador de Servicios
  if (this.idPerfil_global != 2) {
    return
  }

  if (this.nroObraParteDiario_Global == '' || this.nroObraParteDiario_Global == null ) {
    this.alertasService.Swal_alert('error','Por favor ingrese el Nro Orden');
    return
  }

   this.alertasService.Swal_Question('Sistemas', 'Esta seguro de modificar ?')
   .then((result)=>{
     if(result.value){

       Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
       Swal.showLoading();
       this.ordenTrabajoService.set_modificar_nroObra( this.id_OTGlobal, this.nroObraParteDiario_Global, this.idUserGlobal  ).subscribe((res:RespuestaServer)=>{
         Swal.close();
         if (res.ok ==true) {

           for (const user of this.ordenTrabajoCab) {
             if (user.id_OT == this.id_OTGlobal ) {
                 user.nroObra = this.nroObraParteDiario_Global;
                 break;
             }
           }
           this.alertasService.Swal_Success('Modificacion realizada correctamente..')

         }else{
           this.alertasService.Swal_alert('error', JSON.stringify(res.data));
           alert(JSON.stringify(res.data));
         }
       })

     }
   })


 }

  eliminarMedidas(item:any, opcion : string){
    this.opcionEditar_Global = opcion;


    this.alertasService.Swal_Question('Sistemas', 'Esta seguro de eliminar ?')
    .then((result)=>{
      if(result.value){

         Swal.fire({
          icon: 'info', allowOutsideClick: false, allowEscapeKey: false,
          text: 'Espere por favor'
        })
        Swal.showLoading();
        this.aprobacionOTService.get_eliminarMedidas(item.id_OTDet, this.idUserGlobal ).subscribe((res:RespuestaServer)=>{
          Swal.close();
          if (res.ok) {
              var index = this.medidasDetalle.indexOf( item );
              this.medidasDetalle.splice( index, 1 );

              this.calculoTotalMedidas();

          }else{
            this.alertasService.Swal_alert('error', JSON.stringify(res.data));
            alert(JSON.stringify(res.data));
          }
        })

      }
    })




  }

  eliminarDesmonte(item:any, opcion :string){
    this.opcionEditar_Global = opcion;


    this.alertasService.Swal_Question('Sistemas', 'Esta seguro de anular ?')
    .then((result)=>{
      if(result.value){

        Swal.fire({
          icon: 'info', allowOutsideClick: false, allowEscapeKey: false,
          text: 'Espere por favor'
        })
        Swal.showLoading();
        this.aprobacionOTService.get_eliminarDesmonte(item.id_OTDet , this.idUserGlobal ).subscribe((res:RespuestaServer)=>{
          Swal.close();
          if (res.ok) {
              var index = this.desmonteDetalle.indexOf( item );
              this.desmonteDetalle.splice( index, 1 );
              this.calculoTotalDesmonte();

          }else{
            this.alertasService.Swal_alert('error', JSON.stringify(res.data));
            alert(JSON.stringify(res.data));
          }
        })

      }
    })



  }

  cerrarModal_editar(){
    $('#modal_edicion').modal('hide');
  }

  abrirModal_editar(objData:any, opcion){

    this.opcionEditar_Global = opcion;
    this.id_OTDet_Global = objData.id_OTDet;

    this.inicializarFormularioDet();
    setTimeout(()=>{ //
      $('#modal_edicion').modal('show');
    },0);

    console.log(objData);
    this.formParamsDet.patchValue({"largo": objData.largo , "ancho": objData.ancho , "altura": objData.espesor   , "total": objData.total    });
  }

  onBlurMethod(obj){
      this.calcularTotales();
  }

  calcularTotales(){
    let largo = this.formParamsDet.value.largo == null ? 0 :  this.formParamsDet.value.largo ;
    let ancho = this.formParamsDet.value.ancho == null ? 0 :  this.formParamsDet.value.ancho ;
    let altura = this.formParamsDet.value.altura == null ? 0 :  this.formParamsDet.value.altura ;

    const total = (largo * ancho * altura );

    this.formParamsDet.patchValue({"total": Number(total.toFixed(2))});
   }

   set_actualizandoDetalleOt(){

    if (this.formParamsDet.value.largo == '' || this.formParamsDet.value.largo == null) {
      this.alertasService.Swal_alert('error','Por favor ingrese el largo');
      return
    }
    if (this.formParamsDet.value.ancho == '' || this.formParamsDet.value.ancho == null) {
      this.alertasService.Swal_alert('error','Por favor ingrese el ancho');
      return
    }
    if (this.formParamsDet.value.altura == '' || this.formParamsDet.value.altura == null) {
      this.alertasService.Swal_alert('error','Por favor ingrese la altura');
      return
    }


    let largo = this.formParamsDet.value.largo == null ? 0 :  this.formParamsDet.value.largo ;
    let ancho = this.formParamsDet.value.ancho == null ? 0 :  this.formParamsDet.value.ancho ;
    let altura = this.formParamsDet.value.altura == null ? 0 :  this.formParamsDet.value.altura ;
    let total = this.formParamsDet.value.total == null ? 0 :  this.formParamsDet.value.total ;


    Swal.fire({
      icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Actualizando, Espere por favor'
    })
    Swal.showLoading();
    this.aprobacionOTService.set_actualizandoDetalleOT(this.id_OTDet_Global, largo ,ancho, altura, String(total), this.idUserGlobal).subscribe((res:RespuestaServer)=>{
      Swal.close();
     if (res.ok) {

      this.alertasService.Swal_Success('Actualizacion realizada correctamente');
       if ( this.opcionEditar_Global  ==  'M' ) { ///--- medidaas

        for (const iterator of  this.medidasDetalle) {
            if (iterator.id_OTDet == this.id_OTDet_Global ) {
              iterator.largo = largo;
              iterator.ancho = ancho;
              iterator.espesor = altura;
              iterator.total = total;
            }
         }
         this.calculoTotalMedidas();

       }
       else{
        for (const iterator of  this.desmonteDetalle) {   //// --desmonte
            if (iterator.id_OTDet == this.id_OTDet_Global ) {
              iterator.largo = largo;
              iterator.ancho = ancho;
              iterator.espesor = altura;
              iterator.total = total;
            }
         }
         this.calculoTotalDesmonte();
       }

       this.mostrarInformacion();
       this.cerrarModal_editar();

     }else{
       this.alertasService.Swal_alert('error', JSON.stringify(res.data));
       alert(JSON.stringify(res.data));
     }
    })
  }

  cerrarModal_adjuntar(){
    $('#modal_adjuntar').modal('hide');
  }

  abrirModal_adjuntar(objData:any){

    this.id_OTGlobal = objData.id_OT;
    this.inicializarFormulario_file();



    setTimeout(()=>{ //

      const nombreFileBD = objData.nombreArchivo;

      if ( nombreFileBD != '') {
        this.labelAdjuntar = 'Modificar Archivo';
        this.nombreArchivoAdjuntar =` ${nombreFileBD} ` ;
        this.urlArchivoAdjuntar = objData.urlArchivo;
      }else{
        this.labelAdjuntar = 'Adjuntar Archivo';
        this.nombreArchivoAdjuntar = '';
        this.urlArchivoAdjuntar = '';
      }


      $('#modal_adjuntar').modal('show');
    },0);
  }
  
  onFileChange(event:any) {
    var filesTemporal = event.target.files; //FileList object
      var fileE:InputFileI [] = [];
      for (var i = 0; i < event.target.files.length; i++) { //for multiple files
        fileE.push({
            'file': filesTemporal[i],
            'namefile': filesTemporal[i].name,
            'status': '',
            'message': ''
        })
      }
       this.files = fileE;
   }


   upload_adjuntar() {
    if ( this.files.length ==0){
      this.alertasService.Swal_alert('error','Por favor seleccione el archivo');
      return;
    }
    Swal.fire({
      icon: 'info',
      allowOutsideClick: false,
      allowEscapeKey: false,
      text: 'Espere por favor'
    })
    Swal.showLoading();
    this.uploadService.upload_adjuntarArchivo( this.files[0].file , this.id_OTGlobal, this.idUserGlobal ).subscribe(
      (res : any) =>{
      Swal.close();
        if (res.ok==true) {

          const { nombreFile, url } = res.data;

          for (const obj of this.ordenTrabajoCab) {
            if (obj.id_OT == this.id_OTGlobal) {
               obj.nombreArchivo = nombreFile ;
               obj.urlArchivo= url ;
               break;
            }
          }
          this.cerrarModal_adjuntar();
        }else{
          this.alertasService.Swal_alert('error',JSON.stringify(res.data));
        }
        },(err) => {
        Swal.close();
        this.alertasService.Swal_alert('error',JSON.stringify(err));
        }
    )

   }

   verArchivo(){
     if ( this.urlArchivoAdjuntar =='') {
       return
     }
     window.open(String( this.urlArchivoAdjuntar),'_blank');
   }


   guardarObservaciones(){

    if ( this.id_OTGlobal == 0 || this.id_OTGlobal == null)  {
      this.alertasService.Swal_alert('error', 'No se cargo la informacion del ID de la Orden Trabajo, actualize su pagina.');
      return
    }

    Swal.fire({
      icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'
    })
    Swal.showLoading();
    this.aprobacionOTService.set_guardarObservaciones( this.id_OTGlobal, this.formParamsObservacion.value.observarcionGestor,  this.formParamsObservacion.value.estatus ,  this.idUserGlobal ).subscribe((res:RespuestaServer)=>{
      Swal.close();
      if (res.ok) {
          this.alertasService.Swal_Success('Proceso generado correctamente');
          this.mostrarInformacion();
      }else{
        this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        alert(JSON.stringify(res.data));
      }
    })

  }




}
