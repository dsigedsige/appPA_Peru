using Datos;
using Negocio.Procesos;
using Negocio.Resultados;
using Negocio.upload;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

 
namespace WebApi_3R_Dominion.Controllers.upload
{
    [EnableCors("*", "*", "*")]
    public class UploadsController : ApiController
    {
        private Proyecto_3REntities1 db = new Proyecto_3REntities1();

        [HttpPost]
        [Route("api/Uploads/post_archivoExcel")]
        public object post_archivoExcel(string filtros)
        {
            Resultado res = new Resultado();
            var nombreFile = "";
            string sPath = "";

            try
            {
                //--- obteniendo los parametros que vienen por el FormData

                var file = HttpContext.Current.Request.Files["file"];
                //--- obteniendo los parametros que vienen por el FormData
                string extension = Path.GetExtension(file.FileName);

                string[] parametros = filtros.Split('|');
                string fechaImportacion = parametros[0].ToString();
                string idUsuario = parametros[1].ToString();

                //nombreFile = "Impotacion_Excels" + "_" + idUsuario + extension;

                //-----generando clave unica---
                var guid = Guid.NewGuid();
                var guidB = guid.ToString("B");
                nombreFile = idUsuario + "_Importacion_Excel_" + Guid.Parse(guidB) + extension;

                //-------almacenando la archivo---
                sPath = HttpContext.Current.Server.MapPath("~/Archivos/Excel/" + nombreFile);
                //if (System.IO.File.Exists(sPath))
                //{
                //    System.IO.File.Delete(sPath);
                //}
                file.SaveAs(sPath);

                //-------almacenando la archivo---
                if (File.Exists(sPath))
                {
                    Upload_BL obj_negocio = new Upload_BL();
                    string valor = obj_negocio.setAlmacenandoFile_Excel(sPath, file.FileName, fechaImportacion, idUsuario);
                    if (valor == "OK")
                    {
                        res.ok = true;
                        res.data = obj_negocio.get_datosCargados(idUsuario, fechaImportacion);
                        res.totalpage = 0;
                    }
                }
                else
                {
                    res.ok = false;
                    res.data = "No se pudo almacenar el archivo en el servidor";
                    res.totalpage = 0;
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
                res.totalpage = 0;
            }
            return res;
        }
        
        [HttpPost]
        [Route("api/Uploads/post_archivosAcronimos")]
        public object post_archivosAcronimos(string filtros)
        {
            Resultado res = new Resultado();
            int nombreFileBD;
            string sPath = "";

            try
            {
                //--- obteniendo los parametros que vienen por el FormData

                var file = HttpContext.Current.Request.Files["file"];
                //--- obteniendo los parametros que vienen por el FormData
                string extension = Path.GetExtension(file.FileName);

                string[] parametros = filtros.Split('|');
                int idOt = Convert.ToInt32(parametros[0].ToString());
                int idFormato = Convert.ToInt32(parametros[1].ToString());
                int idUsuario = Convert.ToInt32(parametros[2].ToString());
                int opcionModal = Convert.ToInt32(parametros[3].ToString());
                string codCad =  parametros[4].ToString();
 


                Upload_BL obj_negocios = new Upload_BL();
                nombreFileBD = obj_negocios.crear_archivoAcronimo(idOt, idFormato, idUsuario, file.FileName, opcionModal, codCad);
                
                //-------almacenando la archivo---
                sPath = HttpContext.Current.Server.MapPath("~/Archivos/Acronimos/" + nombreFileBD);
                file.SaveAs(sPath);

                //-------almacenando la archivo---
                if (File.Exists(sPath))
                {
                    res.ok = true;
                    res.data = "OK";
                    res.totalpage = 0;
                }
                else
                {
                    res.ok = false;
                    res.data = "No se pudo almacenar el archivo en el servidor";
                    res.totalpage = 0;
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
                res.totalpage = 0;
            }
            return res;
        }
        
        [HttpPost]
        [Route("api/Uploads/post_archivoExcel_personal")]
        public object post_archivoExcel_personal(string filtros)
        {
            Resultado res = new Resultado();
            var nombreFile = "";
            string sPath = "";

