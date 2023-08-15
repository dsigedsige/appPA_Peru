using Negocio.Reportes;
using Negocio.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi_3R_Dominion.Controllers.Reporte
{
    [EnableCors("*", "*", "*")]
    public class ReportesController : ApiController
    {
        public object GetAprobarOrdenTrabajo(int opcion, string filtro)
        {
            Resultado res = new Resultado();
            Reportes_BL obj_negocio = new Reportes_BL();
            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    int idServicio = Convert.ToInt32(parametros[0].ToString());
                    string fechaGps = parametros[1].ToString();
                    int idTipoOT = Convert.ToInt32(parametros[2].ToString());
                    int idProveedor = Convert.ToInt32(parametros[3].ToString()); 
                    int idUsuario = Convert.ToInt32(parametros[4].ToString());
                    
                    res.ok = true;
                    res.data = obj_negocio.get_ubicacionesPorPersonal(idServicio, fechaGps, idTipoOT, idProveedor, idUsuario);
                    res.totalpage = 0;

                    resul = res;

                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int idServicio = Convert.ToInt32(parametros[0].ToString());
                    string fechaGps = parametros[1].ToString();
                    int idTipoOT = Convert.ToInt32(parametros[2].ToString());
                    int idProveedor = Convert.ToInt32(parametros[3].ToString());
                    int idUsuario = Convert.ToInt32(parametros[4].ToString());

                    res.ok = true;
                    res.data = obj_negocio.get_eventosCelular(idServicio, fechaGps, idTipoOT, idProveedor, idUsuario);
                    res.totalpage = 0;

                    resul = res;
                }                  //////----REPORTE DETALLE DE OT----
                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');

                    int idServicio = Convert.ToInt32(parametros[0].ToString());
                    int idTipoOT = Convert.ToInt32(parametros[1].ToString());
                    int idProveedor = Convert.ToInt32(parametros[2].ToString());
                    string fechaIni = parametros[3].ToString();
                    string fechaFin = parametros[4].ToString();
                    int idEstado = Convert.ToInt32(parametros[5].ToString());
                    int idUsuario = Convert.ToInt32(parametros[6].ToString());

                    string idServicioMasivo = parametros[7].ToString();
                    string idSubContrataMasivo = parametros[8].ToString();
                    string idEstadoMasivo = parametros[9].ToString();

                    res.ok = true;
                    res.data = obj_negocio.get_detalleOt(idServicio, idTipoOT, idProveedor, fechaIni, fechaFin, idEstado, idUsuario, idServicioMasivo, idSubContrataMasivo, idEstadoMasivo);
                    res.totalpage = 0;

                    resul = res;

                }
                else if (opcion == 4)
                {
                    string[] parametros = filtro.Split('|');

                    int idServicio = Convert.ToInt32(parametros[0].ToString());
                    int idTipoOT = Convert.ToInt32(parametros[1].ToString());
                    int idProveedor = Convert.ToInt32(parametros[2].ToString());
                    string fechaIni = parametros[3].ToString();
                    string fechaFin = parametros[4].ToString();
                    int idEstado = Convert.ToInt32(parametros[5].ToString());
                    int idUsuario = Convert.ToInt32(parametros[6].ToString());

                    string idServicioMasivo = parametros[7].ToString();
                    string idSubContrataMasivo = parametros[8].ToString();
                    string idEstadoMasivo = parametros[9].ToString();


                    resul = obj_negocio.get_descargarDetalleOT_II(idServicio, idTipoOT, idProveedor, fechaIni, fechaFin, idEstado, idUsuario, idServicioMasivo, idSubContrataMasivo, idEstadoMasivo);
                }
                else if (opcion == 5)   /// REPORTE FUERA PLAZO --
                {
                    string[] parametros = filtro.Split('|');

                    int idServicio = Convert.ToInt32(parametros[0].ToString());
                    int idTipoOT = Convert.ToInt32(parametros[1].ToString());
                    int idProveedor = Convert.ToInt32(parametros[2].ToString()); 
                    int idUsuario = Convert.ToInt32(parametros[3].ToString());

                    resul = obj_negocio.get_fueraPlazoOT(idServicio, idTipoOT, idProveedor, idUsuario);
                }
                else if (opcion == 6)
                {
                    string[] parametros = filtro.Split('|');

                    int idServicio = Convert.ToInt32(parametros[0].ToString());
                    int idTipoOT = Convert.ToInt32(parametros[1].ToString());
                    int idProveedor = Convert.ToInt32(parametros[2].ToString());
                    int idUsuario = Convert.ToInt32(parametros[3].ToString());

                    resul = obj_negocio.get_descargarFueraPlazoOT(idServicio, idTipoOT, idProveedor, idUsuario);
                }
                else if (opcion == 7)
                {
                    string[] parametros = filtro.Split('|');
                    int idServicio = Convert.ToInt32(parametros[0].ToString());
                    string fechaGps = parametros[1].ToString();
                    int idTipoOT = Convert.ToInt32(parametros[2].ToString());
                    int idProveedor = Convert.ToInt32(parametros[3].ToString());
                    int idEstado = Convert.ToInt32(parametros[4].ToString());
                    int idUsuario = Convert.ToInt32(parametros[5].ToString());

                    res.ok = true;
                    res.data = obj_negocio.get_ubicacionesOT(idServicio, fechaGps, idTipoOT, idProveedor, idEstado, idUsuario);
                    res.totalpage = 0;

                    resul = res;
                }
                else if (opcion == 8)
                {
                    string[] parametros = filtro.Split('|');

                    int idServicio = Convert.ToInt32(parametros[0].ToString());
                    int idTipoOT = Convert.ToInt32(parametros[1].ToString());
                    int idProveedor = Convert.ToInt32(parametros[2].ToString());
                    string fechaIni = parametros[3].ToString();
                    string fechaFin = parametros[4].ToString();
                    int idEstado = Convert.ToInt32(parametros[5].ToString());
                    int tipoReporte = Convert.ToInt32(parametros[6].ToString());
                    int idUsuario = Convert.ToInt32(parametros[7].ToString());


                    if (tipoReporte == 1)
                    {
                        resul = obj_negocio.get_descargar_roturaVereda(idServicio, idTipoOT, idProveedor, fechaIni, fechaFin, idEstado, idUsuario);
                    }
                    if (tipoReporte == 2)
                    {
                        resul = obj_negocio.get_descargar_reparacionVereda(idServicio, idTipoOT, idProveedor, fechaIni, fechaFin, idEstado, idUsuario);
                    }
                    if (tipoReporte == 3)
                    {
                        resul = obj_negocio.get_descargar_reparacionDesmonte(idServicio, idTipoOT, idProveedor, fechaIni, fechaFin, idEstado, idUsuario);
                    }
                    //if (tipoReporte == 4)
                    //{
                    //    resul = obj_negocio.get_descargar_detalleOT(idServicio, idTipoOT, idProveedor, fechaIni, fechaFin, idEstado, idUsuario);
                    //}
                    if (tipoReporte == 5)
                    {
                        resul = obj_negocio.get_descargar_detalleContratista_New(idServicio, idTipoOT, idProveedor, fechaIni, fechaFin, idEstado, idUsuario);
                    }
                    if (tipoReporte == 6) ///- -filtros
                    {
                        resul = obj_negocio.get_descargar_detalleOT(idServicio, idTipoOT, idProveedor, fechaIni, fechaFin, idEstado, idUsuario);
                    }
                    if (tipoReporte == 7) ///- - data General
                    {
                        resul = obj_negocio.get_descargar_detalleOT_dataGeneral(idServicio, idTipoOT, idProveedor, fechaIni, fechaFin, idEstado, idUsuario);
                    }

                }

                else
                {
                    res.ok = false;
                    res.data = "Opcion seleccionada invalida";
                    res.totalpage = 0;

                    resul = res;
                }
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
