using Entidades.Procesos;
using Negocio.Procesos;
using Negocio.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi_PA_Peru.Controllers.Proceso
{
    public class DocumentsController : ApiController
    {

        [HttpPost]
        [Route("api/Documents/setInsertDocuments")]
        public object setInsertDocuments(Documents documents)
        {
            Resultado res = new Resultado();
            Documents_BL obj_negocio = new Documents_BL();
            object resul = null;
            try
            {               
                res.ok = true;
                res.data = obj_negocio.Insert_Documents(documents);
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




