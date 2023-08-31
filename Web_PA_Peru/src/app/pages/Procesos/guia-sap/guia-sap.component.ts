import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { DetalleGuia } from 'src/app/models/detalleguia.models';
import { GuiaSap } from 'src/app/models/guiasap.models';
import { GuiaSapService } from 'src/app/services/Procesos/guia-sap.service';
import { AlertasService } from 'src/app/services/alertas/alertas.service';

@Component({
  selector: 'app-guia-sap',
  templateUrl: './guia-sap.component.html',
  styleUrls: ['./guia-sap.component.css']
})
export class GuiaSapComponent implements OnInit {

  movimiento :any[]=[];
  listGuia:any[]=[];
  detalleGuia:DetalleGuia[]=[];

  constructor(private guiaService:GuiaSapService, private spinner: NgxSpinnerService,
    private alertasService : AlertasService) { }

  public formParamsFiltro= new FormGroup({
    idMovimiento : new FormControl('0'),
    fecha_emision : new FormControl(""),
  })

  ngOnInit(): void {
    this.inicializarFechaFiltro();
    this.getComboMovimiento();
  }

  inicializarFechaFiltro(){
    let fecha_emision = new Date();
    fecha_emision.setDate(fecha_emision.getDate());
    this.formParamsFiltro.controls.fecha_emision.setValue(fecha_emision);
  }


  getComboMovimiento(){
    this.spinner.show();
    this.guiaService.get_movimiento().subscribe((resp:any)=>{
    this.spinner.hide();
       this.movimiento = resp;
    },(error)=>{

      this.spinner.hide();
      console.log(error);
      alert(error);
    })
  }


  listarGuias(){
    debugger;
    if ( this.formParamsFiltro.controls.idMovimiento.value == '0' )  {
      this.alertasService.Swal_alert('error', 'Seleccione el tipo de movimiento.');
      return
    }
    this.spinner.show();
    let movimiento=this.formParamsFiltro.controls.idMovimiento.value;
    let fecha=this.formParamsFiltro.controls.fecha_emision.value;
    this.guiaService.get_listarGuias(movimiento,fecha).subscribe((resp:any)=>{
    this.spinner.hide();
       this.listGuia = resp;
    },(error)=>{

      this.spinner.hide();
      console.log(error);
      alert(error);
    })
  }


 async enviar_guiasSAP(){
  debugger;
  let vContador:number=0;
  if(this.listGuia.length==0){
    this.alertasService.Swal_alert('error', 'no se puede enviar al SAP, no existe registros.');
    return
  }
  this.spinner.show();

  try{
     for(let i = 0; i < this.listGuia.length; i++){
          try {
            var listDetGuia = await this.guiaService.get_listarDetalleGuias(this.listGuia[i].Reference2).toPromise();
            if (Array.isArray(listDetGuia)) {
              this.detalleGuia = listDetGuia;
            } else {
              console.error("Los datos recibidos del guiaService no son un arreglo.");
            }
          } catch (error) {
            console.error("Ocurrió un error:", error);
          }
      let guias:GuiaSap={
        Series:this.listGuia[i].Series,
        DocDate:this.listGuia[i].DocDate,
        TaxDate:this.listGuia[i].TaxDate,
        DocTotal:this.listGuia[i].DocTotal,
        Reference2:this.listGuia[i].Reference2,
        Comments:this.listGuia[i].Comments,
        U_EXP_CODSN:this.listGuia[i].U_EXP_CODSN,
        U_EXP_NOMBRESN:this.listGuia[i].U_EXP_NOMBRESN,
        U_EXX_FE_SN:this.listGuia[i].U_EXX_FE_SN,
        U_PA_AVCREADOPOR:this.listGuia[i].U_PA_AVCREADOPOR,
        U_EXX_TIPOOPER:this.listGuia[i].U_EXX_TIPOOPER,
        U_EXX_TIPMOV:this.listGuia[i].U_EXX_TIPMOV,
        U_EXX_MOTIVTRA:this.listGuia[i].U_EXX_MOTIVTRA,
        U_EXX_FE_MODTRA:this.listGuia[i].U_EXX_FE_MODTRA,
        U_EXX_FE_DIRSN:this.listGuia[i].U_EXX_FE_DIRSN,
        U_EXX_CODTRANS:this.listGuia[i].U_EXX_CODTRANS,
        U_EXX_NOMTRANS:this.listGuia[i].U_EXX_NOMTRANS,
        U_EXX_RUCTRANS:this.listGuia[i].U_EXX_RUCTRANS,
        U_EXX_DIRTRANS:this.listGuia[i].U_EXX_DIRTRANS,
        U_EXX_LICCONDU:this.listGuia[i].U_EXX_LICCONDU,
        U_EXX_NOMCONDU:this.listGuia[i].U_EXX_NOMCONDU,
        U_EXX_PLACAVEH:this.listGuia[i].U_EXX_PLACAVEH,
        U_EXP_OTROS:this.listGuia[i].U_EXP_OTROS,
        U_EXP_DEST:this.listGuia[i].U_EXP_DEST,
        U_EXX_FE_GR_FInicio:this.listGuia[i].U_EXX_FE_GR_FInicio,
        U_EXX_FE_GRPESOTOTAL:this.listGuia[i].U_EXX_FE_GRPESOTOTAL,
        U_EXX_FE_GRDESMOTOTROS:this.listGuia[i].U_EXX_FE_GRDESMOTOTROS,
        U_EXX_FE_INDVHM1L:this.listGuia[i].U_EXX_FE_INDVHM1L,
        U_EXX_FE_GRINDRETVH:this.listGuia[i].U_EXX_FE_GRINDRETVH,
        U_EXX_FE_GRINDENVV:this.listGuia[i].U_EXX_FE_GRINDENVV,
        U_EXX_FE_GRINDTTDAM:this.listGuia[i].U_EXX_FE_GRINDTTDAM,
        U_EXX_FE_GRINDVHCT:this.listGuia[i].U_EXX_FE_GRINDVHCT,
        U_EXX_FE_GRUMPESOTOTAL:this.listGuia[i].U_EXX_FE_GRINDVHCT,
        DocumentLines:this.detalleGuia
      };
      var resp = await this.guiaService.insertGuiaSap(guias).toPromise();
      vContador=vContador+1;
    }
    if (vContador>0){
      this.spinner.hide();
    }else{

    }
   } catch (error) {
    this.spinner.hide();
    console.error("Ocurrió un error:", error);
   }

  }



