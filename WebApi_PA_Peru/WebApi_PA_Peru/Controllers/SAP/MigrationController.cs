using Entidades.SAP;
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
    public class MigrationController : ApiController
    {
        public class result
        {
            public bool ok { get; set; }
            public object data { get; set; }

            public object message { get; set; }
        }

        [HttpPost]
        [Route("api/Migration/saveUpdateArticle")]
        public object saveUpdateArticle(ArticuloSapE objArticulo)
        {
            result res = new result();
            object resultado = null;
            try
            {
                MigrationBL obj_negocio = new MigrationBL();
                obj_negocio.set_guardar_articulo(objArticulo);

                res.ok = true;
                res.data = null;
                res.message = "Proceso realizado correctamente";

                resultado = res;
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = null;
                res.message = ex.Message;

                resultado = res;
            }
            return resultado;
        }

        [HttpPost]
        [Route("api/Migration/saveUpdateProvider")]
        public object saveUpdateProvider(ProveedorSapE objProveedor)
        {
            result res = new result();
            object resultado = null;
            try
            {
                MigrationBL obj_negocio = new MigrationBL();
                obj_negocio.set_guardar_proveedor(objProveedor);

                res.ok = true;
                res.data = null;
                res.message = "Proceso realizado correctamente";

                resultado = res;
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = null;
                res.message = ex.Message;

                resultado = res;
            }
            return resultado;
        }


        [HttpPost]
        [Route("api/Migration/saveUpdatePurchaseOrder")]
        public object saveUpdatePurchaseOrder(CompraSapE objOrdenCompra)
        {
            result res = new result();
            object resultado = null;
            try
            {
                MigrationBL obj_negocio = new MigrationBL();
                obj_negocio.set_guardar_ordenCompra(objOrdenCompra);

                res.ok = true;
                res.data = null;
                res.message = "Proceso realizado correctamente";

                resultado = res;
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = null;
                res.message = ex.Message;

                resultado = res;
            }
            return resultado;
        }

        [HttpPost]
        [Route("api/Migration/saveUpdateEmployee")]
        public object saveUpdateEmployee(PersonalSap_E objPersona)
        {
            result res = new result();
            object resultado = null;
            try
            {
                MigrationBL obj_negocio = new MigrationBL();
                obj_negocio.set_guardar_persona(objPersona);

                res.ok = true;
                res.data = null;
                res.message = "Proceso realizado correctamente";

                resultado = res;
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = null;
                res.message = ex.Message;

                resultado = res;
            }
            return resultado;
        }


    }
}
