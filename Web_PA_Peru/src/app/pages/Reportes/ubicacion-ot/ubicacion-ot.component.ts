
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
import { UbicacionPersonalService } from '../../../services/reportes/ubicacion-personal.service';

declare var $:any;

@Component({
  selector: 'app-ubicacion-ot',
  templateUrl: './ubicacion-ot.component.html',
  styleUrls: ['./ubicacion-ot.component.css']
})
 
export class UbicacionOtComponent implements OnInit,AfterViewInit {

  formParamsFiltro : FormGroup;
  formParamsDatosG : FormGroup;
  idUserGlobal :number = 0;
  servicios :any[]=[]; 
  proveedor :any[]=[];    
  tipoOrdenTrabajo :any[]=[];  
  estados :any[]=[];   
  distritos :any[]=[];

  medidasDetalle :any[]=[]; 
  desmonteDetalle :any[]=[]; 
  fotosDetalle :any[]=[]; 
  detalleOT:any;
 
  filtrarMantenimiento = "";
  datepiekerConfig:Partial<BsDatepickerConfig>;

   /// configuracion google maps
    @ViewChild('mapa', {static: false}) mapaElement: ElementRef;
    map : google.maps.Map;
    markers :google.maps.Marker[] = [];
    infowindows :google.maps.InfoWindow[] = [];


  //-TAB control
  tabControlDetalle: string[] = ['DATOS GENERALES','MEDIDAS','DESMONTE',]; 
  selectedTabControlDetalle :any;

  flagMedidas =  true;
  id_OTGlobal = 0;
  id_tipoOTGlobal = 0;
  id_estadoOTGlobal = 0;
  id_FotoOTGlobal = 0;

  nroObraParteDiario_Global = '';
  fechaHora_Global = '';
  tituloModal = "";

