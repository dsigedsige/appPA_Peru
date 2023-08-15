using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.SAP
{
   public class ArticuloSapE
    { 
            public string ID_Articulo { get; set; }
            public string Codigo { get; set; }
            public string Categoria { get; set; }
            public string Linea { get; set; }
            public string SubLinea { get; set; }
            public string Descripcion { get; set; }
            public string Abreviatura { get; set; }
            public string UnidadMedida { get; set; }
            public string StockMinimo { get; set; }
            public string TiempoVida { get; set; }
            public string ExigeNroSerieKardex { get; set; }
            public string Estado { get; set; }
    }
}
