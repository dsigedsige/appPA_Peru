
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
import { DetalleOTService } from '../../../services/reportes/detalle-ot.service';

declare var $:any;


@Component({
  selector: 'app-reporte-analisis',
  templateUrl: './reporte-analisis.component.html',
  styleUrls: ['./reporte-analisis.component.css']
})


export class ReporteAnalisisComponent implements OnInit {

  formParamsFiltro : FormGroup;
  filtrarProyectista = '';
  idUserGlobal = 0;
  tituloCabera =''; 

  servicios :any[]=[];   
  distritos :any[]=[];
  proveedor :any[]=[];   
  estados :any[]=[];   
  tipoOrdenTrabajo :any[]=[];  
 
  datepiekerConfig:Partial<BsDatepickerConfig> 
  showReporteDetallado = false;

  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService, private listaPreciosService : ListaPreciosService, private ordenTrabajoService : OrdenTrabajoService, private aprobacionOTService : AprobacionOTService , private funcionGlobalServices : FuncionesglobalesService, private detalleOTService:DetalleOTService ) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
   }
  

  ngOnInit(): void {
    this.getCargarCombos();
    this.inicializarFormularioFiltro();
  }

  inicializarFormularioFiltro(){ 
    this.formParamsFiltro= new FormGroup({
      idServicio : new FormControl('0'),
      idTipoOT : new FormControl('0'),
      idProveedor : new FormControl('0'),
      fecha_ini : new FormControl(new Date()),
      fecha_fin : new FormControl(new Date()), 
      idEstado : new FormControl('0'),
      tipoReporte : new FormControl('1'),
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

 mostrarInformacion_reporte(){ 
 
      if (this.formParamsFiltro.value.idTipoOT == '' || this.formParamsFiltro.value.idTipoOT == 0) {
        this.alertasService.Swal_alert('error','Por favor seleccione el Tipo de Orden Trabajo');
        return 
      }  
      if (this.formParamsFiltro.value.fecha_ini == '' || this.formParamsFiltro.value.fecha_ini == null) {
        this.alertasService.Swal_alert('error','Por favor seleccione la fecha inicial');
        return 
      }
      if (this.formParamsFiltro.value.fecha_fin == '' || this.formParamsFiltro.value.fecha_fin == null) {
        this.alertasService.Swal_alert('error','Por favor seleccione la fecha final');
        return 
      } 

      const fechaIni = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_ini);
      const fechaFin = this.funcionGlobalServices.formatoFecha(this.formParamsFiltro.value.fecha_fin);
 
 
   this.spinner.show();
   this.detalleOTService.get_descargarReporteAnalisis(this.formParamsFiltro.value, fechaIni, fechaFin, this.idUserGlobal ).subscribe((res:RespuestaServer)=>{            
     this.spinner.hide();

     console.log(res)

     if (res.ok==true) {         
      window.open(String(res.data),'_blank');
     }else{
       this.alertasService.Swal_alert('error', JSON.stringify(res.data));
       alert(JSON.stringify(res.data));
     }
   })

 }

 changeDetallado(opcionReporte:any){
   if (opcionReporte ==4) {
    this.showReporteDetallado = true;
    setTimeout(() => {
      $("#inlineRadio6").attr('checked', true);
      this.formParamsFiltro.patchValue({ "tipoReporte": '6' });
    }, 0);

   }
  else {
    this.showReporteDetallado = false;
   }
 
 }



}
