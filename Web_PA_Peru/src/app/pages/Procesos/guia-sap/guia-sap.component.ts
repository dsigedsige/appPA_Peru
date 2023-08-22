import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-guia-sap',
  templateUrl: './guia-sap.component.html',
  styleUrls: ['./guia-sap.component.css']
})
export class GuiaSapComponent implements OnInit {

  constructor() { }

  public formParamsFiltro= new FormGroup({
    idMovimiento : new FormControl('0'),
    fecha_emision : new FormControl(""),
  })

  ngOnInit(): void {
    this.inicializarFechaFiltro();
  }

  inicializarFechaFiltro(){
    let fecha_emision = new Date();
    fecha_emision.setDate(fecha_emision.getDate());
    this.formParamsFiltro.controls.fecha_emision.setValue(fecha_emision);
  }

}


