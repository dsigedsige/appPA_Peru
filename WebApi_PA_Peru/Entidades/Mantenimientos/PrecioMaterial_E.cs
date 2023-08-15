using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Mantenimientos
{
    public class PrecioMaterial_E
    {
        public string id_precioMaterial { get; set; }
        public string id_empresa { get; set; }
        public string id_servicio { get; set; }
        public string id_tipoTrabajo { get; set; }
        public string id_TipoMaterial { get; set; }
        public string precio { get; set; }
        public string tipo { get; set; }
        public string id_Baremo { get; set; }
        public string estado { get; set; }
        public string usuario_creacion { get; set; }
    }
}
