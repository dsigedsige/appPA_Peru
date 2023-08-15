using Entidades.Mantenimientos;
using Negocio.Mantenimientos;
using Negocio.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi_3R_Dominion.Controllers.Mantenimiento
{
    [EnableCors("*", "*", "*")]
    public class PrecioMaterialController : ApiController
    {

        [HttpPost]
        [Route("api/PrecioMaterial/post_precioMaterial")]
        public object post_precioMaterial(PrecioMaterial_E PrecioMaterial_E)
        {
            Resultado res = new Resultado();
            object resul = null;
            try
            {
                Usuarios_BL obj_negocio = new Usuarios_BL();

                res.ok = true;
                res.data = obj_negocio.set_grabandoPrecioEmpresa(PrecioMaterial_E);
                res.totalpage = 0;
                resul = res;
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
                res.totalpage = 0;

                resul = res;
            }

            return resul;
        }


    }
}
