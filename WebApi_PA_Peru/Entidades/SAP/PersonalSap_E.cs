using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.SAP
{
    public class PersonalSap_E
    {
        public string CodigoInterno { get; set; }
        public string Dni { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }

        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaCese { get; set; }
        public string Cargo { get; set; }
        public string Estado { get; set; }
    }
}
