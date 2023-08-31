import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../services/login/login.service';
import { FormGroup, FormControl } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { AlertasService } from '../../services/alertas/alertas.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  formParams : FormGroup;

  constructor( private alertasService :AlertasService, private spinner: NgxSpinnerService, private loginService : LoginService, private router:Router) { }

  ngOnInit(): void {
    this.inicializarFormularioAnuncio()
  }

  inicializarFormularioAnuncio(){
    this.formParams = new FormGroup({
      usuario : new FormControl(''),
      contrasenia : new FormControl('')
     })
  }


  iniciarSesion(){
    debugger;
        if (this.formParams.value.usuario == '' || this.formParams.value.usuario == 0) {
          this.alertasService.Swal_alert('error','Ingrese el usuario');
          return
        }
        if (this.formParams.value.contrasenia == '' || this.formParams.value.contrasenia == null) {
          this.alertasService.Swal_alert('error','Ingrese la contraseña');
          return
        }

        this.spinner.show();
        this.loginService.get_iniciarSesion(this.formParams.value.usuario, this.formParams.value.contrasenia.trim())
            .subscribe((res:any)=>{
              this.spinner.hide();
              if (res.ok==true) {
                /*-------llamar al home-------*/
                
              }else{
                this.alertasService.Swal_alert('error',JSON.stringify(res.data));
              }
            }, (error)=>{
              this.spinner.hide();
              alert(JSON.stringify(error));
            })
      }




}