 async reenviar_guias(item:any){
  this.spinner.show();
  debugger;
  try{
    try {
        var listDetGuia = await this.guiaService.get_listarDetalleGuias(item.NroGuia).toPromise();
        if (Array.isArray(listDetGuia)) {
          this.detalleGuia = listDetGuia;
        } else {
          console.error("Los datos recibidos del guiaService no son un arreglo.");
        }
     } catch (error) {
      console.error("Ocurrió un error:", error);
    }

    let guias:GuiaSap={
      Series:item.Series,
      DocDate:item.DocDate,
      TaxDate:item.TaxDate,
      DocTotal:item.DocTotal,
      Reference2:item.Reference2,
      Comments:item.Comments,
      U_EXP_CODSN:item.U_EXP_CODSN,
      U_EXP_NOMBRESN:item.U_EXP_NOMBRESN,
      U_EXX_FE_SN:item.U_EXX_FE_SN,
      U_PA_AVCREADOPOR:item.U_PA_AVCREADOPOR,
      U_EXX_TIPOOPER:item.U_EXX_TIPOOPER,
      U_EXX_TIPMOV:item.U_EXX_TIPMOV,
      U_EXX_MOTIVTRA:item.U_EXX_MOTIVTRA,
      U_EXX_FE_MODTRA:item.U_EXX_FE_MODTRA,
      U_EXX_FE_DIRSN:item.U_EXX_FE_DIRSN,
      U_EXX_CODTRANS:item.U_EXX_CODTRANS,
      U_EXX_NOMTRANS:item.U_EXX_NOMTRANS,
      U_EXX_RUCTRANS:item.U_EXX_RUCTRANS,
      U_EXX_DIRTRANS:item.U_EXX_DIRTRANS,
      U_EXX_LICCONDU:item.U_EXX_LICCONDU,
      U_EXX_NOMCONDU:item.U_EXX_NOMCONDU,
      U_EXX_PLACAVEH:item.U_EXX_PLACAVEH,
      U_EXP_OTROS:item.U_EXP_OTROS,
      U_EXP_DEST:item.U_EXP_DEST,
      U_EXX_FE_GR_FInicio:item.U_EXX_FE_GR_FInicio,
      U_EXX_FE_GRPESOTOTAL:item.U_EXX_FE_GRPESOTOTAL,
      U_EXX_FE_GRDESMOTOTROS:item.U_EXX_FE_GRDESMOTOTROS,
      U_EXX_FE_INDVHM1L:item.U_EXX_FE_INDVHM1L,
      U_EXX_FE_GRINDRETVH:item.U_EXX_FE_GRINDRETVH,
      U_EXX_FE_GRINDENVV:item.U_EXX_FE_GRINDENVV,
      U_EXX_FE_GRINDTTDAM:item.U_EXX_FE_GRINDTTDAM,
      U_EXX_FE_GRINDVHCT:item.U_EXX_FE_GRINDVHCT,
      U_EXX_FE_GRUMPESOTOTAL:item.U_EXX_FE_GRINDVHCT,
      DocumentLines:this.detalleGuia
    };
    var resp = await this.guiaService.insertGuiaSap(guias).toPromise();
    console.log(resp);
    this.spinner.hide();
   } catch (error) {
    this.spinner.hide();
    console.error("Ocurrió un error:", error);
   }
  }

}


