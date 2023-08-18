using Negocio.Procesos;
using Negocio.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi_PA_Peru.Controllers.Proceso
{
    [EnableCors("*", "*", "*")]
    public class ListaObrasController : ApiController
    {
        [HttpGet]
        [Route("api/ListaObras/GetArea")]
        public object GetArea()
        
        {
            Resultado res = new Resultado();
            ListaObras_BL obj_negocio = new ListaObras_BL();
            object resul = null;
            try
            {               
                resul = obj_negocio.get_Areas();                                     
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
        [Route("api/ListaObras/GetCuadrilla")]
        public object GetCuadrilla(string area)        
        {
            Resultado res = new Resultado();
            ListaObras_BL obj_negocio = new ListaObras_BL();
            object resul = null;
            try
            {
                resul = obj_negocio.get_Cuadrillas(area);
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
        [Route("api/ListaObras/GetEstado")]
        public object GetEstado()
        {
            Resultado res = new Resultado();
            ListaObras_BL obj_negocio = new ListaObras_BL();
            object resul = null;
            try
            {
                resul = obj_negocio.get_Estado();
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
        [Route("api/ListaObras/GetListaObras")]
        public object GetListaObras(string servicio, string cuadrilla, string fechaInicio, string fechaFin, string estado)
        {
            Resultado res = new Resultado();
            ListaObras_BL obj_negocio = new ListaObras_BL();
            object resul = null;
            try
            {
                resul = obj_negocio.get_ListaObras(servicio,cuadrilla,fechaInicio,fechaFin,estado);
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
        [Route("api/ListaObras/GetFotosObras")]
        public object GetFotosObras(string GesObraCodigo, string Usuario)
        {
            Resultado res = new Resultado();
            ListaObras_BL obj_negocio = new ListaObras_BL();
            object resul = null;
            try
            {
                resul = obj_negocio.get_fotosObras(GesObraCodigo,Usuario);
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
        [Route("api/ListaObras/setAnularFotosObras")]
        public object setAnularFotosObras(string IdObraEjecucion, string Usuario)
        {
            Resultado res = new Resultado();
            ListaObras_BL obj_negocio = new ListaObras_BL();
            object resul = null;
            try
            {                
                res.ok = true;
                res.data = obj_negocio.set_actualizar_obrasFoto(IdObraEjecucion, Usuario);
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


        [HttpGet]
        [Route("api/ListaObras/descargarFotosObrasTodo")]
        public object descargarFotosObrasTodo(string GesObraCodigo, string Usuario)
        {
            Resultado res = new Resultado();
            ListaObras_BL obj_negocio = new ListaObras_BL();
            object resul = null;
            try
            {
                res.ok = true;
                res.data = obj_negocio.get_descargar_Todos_fotosObras(GesObraCodigo, Usuario);
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
