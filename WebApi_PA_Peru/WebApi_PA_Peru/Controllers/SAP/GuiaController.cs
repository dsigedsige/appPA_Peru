using Entidades.Procesos;
using Entidades.SAP;
using Negocio.Procesos;
using Negocio.Resultados;
using Negocio.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi_PA_Peru.Controllers.SAP
{
    public class GuiaController : ApiController
    {
        [HttpPost]
        [Route("api/Guia/setInsertGuias")]
        public object setInsertGuias(GuiaSap guias)
        {
            Resultado res = new Resultado();
            GuiaBL obj_negocio = new GuiaBL();
            object resul = null;
            try
            {
                res.ok = true;
                res.data = obj_negocio.Insert_Guias(guias);
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
