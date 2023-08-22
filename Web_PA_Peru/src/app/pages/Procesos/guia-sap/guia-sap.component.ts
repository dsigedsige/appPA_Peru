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
  this.detalleGuia=[
   {
    ItemCode:'10624018',
    Dscription:'(LDS) CEMENTO EXPANSIVO BOLSA DE 25 KG',
    WarehouseCode:'ALMENELC',
    Quantity:1,
    UnitPrice:"",
    LineTotal:"",
    LineId:0,
    CostingCode:'01',
    CostingCode2:'9501',
    CostingCode3:'950101',
    AccountCode:'_SYS00000007338'
   },
   {
    ItemCode:'20324212',
    Dscription:'ladrillos 18 huecos',
    WarehouseCode:'capitana',
    Quantity:5,
    UnitPrice:"",
    LineTotal:"",
    LineId:0,
    CostingCode:'02',
    CostingCode2:'8501',
    CostingCode3:'9085',
    AccountCode:'_SYS00000007448'
   },
   {
    ItemCode:'30634313',
    Dscription:'techo 15',
    WarehouseCode:'sucursal',
    Quantity:10,
    UnitPrice:"",
    LineTotal:"",
    LineId:0,
    CostingCode:'03',
    CostingCode2:'9385',
    CostingCode3:'930303',
    AccountCode:'_SYS00000007559'
   }
  ];


    for(let i = 0; i < this.listGuia.length; i++){

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

    }


  }



}


