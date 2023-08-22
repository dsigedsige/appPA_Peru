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
using System.Web.Http.Cors;

namespace WebApi_PA_Peru.Controllers.SAP
{
    [EnableCors("*", "*", "*")]
    public class GuiaController : ApiController
    {

        [HttpGet]
        [Route("api/Guia/GetMovimiento")]
        public object GetMovimiento()
        {
            Resultado res = new Resultado();
            GuiaBL obj_negocio = new GuiaBL();
            object resul = null;
            try
            {
                resul = obj_negocio.get_Movimiento();
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


        [HttpGet]
        [Route("api/Guia/GetGuias")]
        public object GetGuias(string movimiento, string fecha)
        {
            Resultado res = new Resultado();
            GuiaBL obj_negocio = new GuiaBL();
            object resul = null;
            try
            {
                resul = obj_negocio.get_Guias(movimiento, fecha);
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


