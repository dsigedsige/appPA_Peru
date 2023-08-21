using Datos;
using Entidades.Procesos;
using Negocio.Procesos;
using Negocio.Resultados;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
                resul = obj_negocio.get_ListaObras(servicio, cuadrilla, fechaInicio, fechaFin, estado);
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
                resul = obj_negocio.get_fotosObras(GesObraCodigo, Usuario);
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


        [HttpGet]
        [Route("api/ListaObras/insertFotosObras")]
        public object insertFotosObras(string GesObraCodigo, string LatitudFoto, string LongitudFoto, string NombreFoto, string Usuario)
        {
            Resultado res = new Resultado();
            ListaObras_BL obj_negocio = new ListaObras_BL();
            object resul = null;
            try
            {
                res.ok = true;
                res.data = obj_negocio.set_insert_obrasFoto(GesObraCodigo, LatitudFoto, LongitudFoto, NombreFoto, Usuario);
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
                  

        [HttpPost]
        [Route("api/ListaObras/PostAdjuntarObraFoto_v2")]
        public object PostAdjuntarObraFoto_v2(string filtros)
        {
            Resultado res = new Resultado();
            string nombreFile = "";
            string nombreFileServer = "";
            string path = "";
           

            try
            {
                var file = HttpContext.Current.Request.Files["file"];
                string extension = System.IO.Path.GetExtension(file.FileName);

                string[] parametros = filtros.Split('|');                
                string GesObraCodigo = parametros[0].ToString();
                string LatitudFoto = parametros[1].ToString();
                string LongitudFoto = parametros[2].ToString();
                string Usuario = parametros[3].ToString();

                nombreFile = file.FileName;


                //-----generando clave unica---
                DateTime currentDate = DateTime.Now;
                string formattedDate = currentDate.ToString("ddMMyyyy");
                string formattedTime = currentDate.ToString("HHmmssfff");

                nombreFileServer = GesObraCodigo + "_" + formattedDate + "_" + formattedTime + extension;

                //---almacenando la imagen--
                path = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Fotos/" + nombreFileServer);
                file.SaveAs(path);

                //------suspendemos el hilo, y esperamos ..
                System.Threading.Thread.Sleep(1000);

                if (File.Exists(path))
                {
                    ListaObras_BL obj_negocioObra = new ListaObras_BL();
                    obj_negocioObra.set_insert_obrasFoto(GesObraCodigo, LatitudFoto, LongitudFoto, nombreFileServer, Usuario);

                    res.ok = true;
                    res.data = "Se grabo";
                }
                else {
                    res.ok = false;
                    res.data = "No se pudo grabar la imagen en el servidor..";
                }          
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }

            return res;
        }



    }
}
