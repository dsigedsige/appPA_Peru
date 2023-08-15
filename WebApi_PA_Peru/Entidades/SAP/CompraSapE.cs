using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.SAP
{
    public class CompraSapE
    {
        public string Id_Compra { get; set; }
        public string Almacen { get; set; }
        public string Localidad { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDoc { get; set; }
        public string NroGuiaRemision { get; set; }
        public string OCompra { get; set; }
        public string Observacion { get; set; }
        public string FechaGuia { get; set; }
        public string FechaEmision { get; set; }
        public string Moneda { get; set; }
        public string Proveedor { get; set; }
        public string TipoCambio { get; set; }

        public List<DetalleCompras> DetalleCompras { get; set; }
    }
    



    public class DetalleCompras
    {
        public string Id_Compra { get; set; }
        public string Id_Compra_Det { get; set; }
        public string CodigoArticulo { get; set; }
        public string Cantidad { get; set; }
        public string Precio { get; set; }
        public string Estado { get; set; }
    }
}