            try
            {
                //--- obteniendo los parametros que vienen por el FormData

                var file = HttpContext.Current.Request.Files["file"];
                //--- obteniendo los parametros que vienen por el FormData
                string extension = Path.GetExtension(file.FileName);

                string[] parametros = filtros.Split('|');
                int idEmpresa = Convert.ToInt32(parametros[0].ToString());
                int idUsuario = Convert.ToInt32(parametros[1].ToString());

                //nombreFile = "Impotacion_Excels" + "_" + idUsuario + extension;

                //-----generando clave unica---
                var guid = Guid.NewGuid();
                var guidB = guid.ToString("B");
                nombreFile = idUsuario + "_Importacion_Excel_personal" + Guid.Parse(guidB) + extension;

                //-------almacenando la archivo---
                sPath = HttpContext.Current.Server.MapPath("~/Archivos/Excel/" + nombreFile);
                //if (System.IO.File.Exists(sPath))
                //{
                //    System.IO.File.Delete(sPath);
                //}
                file.SaveAs(sPath);

                //-------almacenando la archivo---
                if (File.Exists(sPath))
                {
                    Upload_BL obj_negocio = new Upload_BL();
                    string valor = obj_negocio.setAlmacenandoFile_Excel_personal(sPath, file.FileName, idEmpresa, idUsuario);
                    if (valor == "OK")
                    {
                        res.ok = true;
                        res.data = obj_negocio.get_datosCargados_personal(idUsuario);     
                        res.totalpage = 0;
                    }
                }
                else
                {
                    res.ok = false;
                    res.data = "No se pudo almacenar el archivo en el servidor";
                    res.totalpage = 0;
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
                res.totalpage = 0;
            }
            return res;
        }

        [HttpPost]
        [Route("api/Uploads/post_adjuntarArchivo")]
        public object post_adjuntarArchivo(string filtros)
        {
            Resultado res = new Resultado();
            string nombreFile = "";
            string nombreFileServer = "";
            string path = "";
            string url = ConfigurationManager.AppSettings["imagen"];

            try
            {
                var file = HttpContext.Current.Request.Files["file"];
                string extension = System.IO.Path.GetExtension(file.FileName);

                string[] parametros = filtros.Split('|');
                int idOT = Convert.ToInt32(parametros[0].ToString());
                int idUsuario = Convert.ToInt32(parametros[1].ToString());

                nombreFile = file.FileName;

                //-----generando clave unica---
                var guid = Guid.NewGuid();
                var guidB = guid.ToString("B");
                nombreFileServer = idUsuario + "_AdjuntarArchivo" + Guid.Parse(guidB) + extension;

                //---almacenando la imagen--
                path = System.Web.Hosting.HostingEnvironment.MapPath("~/Imagen/" + nombreFileServer);
                file.SaveAs(path);

                //------suspendemos el hilo, y esperamos ..
                System.Threading.Thread.Sleep(1000);

                if (File.Exists(path))
                {
                    ///----validando que en servidor solo halla una sola foto---
                    tbl_OrdenTrabajo_Cab objectOT;
                    objectOT = db.tbl_OrdenTrabajo_Cab.Where(p => p.id_OT == idOT).FirstOrDefault<tbl_OrdenTrabajo_Cab>();

                    OrdenTrabajo_BL obj_negocio = new OrdenTrabajo_BL();
                    obj_negocio.Set_Actualizar_archivoOT(idOT, nombreFile , nombreFileServer);


                    object respuesta = new
                    {
                        url = url + nombreFileServer,
                        nombreFile = nombreFile
                    };

                    res.ok = true;
                    res.data = respuesta;

                    //---si previamente habia una foto, al reemplazarla borramos la anterior
                    if (objectOT != null)
                    {
                        string nombreFileServidor = (string.IsNullOrEmpty(objectOT.nombreArchivoServidor)) ? "" : objectOT.nombreArchivoServidor;
                        path = System.Web.Hosting.HostingEnvironment.MapPath("~/Imagen/" + nombreFileServidor);

                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }
                }
                else
                {
                    res.ok = false;
                    res.data = "No se pudo guardar el archivo en el servidor..";
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