  totalGlobal =0;
  totalGlobal14 =0;
  totalGlobal15 =0;
   
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService, private listaPreciosService : ListaPreciosService, private ordenTrabajoService : OrdenTrabajoService, private ubicacionPersonalService : UbicacionPersonalService , private funcionGlobalServices : FuncionesglobalesService, private aprobacionOTService: AprobacionOTService ) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
  }
 
 ngOnInit(): void {
  this.selectedTabControlDetalle = this.tabControlDetalle[0]; 
  this.getCargarCombos();
  this.inicializarFormularioFiltro();
  this.inicializarFormularioDatosG();
 }

 ngAfterViewInit() {
  this.InicializarMapas()
} 

 inicializarFormularioFiltro(){ 
    this.formParamsFiltro= new FormGroup({
      fechaGps : new FormControl(new Date()),
      idServicio : new FormControl('0'),
      idTipoOT : new FormControl('0'), 
      idProveedor : new FormControl('0') ,
      idEstado : new FormControl('0')       
     }) 
 }

 inicializarFormularioDatosG(){ 
  this.formParamsDatosG= new FormGroup({ 
    direccion : new FormControl(''),
    idDistrito : new FormControl('0'),
    referencia : new FormControl(''),
    descripcionTrabajo : new FormControl(''),
    idEstado : new FormControl('0'), 
   }) 
} 

 getCargarCombos(){ 
    this.spinner.show();
    combineLatest([this.ordenTrabajoService.get_servicio(this.idUserGlobal), this.listaPreciosService.get_tipoOrdenTrabajo(), this.ordenTrabajoService.get_Proveedor(), this.listaPreciosService.get_estados(),this.ordenTrabajoService.get_Distritos()  ]).subscribe( ([ _servicios, _tipoOrdenTrabajo, _proveedor, _estados, _distritos ])=>{
        this.servicios = _servicios;
        this.tipoOrdenTrabajo = _tipoOrdenTrabajo;  
        this.proveedor = _proveedor;  
        this.estados = _estados
        this.distritos = _distritos; 
        this.spinner.hide(); 
    },(error)=>{
      this.spinner.hide(); 
      alert(error);
    })

 }

 InicializarMapas() {

  const latLng = new google.maps.LatLng(-12.046374, -77.0427934 );
  const  mapaOption : google.maps.MapOptions = {
    center : latLng,
    zoom : 13,
    mapTypeControl: true,
  }
  this.map = new google.maps.Map(this.mapaElement.nativeElement, mapaOption);

  if (!document.getElementById("legend")) {
    document.getElementById("content_leyenda").innerHTML = "<div id='legend'></div>";
    const icons = {
        Pendiente: {
            name: 'Pendiente',
            icon: './assets/img/mapa/sum_pendiente.png'
        },
        Asignado: {
            name: 'Asignado',
            icon: './assets/img/mapa/sum_asignado.png'
        },
        Terminado: {
            name: 'Terminado',
            icon: './assets/img/mapa/sum_realizados.png'
        }
    };

    const  legend = document.getElementById('legend');

    for (var key in icons) {
        var type = icons[key];
        var name = type.name;
        var icon = type.icon;
        var div = document.createElement('div');
        div.setAttribute("style", "color:red;text-decoration: line-through;"); 

        if (name == 'Pendiente') {
            div.innerHTML = '<img src="' + icon + '" style="margin-top: -16px;padding: 10px;"> ' + name;
        }
        else if (name == 'Asignado') {
            div.innerHTML = '<img src="' + icon + '" style="margin-top: -16px;padding: 10px;"> ' + name;
        }
        else if (name == 'Terminado') {
            div.innerHTML = '<img src="' + icon + '" style="margin-top: -12px;padding: 10px;"> ' + name;
        }
         legend.appendChild(div);
    }

    this.map.controls[google.maps.ControlPosition.LEFT_CENTER].push(legend);

 };
 
}

 mostrarInformacionMapa(){
      // if (this.formParamsFiltro.value.idServicio == '' || this.formParamsFiltro.value.idServicio == 0) {
      //   this.alertasService.Swal_alert('error','Por favor seleccione el servicio');
      //   return 
      // }
      // if (this.formParamsFiltro.value.fechaGps == '' || this.formParamsFiltro.value.fechaGps == null) {
      //   this.alertasService.Swal_alert('error','Por favor seleccione la fecha Gps');
      //   return 
      // }       
      // if (this.formParamsFiltro.value.idTipoOT == '' || this.formParamsFiltro.value.idTipoOT == 0) {
      //   this.alertasService.Swal_alert('error','Por favor seleccione el Tipo de Orden Trabajo');
      //   return 
      // } 
      // if (this.formParamsFiltro.value.idProveedor == '' || this.formParamsFiltro.value.idProveedor == 0) {
      //   this.alertasService.Swal_alert('error','Por favor seleccione un Proveedor');
      //   return 
      // }
 

      const fechaFormato = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fechaGps); 
      this.spinner.show();
      this.ubicacionPersonalService.get_mostrar_ubicacionesOT(this.formParamsFiltro.value,fechaFormato,  this.idUserGlobal)
          .subscribe((res:RespuestaServer)=>{            
              this.spinner.hide();
   
              if (res.ok==true) {     
                  if(res.data.length > 0){
                    this.MostrarUbicacionesMap( res.data);
                  }else{
                    this.alertasService.Swal_alert('info','No hay información para disponible para mostrar');
                    this.RemoveMarker(null);
                    this.markers = [];
                  }               
                }else{
                this.alertasService.Swal_alert('error', JSON.stringify(res.data));
                alert(JSON.stringify(res.data));
              }
      })
 }   

 MostrarUbicacionesMap(obj_Lista:any) {

  const lat = Number(obj_Lista[0].latitud);
  const lng = Number(obj_Lista[0].longitud);
  const latLng = new google.maps.LatLng(lat, lng);
  // Enfocando el mapa
  this.map.setCenter(latLng);

   this.markers.forEach((marker)=>marker.setMap(null));
   this.markers = [];
 
  for (const ubicaciones of obj_Lista) {
    this.createMarker(ubicaciones);
  }
 }

 createMarker({id_OT, empresa, personal, nroOrden, direccion, fechaRegistro, diasVencimiento, latitud, longitud, idEstado, fechaHora, id_Distrito, referencia, id_tipoTrabajo, descripcion_OT,id_estadoOT

}) {

  let icono = '';
  let titulo= '';

  let ContenidoMarker = '';
  ContenidoMarker += '<div  id="_market" style="width:500px;height:160px;position:relative;">';
  ContenidoMarker += '<table><tr><td class="text-info"><strong > Nro. OT/TD </strong></td><td style="width:100%">: ' + nroOrden + '</td></tr>';
  ContenidoMarker += '<tr><td class="text-info"><div style="width: 110px;">Sub Contrata</div></td><td style="width:100%">: ' + empresa + ' </td></tr>';
  ContenidoMarker += '<tr><td class="text-info"><strong>Personal</strong></td><td>: ' + personal + '</td></tr>';
  ContenidoMarker += '<tr><td class="text-info"><strong>Direccion</strong></td><td>: ' + direccion + '</td></tr>';
  ContenidoMarker += '<tr><td class="text-info"><strong> Fecha Registro</strong></td><td>: ' + fechaRegistro + '</td></tr>';
  ContenidoMarker += '<tr><td class="text-info"><strong>Dias Vencimiento</strong></td><td>: ' + diasVencimiento + ' </td></tr></tr>'; 
  ContenidoMarker += '<tr><td> </td><td style="width:100%"  > <button  id="btn' + id_OT + '" class="btn btn-block btn-outline-primary btn-sm ">Ver Informe</button> </td></tr></table>';


  if (idEstado =='P') {
    icono = './assets/img/mapa/sum_pendiente.png';
    titulo = 'OT pendientes';
  }
  if (idEstado =='A') {
    icono = './assets/img/mapa/sum_asignado.png';
    titulo = 'OT Asignados';
  }
  if (idEstado =='T') {
    icono = './assets/img/mapa/sum_realizados.png';
    titulo = 'OT Terminados';
  }

  const marker = new google.maps.Marker({
        map: this.map,
        position: new google.maps.LatLng( Number(latitud), Number(longitud)),
        title: titulo,
        icon: icono
  }); 

  this.markers.push(marker); 

  const infowindow = new google.maps.InfoWindow({
    content: ContenidoMarker
  });

  this.infowindows.push(infowindow); 

  google.maps.event.addDomListener(marker,'click',()=>{
    //-----borrando los infowindows
    this.infowindows.forEach(infoW=>infoW.close());
    infowindow.setContent('<center><h4><b> UBICACIÓN OT </b></h4></center>' + ContenidoMarker);
    infowindow.open(this.map, marker);
  }) 

      //-----modal informe
    google.maps.event.addListener(infowindow, 'domready',   ()=> { 
      if (document.getElementById('btn' + id_OT)) { 
          google.maps.event.addDomListener(document.getElementById('btn' + id_OT), 'click', ()=> {  
 
          this.abrirModal_OT({id_OT : id_OT, nroObra: nroOrden ,FechaAsignacion : fechaHora ,direccion: direccion , id_Distrito: id_Distrito , referencia : referencia, descripcion_OT : descripcion_OT, id_tipoTrabajo : id_tipoTrabajo ,id_estado: id_estadoOT })
        
          });
          return
      } else {
          return
      }
    })

}
  
  RemoveMarker(map:any) {
    for (var i = 0; i < this.markers.length; i++) {
        this.markers[i].setMap(map);
    }
  }





  cerrarModal_OT(){
    setTimeout(()=>{ // 
      $('#modal_OT').modal('hide');  
    },0); 
  }

  descargarFotosOT(pantalla:string){

    if (pantalla='P') {
      if (this.medidasDetalle.length ==0) {
        return;
      }    
    }
  
    Swal.fire({
      icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Obteniendo Fotos, Espere por favor'
    })
    Swal.showLoading();  
    this.aprobacionOTService.get_descargarFotosOT_todos( this.id_OTGlobal, this.id_tipoOTGlobal, this.idUserGlobal ).subscribe( (res:any)=>{           
      Swal.close();
  
      if (res.ok ==true) {   
       window.open(String(res.data),'_blank');
      }else{
        this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        alert(JSON.stringify(res.data));
      }
      
    })
  
  }
  
  descargarFotosOT_visor(pantalla:string){
   
    if (this.fotosDetalle.length ==0) {
      return;
    }    
    Swal.fire({
      icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Obteniendo Fotos, Espere por favor'
    })
    Swal.showLoading();  
    this.aprobacionOTService.get_descargarFotosOT_visor( this.id_FotoOTGlobal , this.id_tipoOTGlobal, this.idUserGlobal ).subscribe( (res:any)=>{           
      Swal.close();
  
      if (res.ok ==true) {   
       window.open(String(res.data),'_blank');
      }else{
        this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        alert(JSON.stringify(res.data));
      }
      
    })
  
  }

  abrirModal_visorFotos(objData:any){ 

    this.detalleOT = objData;
    this.id_FotoOTGlobal = objData.id_OTDet;

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

  
  cerrarModal_visor(){
    $('#modal_visorFotos').modal('hide');    
  }

      
  get_medidasOT(){
    this.aprobacionOTService.get_medidasOT(this.id_OTGlobal, this.id_tipoOTGlobal, this.idUserGlobal).subscribe((res:RespuestaServer)=>{
     if (res.ok) {            
       this.medidasDetalle = res.data; 
       
       let importeTotal =0;
       for (const iterator of  this.medidasDetalle) {
        importeTotal += iterator.total;
       }
       
       this.totalGlobal = importeTotal;
  
     }else{
       this.alertasService.Swal_alert('error', JSON.stringify(res.data));
       alert(JSON.stringify(res.data));
     }      
    })        
  }

  get_desmonteOT(){
    this.aprobacionOTService.get_mesmonteOT(this.id_OTGlobal, this.id_tipoOTGlobal, this.idUserGlobal).subscribe((res:RespuestaServer)=>{
     if (res.ok) {            
       this.desmonteDetalle = res.data; 
       
       let importeTotal14 =0;
       let importeTotal15 =0;

       for (const iterator of  this.desmonteDetalle) {
         
          if (iterator.id_TipoMaterial == 14 ) {
            importeTotal14 += iterator.total;
          }
          if (iterator.id_TipoMaterial == 15 ) {
            importeTotal15 += iterator.total;
          }

       }
       
       this.totalGlobal14 = importeTotal14;
       this.totalGlobal15 = importeTotal15;

  
     }else{
       this.alertasService.Swal_alert('error', JSON.stringify(res.data));
       alert(JSON.stringify(res.data));
     }      
    })        
  }


  abrirModal_OT( {id_OT,nroObra,FechaAsignacion,direccion, id_Distrito, referencia, descripcion_OT, id_tipoTrabajo,id_estado  }){ 
  
    // //----- Datos Generales  -----  
    this.id_OTGlobal = id_OT;
    this.id_tipoOTGlobal = id_tipoTrabajo;
    this.nroObraParteDiario_Global = nroObra;
    this.fechaHora_Global = FechaAsignacion;
    this.id_estadoOTGlobal = id_estado;
  
    this.totalGlobal =0;
    this.totalGlobal14 =0;
    this.totalGlobal15 =0;
  
    this.tituloModal = 'Tipo Trabajo No-Definido'
  
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
    
    
    this.formParamsDatosG.patchValue({"direccion": direccion , "idDistrito": id_Distrito, "referencia": referencia, "descripcionTrabajo": descripcion_OT , "idEstado": id_estado });
    this.selectedTabControlDetalle = this.tabControlDetalle[0];
  
    setTimeout(()=>{ // 
      $('#modal_OT').modal('show');
    },0);
  }
  

 
  
 
  
 

}