import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { ProveedorComponent } from './pages/Mantenimientos/proveedor/proveedor.component';
import { PersonalComponent } from './pages/Mantenimientos/personal/personal.component';
import { UsuariosComponent } from './pages/Accesos/usuarios/usuarios.component';
import { DistritosComponent } from './pages/Mantenimientos/distritos/distritos.component';
import { ListaPreciosComponent } from './pages/Mantenimientos/lista-precios/lista-precios.component';
import { OrdenTrabajoComponent } from './pages/Procesos/orden-trabajo/orden-trabajo.component';
import { AprobacionOTComponent } from './pages/Procesos/aprobacion-ot/aprobacion-ot.component';
import { UbicacionPersonalComponent } from './pages/Reportes/ubicacion-personal/ubicacion-personal.component';
import { DetalleOTComponent } from './pages/Reportes/detalle-ot/detalle-ot.component';
import { FueraPlazoComponent } from './pages/Reportes/fuera-plazo/fuera-plazo.component';
import { ConfiguracionZonasComponent } from './pages/Mantenimientos/configuracion-zonas/configuracion-zonas.component';
import { UbicacionOtComponent } from './pages/Reportes/ubicacion-ot/ubicacion-ot.component';
import { AccesosComponent } from './pages/Accesos/accesos/accesos.component';
import { CargoPersonalComponent } from './pages/Mantenimientos/cargo-personal/cargo-personal.component';
import { AreasComponent } from './pages/Mantenimientos/areas/areas.component';
import { EstadosComponent } from './pages/Mantenimientos/estados/estados.component';
import { ReporteAnalisisComponent } from './pages/Reportes/reporte-analisis/reporte-analisis.component';
import { DetalladoContratistaComponent } from './pages/Reportes/detallado-contratista/detallado-contratista.component';
import { PrecioEmpresaComponent } from './pages/Mantenimientos/precio-empresa/precio-empresa.component';
 
 
const APP_ROUTERS: Routes = [
    //{ path: 'home', component: HomeComponent  ,  canActivate: [AuthGuard]},  
    { path: '', component: HomeComponent},  
    { path: 'mantenimiento-proveedor', component: ProveedorComponent},  
    { path: 'mantenimiento-personal', component: PersonalComponent},  
    { path: 'mantenimiento-usuarios', component: UsuariosComponent},  
    { path: 'mantenimiento-distritos', component: DistritosComponent},  
    { path: 'mantenimiento-lista-precios', component: ListaPreciosComponent},  


    { path: 'proceso-orden-trabajo', component: OrdenTrabajoComponent},  
    { path: 'proceso-aprobacion-orden-trabajo', component: AprobacionOTComponent},  

    { path: 'reporte-ubicacion-personal', component: UbicacionPersonalComponent},  
    { path: 'reporte-detalle-ot', component: DetalleOTComponent},  
    { path: 'reporte-fuerza-plazo', component: FueraPlazoComponent},  

    { path: 'mantenimiento-config-zonas', component: ConfiguracionZonasComponent},  
    { path: 'reporte-ubicacion-ot', component: UbicacionOtComponent},  

    { path: 'seguridad-accesos', component: AccesosComponent , canActivate: [ AuthGuard]}, 
    { path: 'mantenimiento-cargo-personal', component: CargoPersonalComponent},  
    { path: 'mantenimiento-areas', component: AreasComponent},  
    { path: 'mantenimiento-estados', component: EstadosComponent},  

    { path: 'reporte-analisis', component: ReporteAnalisisComponent},  
    { path: 'reporte-detallado-contratista', component: DetalladoContratistaComponent},  

    { path: 'mantenimiento-precio-empresa', component: PrecioEmpresaComponent }, 

    { path: '', pathMatch:'full', redirectTo:'inicio' },
    { path: '**', pathMatch:'full', redirectTo:'inicio' },
  ];
  
  export const APP_ROUTING = RouterModule.forRoot(APP_ROUTERS,{useHash:true});  


 