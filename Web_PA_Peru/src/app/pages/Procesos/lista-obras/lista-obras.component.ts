import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ListaObrasService } from 'src/app/services/Procesos/lista-obras.service';
import Swal from 'sweetalert2';
import { NgxSpinnerService } from 'ngx-spinner';
import { RespuestaServer } from 'src/app/models/respuestaServer.models';
import { AlertasService } from 'src/app/services/alertas/alertas.service';
import { InputFileI } from 'src/app/models/inputFile.models';


declare var $:any;
@Component({
  selector: 'app-lista-obras',
  templateUrl: './lista-obras.component.html',
  styleUrls: ['./lista-obras.component.css']
})
export class ListaObrasComponent implements OnInit {

  //formParamsFiltro : FormGroup;

  area :any[]=[];
  cuadrilla :any[]=[];
  estado :any[]=[];
  listObras:any[]=[];
  fotosDetalle:any[]=[];
  Usuario:string='';
  medidasDetalle :any[]=[];
  vGes_Obra_Codigo:string='';
  files:InputFileI[] = [];

  constructor(private listObrasService:ListaObrasService, private spinner: NgxSpinnerService,
             private alertasService : AlertasService) { }

  public formParamsFiltro= new FormGroup({
    idServicio : new FormControl('0'),
    idCuadrilla : new FormControl('0'),
    fecha_ini : new FormControl(""),
    fecha_fin : new FormControl(""),
    idEstado : new FormControl('0'),
    busqueda: new FormControl('')
  })

  public formParamsFile = new FormGroup({
    file : new FormControl('')
   })

  ngOnInit(): void {
    this.inicializarFormularioFiltro();
    this.getComboArea();
    this.getComboEstado();
  }


  inicializarFormularioFiltro(){
    let fechaIni = new Date();
    let fechaFin = new Date();
    fechaIni.setDate(fechaIni.getDate() - 2);
    fechaFin.setDate(fechaFin.getDate() - 1);
    this.formParamsFiltro.controls.fecha_ini.setValue(fechaIni);
    this.formParamsFiltro.controls.fecha_fin.setValue(fechaFin);
  }

  getComboArea(){
    // Swal.fire({
    //   icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'
    // })
    // Swal.showLoading();
    this.spinner.show();
   this.listObrasService.get_area().subscribe((resp:any)=>{
    // Swal.close();
    this.spinner.hide();
        this.area = resp;
    },(error)=>{
      // Swal.close();
      this.spinner.hide();
      console.log(error);
      alert(error);
    })
  }

  getComboCuadrilla(){
    debugger
    this.formParamsFiltro.controls.idCuadrilla.setValue('0');
    this.cuadrilla=[];
    this.spinner.show();
      if(this.formParamsFiltro.controls.idServicio.value!=='0'){
        let area=this.formParamsFiltro.controls.idServicio.value;
        this.listObrasService.get_cuadrilla(area).subscribe((resp:any)=>{
          this.spinner.hide();
            this.cuadrilla = resp;
        },(error)=>{
          alert(error);
        })
      }
   }


  getComboEstado(){
    this.spinner.show();
    this.listObrasService.get_estado().subscribe((resp:any)=>{
      console.log(resp);
      this.spinner.hide();
         this.estado = resp;
     },(error)=>{
       alert(error);
     })
   }

  listarObras(){
    this.spinner.show();
    let servicio=this.formParamsFiltro.controls.idServicio.value;
    let cuadrilla=this.formParamsFiltro.controls.idCuadrilla.value;
    let fecInicio=this.formParamsFiltro.controls.fecha_ini.value;
    let fecFin=this.formParamsFiltro.controls.fecha_fin.value;
    let estado=this.formParamsFiltro.controls.idEstado.value;
    this.listObrasService.get_listarObras_todos(servicio,cuadrilla,fecInicio,fecFin,estado).subscribe((resp:any)=>{
      console.log(resp);
      this.spinner.hide();
         this.listObras = resp;
     },(error)=>{
       alert(error);
     })
  }


  cerrarModal_visor(){
    $('#modal_visorFotos').modal('hide');
  }


  abrirModal_visorFotos(objData:any){
    this.vGes_Obra_Codigo=objData.Ges_Obra_Codigo;
    setTimeout(()=>{ //
      $('#modal_visorFotos').modal('show');
    },0);

    Swal.fire({
      icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Obteniendo Fotos, Espere por favor'
    })
    Swal.showLoading();
    this.listObrasService.get_listarObras_fotos(objData.Ges_Obra_Codigo,this.Usuario).subscribe((res:RespuestaServer)=>{
      Swal.close();
      console.log(res.data);
      if (res.ok) {
        this.fotosDetalle = res.data;
      }else{
        this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        alert(JSON.stringify(res.data));
      }
     })

  }




  anulandoFoto(objFoto:any){
    debugger;
    this.alertasService.Swal_Question('Sistemas', 'Esta seguro de anular ?')
    .then((result)=>{
      if(result.value){
        debugger;
        Swal.fire({
          icon: 'info',
          allowOutsideClick: false,
          allowEscapeKey: false,
          text: 'Anulando la foto, espere por favor'
        })
        Swal.showLoading();
        this.listObrasService.set_anular_fotos(objFoto.IdObraEjecucion,this.Usuario).subscribe((res:RespuestaServer)=>{
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
        },(error)=>{
          console.log(error);
          alert(error);
        })

      }
    })
   }


   onFileChange(event:any) {
    debugger;
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
       //this.insert_obraFotos();
   }


  //  insert_obraFotos(){
  //   debugger;
  //     this.listObrasService.insertObraFotos(this.vGes_Obra_Codigo,'-11.9919828',
  //         '-77.0051692',this.files[0].namefile,'sa').subscribe((res:RespuestaServer)=>{

  //     },(error)=>{
  //       console.log(error);
  //       alert(error);
  //     })
  //  }



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
    this.listObrasService.upload_adjuntarArchivoObras( this.files[0].file , this.vGes_Obra_Codigo, '-11.9919828','-77.0051692','sa' ).subscribe(
      (res : any) =>{
      Swal.close();

        },(err) => {
        Swal.close();
        this.alertasService.Swal_alert('error',JSON.stringify(err));
        }
    )

   }



  descargarFotosObras(pantalla:string){
    debugger;
    // if (pantalla='P') {
    //   if (this.medidasDetalle.length ==0) {
    //     return;
    //   }
    // }

    Swal.fire({
      icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Obteniendo Fotos, Espere por favor'
    })
    Swal.showLoading();
    this.listObrasService.get_descargarFotosObra_todos( this.vGes_Obra_Codigo,this.Usuario).subscribe( (res:any)=>{
      Swal.close();

      if (res.ok ==true) {
       window.open(String(res.data),'_blank');
      }else{
        this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        alert(JSON.stringify(res.data));
      }

    })

  }


}


