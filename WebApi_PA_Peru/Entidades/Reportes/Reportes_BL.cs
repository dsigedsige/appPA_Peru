﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Reportes
{
    public class Reportes_BL
    {
    }

    public class detalleOT_E
    {
        public int id_OT { get; set; }
        public bool checkeado { get; set; }
        public string descripcionEstado { get; set; }
        public string tipoOT { get; set; }

        public string nroSed { get; set; }
        public string nroSuministro { get; set; }

        public string nroObra { get; set; }
 
        public string direccion { get; set; }
        public string distrito { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }

        public string FechaOrigen { get; set; }
        public string FechaAsignacion { get; set; }
        public string FechaMovil { get; set; }

        public string fechaRegistro { get; set; }
        public string fechaAprobacion { get; set; }
        public string fechaCancelacion { get; set; }


        public string empresaContratista { get; set; }
        public string jefeCuadrilla { get; set; }
        public string generaVolumen { get; set; }
        public string volumenDesmonte { get; set; }
        public string volumenDesmonteRecoger { get; set; }
        public string fechaHora { get; set; }
        public string Informe { get; set; }
        public string totalVolumen { get; set; }
        public string totalDesmonte { get; set; }
        public string totalDesmonteRecoger { get; set; }
 
        public string diasVencimiento { get; set; }

        public int id_tipoTrabajo { get; set; }
        public string id_Distrito { get; set; }
        public string referencia { get; set; }
        public string descripcion_OT { get; set; }
        public int id_estado { get; set; }
        public string descripcionServicio { get; set; }
        public string color { get; set; }
        public string viajeIndebido { get; set; }
        public string tipoTrabajo_OTOrigen { get; set; }
        public string jefeCuadrillaOrigen { get; set; }
        public string observacion { get; set; }

        public string usuarioAprobacion { get; set; }
        public string empresaOrigen { get; set; }
    }


    public class fueraPlazoOT_E
    {
        public int id_OT { get; set; }
        public bool checkeado { get; set; }
        public string descripcionEstado { get; set; }
        public string tipoOT { get; set; }
        public string nroObra { get; set; }

        public string direccion { get; set; }
        public string distrito { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }

        public string FechaAsignacion { get; set; }
        public string FechaMovil { get; set; } 
 
        public string empresaContratista { get; set; }
        public string jefeCuadrilla { get; set; }

        public string fueraPlazoHoras { get; set; }
        public string fueraPlazoDias { get; set; }     
        
        public int id_tipoTrabajo { get; set; }
        public string id_Distrito { get; set; }
        public string referencia { get; set; }
        public string descripcion_OT { get; set; }
        public int id_estado { get; set; }
    }

}
