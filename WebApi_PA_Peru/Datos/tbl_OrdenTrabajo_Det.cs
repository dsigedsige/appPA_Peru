//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_OrdenTrabajo_Det
    {
        public int id_OTDet { get; set; }
        public Nullable<int> id_OT { get; set; }
        public Nullable<int> id_TipoTrabajo { get; set; }
        public Nullable<int> id_TipoMaterial { get; set; }
        public Nullable<int> id_TipoDesmonte { get; set; }
        public Nullable<decimal> Largo_OTDet { get; set; }
        public Nullable<decimal> Ancho_OTDet { get; set; }
        public Nullable<decimal> Espesor_OTDet { get; set; }
        public Nullable<decimal> total_OTDet { get; set; }
        public string nroPlacaVehiculo { get; set; }
        public Nullable<decimal> m3Vehiculo { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_edicion { get; set; }
        public Nullable<System.DateTime> fecha_edicion { get; set; }
        public Nullable<decimal> total_OTSoles { get; set; }
        public string latitud_Det { get; set; }
        public string longitud_Det { get; set; }
        public Nullable<double> Cant_Panos { get; set; }
        public Nullable<decimal> Med_Horizontal { get; set; }
        public Nullable<decimal> Med_Vertical { get; set; }
        public Nullable<decimal> Precio_Unitario { get; set; }
    }
}
