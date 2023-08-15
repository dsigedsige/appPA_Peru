using Entidades.Reportes;
using Negocio.Conexion;
using Negocio.Resultados;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
 
using System.Threading;
using Excel = OfficeOpenXml;
using Style = OfficeOpenXml.Style;


namespace Negocio.Reportes
{
    public class Reportes_BL
    {
        public DataTable get_ubicacionesPorPersonal( int idServicio, string fechaGps, int idTipoOT, int  idProveedor, int idUsuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Reporte_Ubicacion_Personal", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = fechaGps;
                        cmd.Parameters.Add("@Servicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@TipoOrden", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@Proveedor", SqlDbType.Int).Value = idProveedor;
                        cmd.Parameters.Add("@TipoRepor", SqlDbType.VarChar).Value = "W";

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        public DataTable get_eventosCelular(int idServicio, string fechaGps, int idTipoOT, int idProveedor, int idUsuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Reporte_Eventos_Personal", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = fechaGps;
                        cmd.Parameters.Add("@Servicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@TipoOrden", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@Proveedor", SqlDbType.Int).Value = idProveedor; 

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        //------
        // REPORTE DETALLE OT
        ///

        public DataTable get_detalleOt(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario,string idServicioMasivo, string idSubContrataMasivo, string idEstadoMasivo)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_DETALLE_OT_NEW", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicioMasivo", SqlDbType.VarChar).Value = idServicioMasivo;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idSubContrataMasivo", SqlDbType.VarChar).Value = idSubContrataMasivo;
                        cmd.Parameters.Add("@FecIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@FecFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstadoMasivo", SqlDbType.VarChar).Value = idEstadoMasivo;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }


        //public object get_detalleOt(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario)
        //{
        //    Resultado res = new Resultado();
        //    List<detalleOT_E> obj_List = new List<detalleOT_E>();
        //    //DataTable dt_detalle = new DataTable();
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
        //        {
        //            cn.Open();
        //            using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Reporte_Detallado", cn))
        //            {
        //                cmd.CommandTimeout = 0;
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
        //                cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
        //                cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;
        //                cmd.Parameters.Add("@FecIni", SqlDbType.VarChar).Value = fechaIni;
        //                cmd.Parameters.Add("@FecFin", SqlDbType.VarChar).Value = fechaFin;
        //                cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
        //                cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    while (dr.Read())
        //                    {
        //                        detalleOT_E Entidad = new detalleOT_E();

        //                        Entidad.checkeado = false;

        //                        Entidad.id_OT = Convert.ToInt32(dr["id_OT"]);
        //                        Entidad.descripcionEstado = dr["descripcionEstado"].ToString();
        //                        Entidad.tipoOT = dr["tipoOT"].ToString();

        //                        Entidad.nroSed = dr["nroSed"].ToString();
        //                        Entidad.nroSuministro = dr["nroSuministro"].ToString();


        //                        Entidad.nroObra = dr["nroObra"].ToString();
        //                        Entidad.direccion = dr["direccion"].ToString();
        //                        Entidad.distrito = dr["distrito"].ToString();

        //                        Entidad.FechaOrigen = dr["FechaOrigen"].ToString();
        //                        Entidad.FechaAsignacion = dr["FechaAsignacion"].ToString();
        //                        Entidad.FechaMovil = dr["FechaMovil"].ToString();


        //                        Entidad.latitud = dr["latitud"].ToString();
        //                        Entidad.longitud = dr["longitud"].ToString();

        //                        Entidad.empresaContratista = dr["empresaContratista"].ToString();
        //                        Entidad.jefeCuadrilla = dr["jefeCuadrilla"].ToString();

        //                        Entidad.fechaRegistro = dr["fechaRegistro"].ToString();

        //                        Entidad.generaVolumen = dr["generaVolumen"].ToString();
        //                        Entidad.volumenDesmonte = dr["volumenDesmonte"].ToString();
        //                        Entidad.volumenDesmonteRecoger = dr["volumenDesmonteRecoger"].ToString();

        //                        Entidad.fechaAprobacion = dr["fechaAprobacion"].ToString();
        //                        Entidad.fechaCancelacion = dr["fechaCancelacion"].ToString();

        //                        Entidad.totalVolumen = dr["totalVolumen"].ToString();
        //                        Entidad.totalDesmonte = dr["totalDesmonte"].ToString();
        //                        Entidad.totalDesmonteRecoger = dr["totalDesmonteRecoger"].ToString();
        //                        Entidad.diasVencimiento = dr["diasVencimiento"].ToString();


        //                        Entidad.id_tipoTrabajo = Convert.ToInt32(dr["id_tipoTrabajo"]);
        //                        Entidad.id_Distrito = dr["id_Distrito"].ToString();
        //                        Entidad.referencia = dr["referencia"].ToString();
        //                        Entidad.descripcion_OT = dr["descripcion_OT"].ToString();
        //                        Entidad.id_estado = Convert.ToInt32(dr["id_estado"]);
        //                        Entidad.descripcionServicio = dr["descripcionServicio"].ToString();
        //                        Entidad.color = dr["color"].ToString();
        //                        Entidad.viajeIndebido = dr["viajeIndebido"].ToString();
        //                        Entidad.tipoTrabajo_OTOrigen = dr["tipoTrabajo_OTOrigen"].ToString();
        //                        Entidad.jefeCuadrillaOrigen = dr["jefeCuadrillaOrigen"].ToString();
        //                        Entidad.observacion = dr["observacion"].ToString();

        //                        Entidad.usuarioAprobacion = dr["usuarioAprobacion"].ToString();
        //                        Entidad.empresaOrigen = dr["empresaOrigen"].ToString();

        //                        obj_List.Add(Entidad);
        //                    }

        //                    res.ok = true;
        //                    res.data = obj_List;
        //                    res.totalpage = 0;
        //                }


        //                //using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //                //{
        //                //    da.Fill(dt_detalle);

        //                //    res.ok = true;
        //                //    res.data = dt_detalle;
        //                //    res.totalpage = 0;
        //                //}


        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        res.ok = false;
        //        res.data = ex.Message;
        //        res.totalpage = 0;
        //    }
        //    return res;
        //}

        public object get_descargarDetalleOT(int idServicio, int idTipoOT, int idProveedor,string fechaIni, string fechaFin, int idEstado, int idUsuario)
        {
            Resultado res = new Resultado();
            List<detalleOT_E> obj_List = new List<detalleOT_E>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_DETALLE_OT", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;
                        cmd.Parameters.Add("@FecIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@FecFin", SqlDbType.VarChar).Value = fechaFin;

                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                detalleOT_E Entidad = new detalleOT_E();
                                                               
                                Entidad.id_OT = Convert.ToInt32(dr["id_OT"]);
                                Entidad.descripcionEstado = dr["descripcionEstado"].ToString();
                                Entidad.tipoOT = dr["tipoOT"].ToString();

                                Entidad.nroSed = dr["nroSed"].ToString();
                                Entidad.nroSuministro = dr["nroSuministro"].ToString();

                                Entidad.nroObra = dr["nroObra"].ToString();
                                Entidad.direccion = dr["direccion"].ToString();
                                Entidad.distrito = dr["distrito"].ToString();

                                Entidad.FechaOrigen = dr["FechaOrigen"].ToString();
                                Entidad.FechaAsignacion = dr["FechaAsignacion"].ToString();
                                Entidad.FechaMovil = dr["FechaMovil"].ToString();

                                Entidad.empresaContratista = dr["empresaContratista"].ToString();
                                Entidad.jefeCuadrilla = dr["jefeCuadrilla"].ToString();

                                Entidad.fechaRegistro = dr["fechaRegistro"].ToString();                                

                                Entidad.generaVolumen = dr["generaVolumen"].ToString();
                                Entidad.volumenDesmonte = dr["volumenDesmonte"].ToString();
                                Entidad.volumenDesmonteRecoger = dr["volumenDesmonteRecoger"].ToString();

                                Entidad.fechaAprobacion = dr["fechaAprobacion"].ToString();
                                Entidad.fechaCancelacion = dr["fechaCancelacion"].ToString();
 
                                Entidad.totalVolumen = dr["totalVolumen"].ToString();
                                Entidad.totalDesmonte = dr["totalDesmonte"].ToString();
                                Entidad.totalDesmonteRecoger = dr["totalDesmonteRecoger"].ToString(); 
                                Entidad.diasVencimiento = dr["diasVencimiento"].ToString();
                                Entidad.viajeIndebido = dr["viajeIndebido"].ToString();
                                Entidad.jefeCuadrillaOrigen = dr["jefeCuadrillaOrigen"].ToString();
                                Entidad.observacion = dr["observacion"].ToString();


                                Entidad.usuarioAprobacion = dr["usuarioAprobacion"].ToString();
                                Entidad.empresaOrigen = dr["empresaOrigen"].ToString();

                                obj_List.Add(Entidad);
                            }

                            if (obj_List.Count == 0)
                            {
                                res.ok = false;
                                res.data = "No hay informacion disponible";
                                res.totalpage = 0;
                            }
                            else
                            {
                                res.ok = true;
                                 res.data = GenerarArchivoExcel_detalle(obj_List, idUsuario);
                                res.totalpage = 0;
                            }
                        }
                    }
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

        public string GenerarArchivoExcel_detalle(List<detalleOT_E> listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_detalleOT.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["Archivos"];

                FileExcel = rutaServer + "Excel/" + id_usuario + "_detalleOT.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("DetalleOT");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));


                    oWs.Cells[1, 1].Style.Font.Size = 24; //letra tamaño  2
                    oWs.Cells[1, 1].Value = "DETALLE DE ORDENES TRABAJO";
                    oWs.Cells[1, 1, 1, 26].Merge = true;  // combinar celdaS


                    oWs.Cells[3, 1].Value = "USUARIO APROBACION";
                    oWs.Cells[3, 2].Value = "ESTADO";
                    oWs.Cells[3, 3].Value = "TIPO DE ORDEN";

                    oWs.Cells[3, 4].Value = "SED";
                    oWs.Cells[3, 5].Value = "NRO SUMINISTRO";
                    oWs.Cells[3, 6].Value = "NRO OBRA/ TD";

                    oWs.Cells[3, 7].Value = "JEFE CUADRILLA ORIGEN ";
                    oWs.Cells[3, 8].Value = "EMPRESA ORIGEN";

                    oWs.Cells[3, 9].Value = "DIRECCION";
                    oWs.Cells[3, 10].Value = "DISTRITO";

                    oWs.Cells[3, 11].Value = "VIAJE INDEBIDO ";

                    oWs.Cells[3, 12].Value = "FECHA ORIGEN ";
                    oWs.Cells[3, 13].Value = "FECHA ASIGNACION ";
                    oWs.Cells[3, 14].Value = "FECHA ENVIO MOVIL";

                    oWs.Cells[3, 15].Value = "EMPRESA CONTRATISTA";
                    oWs.Cells[3, 16].Value = "JEFE CUADRILLA";
                    oWs.Cells[3, 17].Value = "FECHA DE REGISTRO";

                    oWs.Cells[3, 18].Value = "VEREDA M2";
                    oWs.Cells[3, 19].Value = "DESMONTE RECOGIDO M3";
                    oWs.Cells[3, 20].Value = "DESMONTE GENERADO M3";

                    oWs.Cells[3, 21].Value = "FECHA APROBACION";
                    oWs.Cells[3, 22].Value = "FECHA CANCELACION";

                    oWs.Cells[3, 23].Value = "TOTAL VEREDAS M2 ";
                    oWs.Cells[3, 24].Value = "TOTAL EN DESMONTE RECOGIDO M3 ";
                    oWs.Cells[3, 25].Value = "TOTAL EN DESMONTE GENERADO M3"; 
                    oWs.Cells[3, 26].Value = "DIAS DE VENCIMIENTO";


                    foreach (var item in listDetalle)
                    {
                        oWs.Cells[_fila, 1].Value = item.usuarioAprobacion.ToString();
                        oWs.Cells[_fila, 2].Value = item.descripcionEstado.ToString();
                        oWs.Cells[_fila, 3].Value = item.tipoOT.ToString();

                        oWs.Cells[_fila, 4].Value = item.nroSed.ToString();
                        oWs.Cells[_fila, 5].Value = item.nroSuministro.ToString();

                        oWs.Cells[_fila, 6].Value = item.nroObra.ToString();
                        oWs.Cells[_fila, 7].Value = item.jefeCuadrillaOrigen.ToString();
                        oWs.Cells[_fila, 8].Value = item.empresaOrigen.ToString();

                        oWs.Cells[_fila, 9].Value = item.direccion.ToString();
                        oWs.Cells[_fila, 10].Value = item.distrito.ToString();

                        oWs.Cells[_fila, 11].Value = item.viajeIndebido.ToString();

                        oWs.Cells[_fila, 12].Value = item.FechaOrigen.ToString();
                        oWs.Cells[_fila, 13].Value = item.FechaAsignacion.ToString();
                        oWs.Cells[_fila, 14].Value = item.FechaMovil.ToString();

                        oWs.Cells[_fila, 15].Value = item.empresaContratista.ToString();
                        oWs.Cells[_fila, 16].Value = item.jefeCuadrilla.ToString();

                        oWs.Cells[_fila, 17].Value = item.fechaRegistro.ToString();

                        oWs.Cells[_fila, 18].Value = item.generaVolumen.ToString();
                        oWs.Cells[_fila, 19].Value = item.volumenDesmonte.ToString();
                        oWs.Cells[_fila, 20].Value = item.volumenDesmonteRecoger.ToString();

                        oWs.Cells[_fila, 21].Value = item.fechaAprobacion.ToString();
                        oWs.Cells[_fila, 22].Value = item.fechaCancelacion.ToString();

                        oWs.Cells[_fila, 23].Value = item.totalVolumen.ToString();
                        oWs.Cells[_fila, 23].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;

                        oWs.Cells[_fila, 24].Value = item.totalDesmonte.ToString();
                        oWs.Cells[_fila, 24].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;

                        oWs.Cells[_fila, 25].Value = item.totalDesmonteRecoger.ToString();
                        oWs.Cells[_fila, 25].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
 
                        oWs.Cells[_fila, 26].Value = item.diasVencimiento.ToString();

                        _fila++;
                    }


                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 26; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }


        public object get_descargarDetalleOT_II(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario, string idServicioMasivo, string idSubContrataMasivo, string idEstadoMasivo)
        {
            Resultado res = new Resultado();
            DataTable listaDetallado = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_DETALLE_OT_NEW", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicioMasivo", SqlDbType.VarChar).Value = idServicioMasivo;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idSubContrataMasivo", SqlDbType.VarChar).Value = idSubContrataMasivo;
                        cmd.Parameters.Add("@FecIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@FecFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstadoMasivo", SqlDbType.VarChar).Value = idEstadoMasivo;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(listaDetallado);
                        }

                    }
                }

                if (listaDetallado.Rows.Count <= 0)
                {
                    res.ok = false;
                    res.data = "0|No hay informacion disponible";
                }
                else
                {
                    res.ok = true;
                    res.data = generarArchivoExcel_detalleOT_II(listaDetallado, idUsuario);
                }

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }
                     
        public string generarArchivoExcel_detalleOT_II(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_detalleOT.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_detalleOT.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                int pos = 1;

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("detalleOT");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));


                    oWs.Cells[1, 1].Style.Font.Size = 15; //letra tamaño  2
                    oWs.Cells[1, 1].Value = listDetalle.Rows[0]["tituloReporte"].ToString();
                    oWs.Cells[1, 1, 1, 32].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Cells[3, pos].Value = "SERVICIO"; pos += 1;
                    oWs.Cells[3, pos].Value = "ESTADO"; pos += 1;
                    oWs.Cells[3, pos].Value = "TIPO ORDEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "NRO OBRA TD"; pos += 1;
                    oWs.Cells[3, pos].Value = "SED"; pos += 1;
                    oWs.Cells[3, pos].Value = "NRO SUMINISTRO"; pos += 1;
 
                    oWs.Cells[3, pos].Value = "DIRECCION"; pos += 1;
                    oWs.Cells[3, pos].Value = "DISTRITO"; pos += 1;

                    oWs.Cells[3, pos].Value = "VIAJE INDEBIDO"; pos += 1;
                    oWs.Cells[3, pos].Value = "FECHA ORIGEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "USUARIO APROBACION RECEPCION"; pos += 1;
                    oWs.Cells[3, pos].Value = "FECHA APROBACION"; pos += 1;

                    oWs.Cells[3, pos].Value = "USUARIO APROBACION ORIGEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "FECHA APROBACION ORIGEN"; pos += 1;

                    oWs.Cells[3, pos].Value = "MES"; pos += 1;

                    oWs.Cells[3, pos].Value = "FECHA ASIGNACION"; pos += 1;
                    oWs.Cells[3, pos].Value = "FECHA ENVIOMOVIL"; pos += 1;
                    oWs.Cells[3, pos].Value = "EMPRESA CONTRATISTA"; pos += 1;
                    oWs.Cells[3, pos].Value = "JEFE CUADRILLA"; pos += 1;
                    oWs.Cells[3, pos].Value = "FECHA REGISTRO"; pos += 1;
                    oWs.Cells[3, pos].Value = "VEREDA M2"; pos += 1;
                    oWs.Cells[3, pos].Value = "DESMONTE RECOGIDO M3"; pos += 1;
                    oWs.Cells[3, pos].Value = "DESMONTE GENERADO M3"; pos += 1;

                    oWs.Cells[3, pos].Value = "DESMONTE ORIGEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "JEFE CUADRILLA ORIGEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "EMPRESA ORIGEN"; pos += 1;

                    oWs.Cells[3, pos].Value = "USUARIO APROBACION CANCELACION"; pos += 1;
                    oWs.Cells[3, pos].Value = "FECHA CANCELACION"; pos += 1;
                    oWs.Cells[3, pos].Value = "TOTAL S/. VEREDAS M2"; pos += 1;

                    oWs.Cells[3, pos].Value = "TOTAL S/. EN DESMONTE RECOGIDO M3"; pos += 1;
                    oWs.Cells[3, pos].Value = "TOTAL S/. EN DESMONTE GENERADO M3"; pos += 1;
                    oWs.Cells[3, pos].Value = "FUERA PLAZO"; pos += 1;

                    //oWs.Cells[3, pos].Value = "LARGO"; pos += 1;
                    //oWs.Cells[3, pos].Value = "ANCHO"; pos += 1;
                    //oWs.Cells[3, pos].Value = "ALTURA"; pos += 1;
                    //oWs.Cells[3, pos].Value = "COMENTARIO DEL GESTOR"; pos += 1;
                    //oWs.Cells[3, pos].Value = "ESTATUS"; pos += 1;

                    int ac = 0;
                    pos = 1;
                    foreach (DataRow item in listDetalle.Rows)
                    {
                        pos = 1;
                        oWs.Cells[_fila, pos].Value = item["servicio"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["estado"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["tipoorden"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["nroobratd"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["sed"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["nrosuministro"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["direccion"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["distrito"].ToString(); pos += 1;

                        oWs.Cells[_fila, pos].Value = item["viajeindebido"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechaorigen"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["usuarioaprobacionrecepcion"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechaaprobacion"].ToString(); pos += 1;
                        
                        oWs.Cells[_fila, pos].Value = item["usuarioAprobacionOrigen"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechaAprobacionOrigen"].ToString(); pos += 1;

                        oWs.Cells[_fila, pos].Value = item["mes"].ToString(); pos += 1;

                        oWs.Cells[_fila, pos].Value = item["FechaAsignacion"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechaenviomovil"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["empresacontratista"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["jefecuadrilla"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fecharegistro"].ToString(); pos += 1;

                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value =  Math.Round(Convert.ToDecimal(item["veredam2"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;

                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value =  Math.Round(Convert.ToDecimal(item["desmonterecogidom3"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                     
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["desmontegeneradom3"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;              
                        pos += 1;

                        oWs.Cells[_fila, pos].Value = item["desmonteOrigen"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["jefecuadrillaorigen"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["empresaorigen"].ToString(); pos += 1;

                        oWs.Cells[_fila, pos].Value = item["usuarioaprobacioncancelacion"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechacancelacion"].ToString(); pos += 1;

                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["totalveredasm2"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                       
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["totaldesmonterecogidom3"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;

                    
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["totaldesmontegeneradom3"]), 2);     
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;


                        pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fueraplazo"].ToString(); pos += 1;

                        //oWs.Cells[_fila, pos].Value = item["largo"].ToString(); pos += 1;
                        //oWs.Cells[_fila, pos].Value = item["ancho"].ToString(); pos += 1;
                        //oWs.Cells[_fila, pos].Value = item["altura"].ToString(); pos += 1;
                        //oWs.Cells[_fila, pos].Value = item["comentarioGestor"].ToString(); pos += 1;
                        //oWs.Cells[_fila, pos].Value = item["estatus"].ToString(); pos += 1;

                        _fila++;
                    }
                    
                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 32; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }




        public object get_fueraPlazoOT(int idServicio, int idTipoOT, int idProveedor, int idUsuario)
        {
            Resultado res = new Resultado();
            List<fueraPlazoOT_E> obj_List = new List<fueraPlazoOT_E>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Reporte_FueraPlazo", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                fueraPlazoOT_E Entidad = new fueraPlazoOT_E();
                                                               
                                Entidad.id_OT = Convert.ToInt32(dr["id_OT"]);
                                Entidad.descripcionEstado = dr["descripcionEstado"].ToString();
                                Entidad.tipoOT = dr["tipoOT"].ToString();
                                Entidad.nroObra = dr["nroObra"].ToString();
                                Entidad.direccion = dr["direccion"].ToString();
                                Entidad.distrito = dr["distrito"].ToString();

                                Entidad.latitud = dr["latitud"].ToString();
                                Entidad.longitud = dr["longitud"].ToString();

                                Entidad.FechaAsignacion = dr["FechaAsignacion"].ToString();
                                Entidad.FechaMovil = dr["FechaMovil"].ToString();

                                Entidad.empresaContratista = dr["empresaContratista"].ToString();
                                Entidad.jefeCuadrilla = dr["jefeCuadrilla"].ToString();

                                Entidad.fueraPlazoHoras = dr["fueraPlazoHoras"].ToString();
                                Entidad.fueraPlazoDias = dr["fueraPlazoDias"].ToString();

                                Entidad.id_tipoTrabajo = Convert.ToInt32(dr["id_tipoTrabajo"]);
                                Entidad.id_Distrito = dr["id_Distrito"].ToString();
                                Entidad.referencia = dr["referencia"].ToString();
                                Entidad.descripcion_OT = dr["descripcion_OT"].ToString();
                                Entidad.id_estado = Convert.ToInt32(dr["id_estado"]);

                                obj_List.Add(Entidad);
                            }

                            res.ok = true;
                            res.data = obj_List;
                            res.totalpage = 0;
                        }
                    }
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
        
        public object get_descargarFueraPlazoOT(int idServicio, int idTipoOT, int idProveedor, int idUsuario)
        {
            Resultado res = new Resultado();
            List<fueraPlazoOT_E> obj_List = new List<fueraPlazoOT_E>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Reporte_FueraPlazo", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                fueraPlazoOT_E Entidad = new fueraPlazoOT_E();

                                Entidad.id_OT = Convert.ToInt32(dr["id_OT"]);
                                Entidad.descripcionEstado = dr["descripcionEstado"].ToString();
                                Entidad.tipoOT = dr["tipoOT"].ToString();
                                Entidad.nroObra = dr["nroObra"].ToString();
                                Entidad.direccion = dr["direccion"].ToString();
                                Entidad.distrito = dr["distrito"].ToString();

                                Entidad.FechaAsignacion = dr["FechaAsignacion"].ToString();
                                Entidad.FechaMovil = dr["FechaMovil"].ToString();
                                Entidad.empresaContratista = dr["empresaContratista"].ToString();
                                Entidad.jefeCuadrilla = dr["jefeCuadrilla"].ToString();

                                Entidad.fueraPlazoHoras = dr["fueraPlazoHoras"].ToString();
                                Entidad.fueraPlazoDias = dr["fueraPlazoDias"].ToString();

                                obj_List.Add(Entidad);
                            }

                            if (obj_List.Count == 0)
                            {
                                res.ok = false;
                                res.data = "No hay informacion disponible";
                                res.totalpage = 0;
                            }
                            else
                            {
                                res.ok = true;
                                res.data = GenerarArchivoExcel_fueraPlazoOT(obj_List, idUsuario);
                                res.totalpage = 0;
                            }
                        }
                    }
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

        public string GenerarArchivoExcel_fueraPlazoOT(List<fueraPlazoOT_E> listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {
                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_fueraPlazoOT.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["Archivos"];

                FileExcel = rutaServer + "Excel/" + id_usuario + "_fueraPlazoOT.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("OTFueraPlazo");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));

                    oWs.Cells[1, 1].Style.Font.Size = 24; //letra tamaño  2
                    oWs.Cells[1, 1].Value = "ORDEN DE TRABAJO FUERA DE PLAZO";
                    oWs.Cells[1, 1, 1, 11].Merge = true;  // combinar celdaS


                    oWs.Cells[3, 1].Value = "ESTADO";
                    oWs.Cells[3, 2].Value = "TIPO DE ORDEN";
                    oWs.Cells[3, 3].Value = "NRO OBRA/ TD";
                    oWs.Cells[3, 4].Value = "DIRECCION";
                    oWs.Cells[3, 5].Value = "DISTRITO";

                    oWs.Cells[3, 6].Value = "FECHA ASIGNACION ";
                    oWs.Cells[3, 7].Value = "FECHA ENVIO MOVIL";

                    oWs.Cells[3, 8].Value = "EMPRESA CONTRATISTA";
                    oWs.Cells[3, 9].Value = "JEFE CUADRILLA";
                    oWs.Cells[3, 10].Value = "FUERA DE PLAZO EN HORAS";
                    oWs.Cells[3, 11].Value = "FUERA DE PLAZO EN DIAS";
 


                    foreach (var item in listDetalle)
                    {
                        oWs.Cells[_fila, 1].Value = item.descripcionEstado.ToString();
                        oWs.Cells[_fila, 2].Value = item.tipoOT.ToString();
                        oWs.Cells[_fila, 3].Value = item.nroObra.ToString();
                        oWs.Cells[_fila, 4].Value = item.direccion.ToString();
                        oWs.Cells[_fila, 5].Value = item.distrito.ToString();

                        oWs.Cells[_fila, 6].Value = item.FechaAsignacion.ToString();
                        oWs.Cells[_fila, 7].Value = item.FechaMovil.ToString();

                        oWs.Cells[_fila, 8].Value = item.empresaContratista.ToString();
                        oWs.Cells[_fila, 9].Value = item.jefeCuadrilla.ToString();

                        oWs.Cells[_fila, 10].Value = item.fueraPlazoHoras.ToString();
                        oWs.Cells[_fila, 11].Value = item.fueraPlazoDias.ToString();
                        _fila++;
                    }


                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 11; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }

        public DataTable get_ubicacionesOT(int idServicio, string fechaGps, int idTipoOT, int idProveedor, int idEstado, int idUsuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_UBICACION_OT", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = fechaGps;
                        cmd.Parameters.Add("@Servicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@TipoOrden", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@Proveedor", SqlDbType.Int).Value = idProveedor;
                        cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = idEstado;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }


        public void funcionGlobal_centrarNegrita_Fila(Excel.ExcelWorksheet oWs, int[] fil)
        {
            for (int i = 0; i < fil.Length; i++)
            {
                oWs.Row(fil[i]).Style.Font.Bold = true;
                oWs.Row(fil[i]).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                oWs.Row(fil[i]).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;
            }
        }


        public void funcionGlobal_bordes_fila_columna(Excel.ExcelWorksheet oWs, int[] fil, int[] col)
        {
            for (int x = 0; x < fil.Length; x++)
            {
                for (int y = 0; y < col.Length; y++)
                {
                    oWs.Cells[fil[x], col[y]].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                }
            }
        }


        public void funcionGlobal_ajustarAnchoAutomatica_columna(Excel.ExcelWorksheet oWs, int[] col)
        {
            for (int y = 0; y < col.Length; y++)
            {
                oWs.Column(col[y]).AutoFit();
            }
        }

        public object get_descargar_roturaVereda(int idServicio, int idTipoOT, int idProveedor, string fechaIni,string  fechaFin, int idEstado, int  idUsuario)
        {
            Resultado res = new Resultado();
            DataTable listaDetallado = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_ANALISIS_ROTURA_VEREDA", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;

                        cmd.Parameters.Add("@fechaIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(listaDetallado);
                        }

                    }
                }

                if (listaDetallado.Rows.Count <= 0)
                {
                    res.ok = false;
                    res.data = "0|No hay informacion disponible";
                }
                else
                {
                    res.ok = true;
                    res.data = generarArchivoExcel_roturaVereda(listaDetallado, idUsuario);
                }

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

        public string generarArchivoExcel_roturaVereda(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_roturaVereda.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_roturaVereda.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("roturaVereda");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));


                    oWs.Cells[1, 1].Style.Font.Size = 15; //letra tamaño  2
                    oWs.Cells[1, 1].Value = listDetalle.Rows[0]["tituloReporte"].ToString();
                    oWs.Cells[1, 1, 1, 13].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Cells[3, 1].Value = "TIPO DE TRABAJO";
                    oWs.Cells[3, 2].Value = "N° DE ORDEN";
                    oWs.Cells[3, 3].Value = "SUMINISTRO";
                    oWs.Cells[3, 4].Value = "SED";
                    oWs.Cells[3, 5].Value = "FECHA EJECUSION ELECTRICA";

                    oWs.Cells[3, 6].Value = " TECNICO RESPONSABLE";
                    oWs.Cells[3, 7].Value = "DIRECCION ";
                    oWs.Cells[3, 8].Value = "DISTRITO";
                    oWs.Cells[3, 9].Value = "ZONA";
                    oWs.Cells[3, 10].Value = "VEREDAS m2";

                    oWs.Cells[3, 11].Value = "PROVEEDOR ASIGNADO ";
                    oWs.Cells[3, 12].Value = "M3 - REPORTADO - DESMONTE";
                    oWs.Cells[3, 13].Value = "OBSERVACIONES  ";
 

                    int ac = 0;
                    foreach (DataRow item in listDetalle.Rows)
                    {
                        ac += 1;

                        oWs.Cells[_fila, 1].Value = item["tipoTrabajo"].ToString();
                        oWs.Cells[_fila, 2].Value = item["nroOrden"].ToString();
                        oWs.Cells[_fila, 3].Value = item["suministro"].ToString();

                        oWs.Cells[_fila, 4].Value = item["sed"].ToString();
                        oWs.Cells[_fila, 5].Value = item["fechaEjecucion"].ToString();
                        oWs.Cells[_fila, 6].Value = item["tecnicoResponsable"].ToString();
                        oWs.Cells[_fila, 7].Value = item["direccion"].ToString();

                        oWs.Cells[_fila, 8].Value = item["distrito"].ToString();
                        oWs.Cells[_fila, 9].Value = item["zona"].ToString();
                        oWs.Cells[_fila, 10].Value = item["veredas"].ToString();
                        oWs.Cells[_fila, 11].Value = item["proveedorAsignado"].ToString();
                        oWs.Cells[_fila, 12].Value = item["m3reportado"].ToString();
                        oWs.Cells[_fila, 13].Value = item["observaciones"].ToString();

                        _fila++;
                    }

                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 13; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }
        
        public object get_descargar_reparacionVereda(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario)
        {
            Resultado res = new Resultado();
            DataTable listaDetallado = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_ANALISIS_REPARACION_VEREDA_II", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;

                        cmd.Parameters.Add("@fechaIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(listaDetallado);
                        }

                    }
                }

                if (listaDetallado.Rows.Count <= 0)
                {
                    res.ok = false;
                    res.data = "0|No hay informacion disponible";
                }
                else
                {
                    res.ok = true;
                    res.data = generarArchivoExcel_reparacionVereda_II(listaDetallado, idUsuario);
                }

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }


        public string generarArchivoExcel_reparacionVereda_II(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_reparacionVereda.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_reparacionVereda.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);
                int pos = 1;


                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("reparacionVereda");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));


                    oWs.Cells[1, 1].Style.Font.Size = 15; //letra tamaño  2
                    oWs.Cells[1, 1].Value = listDetalle.Rows[0]["tituloReporte"].ToString();
                    oWs.Cells[1, 1, 1, 41].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Cells[3, pos].Value = "SERVICIO"; pos += 1;
                    oWs.Cells[3, pos].Value = "ESTADO"; pos += 1;
                    oWs.Cells[3, pos].Value = "TIPO ORDEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "NRO ORDEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "NRO SUMINISTRO"; pos += 1;
                    oWs.Cells[3, pos].Value = "SED"; pos += 1;
                    oWs.Cells[3, pos].Value = "JEFE CUADRILLA ORIGEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "DIRECCION"; pos += 1;
                    oWs.Cells[3, pos].Value = "DISTRITO"; pos += 1;
                    oWs.Cells[3, pos].Value = "VIAJE INDEBIDO"; pos += 1;

                    oWs.Cells[3, pos].Value = "FECHA TRABAJO ORIGEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "VEREDAS M2 ORIGEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "ADOQUINO RIGEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "GRASS ORIGEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "ASFALTO ORIGEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "MAYOLICA ORIGEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "PISTA CONCRETO ORIGEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "PISO ESPECIAL ORIGEN"; pos += 1;
                    oWs.Cells[3, pos].Value = "USUARIOAPROBACIONRECEPCION"; pos += 1;
                    oWs.Cells[3, pos].Value = "FECHA ASIGNACION"; pos += 1;

                    oWs.Cells[3, pos].Value = "EMPRESA CONTRATISTA"; pos += 1;
                    oWs.Cells[3, pos].Value = "FECHA ENVIOMOVIL"; pos += 1;
                    oWs.Cells[3, pos].Value = "JEFE CUADRILLA"; pos += 1;
                    oWs.Cells[3, pos].Value = "FECHA REGISTRO"; pos += 1;
                    oWs.Cells[3, pos].Value = "VEREDAS M2"; pos += 1;
                    oWs.Cells[3, pos].Value = "ADOQUIN"; pos += 1;
                    oWs.Cells[3, pos].Value = "GRASS"; pos += 1;
                    oWs.Cells[3, pos].Value = "ASFALTO"; pos += 1;
                    oWs.Cells[3, pos].Value = "MAYOLICA"; pos += 1;
                    oWs.Cells[3, pos].Value = "PISTA CONCRETO"; pos += 1;

                    oWs.Cells[3, pos].Value = "PISO ESPECIAL"; pos += 1;
                    oWs.Cells[3, pos].Value = "DESMONTE RECOGID OM3"; pos += 1;
                    oWs.Cells[3, pos].Value = "M3 DESMONTE GENERADO"; pos += 1;
                    oWs.Cells[3, pos].Value = "EMPRESA RECOGO DESMONTE"; pos += 1;
                    oWs.Cells[3, pos].Value = "M3 RECOGIDO"; pos += 1;
                    oWs.Cells[3, pos].Value = "USUARIO APROBACION CANCELACION"; pos += 1;
                    oWs.Cells[3, pos].Value = "FECHA CANCELACION"; pos += 1;
                    oWs.Cells[3, pos].Value = "TOTAL VEREDA SM2"; pos += 1;
                    oWs.Cells[3, pos].Value = "TOTAL DESMONTE RECOGID OM3"; pos += 1;
                    oWs.Cells[3, pos].Value = "TOTAL EN DESMONTE GENERAD OM3"; pos += 1;
                    oWs.Cells[3, pos].Value = "FUERA PLAZO"; pos += 1;
                                                         
                    int ac = 0;
                    foreach (DataRow item in listDetalle.Rows)
                    {

                        pos = 1;
                        oWs.Cells[_fila, pos].Value = item["servicio"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["estado"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["tipoorden"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["nroorden"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["nrosuministro"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["sed"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["jefecuadrillaorigen"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["direccion"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["distrito"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["viajeindebido"].ToString(); pos += 1;

                        oWs.Cells[_fila, pos].Value = item["fechatrabajoorigen"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["veredasm2origen"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["adoquinorigen"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["grassorigen"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["asfaltoorigen"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["mayolicaorigen"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["pistaconcretoorigen"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["pisoespecialorigen"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["usuarioaprobacionrecepcion"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechaasignacion"].ToString(); pos += 1;

                        oWs.Cells[_fila, pos].Value = item["empresacontratista"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechaenviomovil"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["jefecuadrilla"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fecharegistro"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["veredasm2"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["adoquin"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["grass"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["asfalto"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["mayolica"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["pistaconcreto"].ToString(); pos += 1;

                        oWs.Cells[_fila, pos].Value = item["pisoespecial"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["desmonterecogidom3"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["m3desmontegenerado"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["empresarecogodesmonte"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["m3recogido"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["usuarioaprobacioncancelacion"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechacancelacion"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["totalveredasm2"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["totaldesmonterecogidom3"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["totalendesmontegeneradom3"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fueraplazo"].ToString(); pos += 1;

                        _fila++;
                    }


                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 41; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }



        public string generarArchivoExcel_reparacionVereda(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_reparacionVereda.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_reparacionVereda.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("reparacionVereda");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));


                    oWs.Cells[1, 1].Style.Font.Size = 15; //letra tamaño  2
                    oWs.Cells[1, 1].Value = listDetalle.Rows[0]["tituloReporte"].ToString();
                    oWs.Cells[1, 1, 1, 19].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Cells[3, 1].Value = "NRO ORDEN ";
                    oWs.Cells[3, 2].Value = "FECHA TRABAJO ORIGEN ";
                    oWs.Cells[3, 3].Value = "JEFE CUADRILLA ORIGEN ";
                    oWs.Cells[3, 4].Value = "DISTRITO ";

                    oWs.Cells[3, 5].Value = "PROVEEDOR ";
                    oWs.Cells[3, 6].Value = "FECHA ASIGNACION PROVEEDOR)";
                    oWs.Cells[3, 7].Value = "ESTADO DE VEREDA";
                    oWs.Cells[3, 8].Value = "FECHA EJECUCION VEREDA";
                    oWs.Cells[3, 9].Value = "VEREDAS M2";

                    oWs.Cells[3, 10].Value = "ADOQUIN  ";
                    oWs.Cells[3, 11].Value = "GRASS";
                    oWs.Cells[3, 12].Value = "ASFALTO";
                    oWs.Cells[3, 13].Value = "MAYOLICA";
                    oWs.Cells[3, 14].Value = "PISTA CONCRETO";
                    oWs.Cells[3, 15].Value = "PISO ESPECIAL ";
                    oWs.Cells[3, 16].Value = "M3 - REPORTADO - DESMONTE";

                    oWs.Cells[3, 17].Value = "M3 RECOGIDO ";
                    oWs.Cells[3, 18].Value = "M3 DESMONTE GENERADO ";
                    oWs.Cells[3, 19].Value = "EMPRESA RECOGO DESMONTE ";



                    int ac = 0;
                    foreach (DataRow item in listDetalle.Rows)
                    {

                        oWs.Cells[_fila, 1].Value = item["nroOrden"].ToString();

                        oWs.Cells[_fila, 2].Value = item["fechaTrabajoOrigen"].ToString();
                        oWs.Cells[_fila, 3].Value = item["jefeCuadrillaOrigen"].ToString();
                        oWs.Cells[_fila, 4].Value = item["distrito"].ToString();

                        oWs.Cells[_fila, 5].Value = item["proveedor"].ToString();
                        oWs.Cells[_fila, 6].Value = item["fechaAsignacion"].ToString();
                        oWs.Cells[_fila, 7].Value = item["estadoVereda"].ToString();
                        oWs.Cells[_fila, 8].Value = item["fechaEjecucion"].ToString();
                        oWs.Cells[_fila, 9].Value = item["veredasm2"].ToString();
                        oWs.Cells[_fila, 10].Value = item["adoquin"].ToString();

                        oWs.Cells[_fila, 11].Value = item["grass"].ToString();
                        oWs.Cells[_fila, 12].Value = item["asfalto"].ToString();
                        oWs.Cells[_fila, 13].Value = item["mayolica"].ToString();
                        oWs.Cells[_fila, 14].Value = item["pistaConcreto"].ToString();
                        oWs.Cells[_fila, 15].Value = item["pisoEspecial"].ToString();
                        oWs.Cells[_fila, 16].Value = item["m3reportado"].ToString();

                        oWs.Cells[_fila, 17].Value = item["m3Recogido"].ToString();
                        oWs.Cells[_fila, 18].Value = item["m3DesmonteGenerado"].ToString();
                        oWs.Cells[_fila, 19].Value = item["empresaRecojoDesmonte"].ToString();

                        _fila++;
                    }


                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 19; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }
        
        public object get_descargar_reparacionDesmonte(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario)
        {
            Resultado res = new Resultado();
            DataTable listaDetallado = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_ANALISIS_DESMONTE", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;

                        cmd.Parameters.Add("@fechaIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(listaDetallado);
                        }

                    }
                }

                if (listaDetallado.Rows.Count <= 0)
                {
                    res.ok = false;
                    res.data = "0|No hay informacion disponible";
                }
                else
                {
                    res.ok = true;
                    res.data = generarArchivoExcel_reparacionDesmonte(listaDetallado, idUsuario);
                }

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

        public string generarArchivoExcel_reparacionDesmonte(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_reparacionDesmonte.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_reparacionDesmonte.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("reparacionDesmonte");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));
                    
                    oWs.Cells[1, 1].Style.Font.Size = 15; //letra tamaño  2
                    oWs.Cells[1, 1].Value = listDetalle.Rows[0]["tituloReporte"].ToString();
                    oWs.Cells[1, 1, 1, 6].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Cells[3, 1].Value = "NRO ORDEN";
                    oWs.Cells[3, 2].Value = "PROVEEDOR";
                    oWs.Cells[3, 3].Value = "F. ASIGNACION";
                    oWs.Cells[3, 4].Value = "M3 - ENCONTRADO PARTE ELEC.";
                    oWs.Cells[3, 5].Value = "M3 - ENCONTRADO REPARACION DE VEREDA";
                    oWs.Cells[3, 6].Value = "F.RECOJO";

                    int ac = 0;
                    foreach (DataRow item in listDetalle.Rows)
                    {
                        ac += 1;

                        oWs.Cells[_fila, 1].Value = item["nroOrden"].ToString();
                        oWs.Cells[_fila, 2].Value = item["proveedor"].ToString();
                        oWs.Cells[_fila, 3].Value = item["fechaAsignacion"].ToString();
                        oWs.Cells[_fila, 4].Value = item["m3EncontradoElectrico"].ToString();
                        oWs.Cells[_fila, 5].Value = item["m3EncontradoReparacionVereda"].ToString();
                        oWs.Cells[_fila, 6].Value = item["fechaRecojo"].ToString();

                        _fila++;
                    }


                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 6; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }
        
        public object get_descargar_detalleOT(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario)
        {
            Resultado res = new Resultado();
            DataTable listaDetallado = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_ANALISIS_DETALLADO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;

                        cmd.Parameters.Add("@fechaIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(listaDetallado);
                        }

                    }
                }

                if (listaDetallado.Rows.Count <= 0)
                {
                    res.ok = false;
                    res.data = "0|No hay informacion disponible";
                }
                else
                {
                    res.ok = true;
                    res.data = generarArchivoExcel_detalleOT(listaDetallado, idUsuario);
                }

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

        public string generarArchivoExcel_detalleOT(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_detalleOT.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_detalleOT.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("detalleOT");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));

                    oWs.Cells[1, 1].Style.Font.Size = 15; //letra tamaño  2
                    oWs.Cells[1, 1].Value = listDetalle.Rows[0]["tituloReporte"].ToString();
                    oWs.Cells[1, 1, 1, 35].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Cells[3, 1].Value = "SERVICIO";
                    oWs.Cells[3, 2].Value = "TIPO DE ORDEN";
                    oWs.Cells[3, 3].Value = "NRO ORDEN ";
                    oWs.Cells[3, 4].Value = "SUMINISTRO";
                    oWs.Cells[3, 5].Value = "NRO SED";

                    oWs.Cells[3, 6].Value = "DIRECCION";
                    oWs.Cells[3, 7].Value = "DISTRITO";
                    oWs.Cells[3, 8].Value = "PROVEEDOR ";
                    oWs.Cells[3, 9].Value = "FECHA ASIGNACION PROVEEDOR";
                    oWs.Cells[3, 10].Value = "JEFE DE CUADRILLA";

                    oWs.Cells[3, 11].Value = "TIPO DE TRABAJO";
                    oWs.Cells[3, 12].Value = "TIPO DE MATERIAL";
                    oWs.Cells[3, 13].Value = "LARGO";
                    oWs.Cells[3, 14].Value = "ANCHO";
                    oWs.Cells[3, 15].Value = "ESPESOR";

                    oWs.Cells[3, 16].Value = "FACTOR MULTIPLO";
                    oWs.Cells[3, 17].Value = "TOTAL";
                    oWs.Cells[3, 18].Value = "PRECIO";
                    oWs.Cells[3, 19].Value = "TOTAL IMPORTE";


                    oWs.Cells[3, 20].Value = "CANT PAÑOS";
                    oWs.Cells[3, 21].Value = "MEDIDA HORIZONTAL";
                    oWs.Cells[3, 22].Value = "MEDIDA VERTICAL";

                    oWs.Cells[3, 23].Value = "FECHA EJECUCION VEREDA";
                    oWs.Cells[3, 24].Value = "ESTADO";

                    oWs.Cells[3, 25].Value = "OBSERVACION";

                    oWs.Cells[3, 26].Value = "ORDEN DE ORIGEN";

                    oWs.Cells[3, 27].Value = "EMPRESA ORIGEN";
                    oWs.Cells[3, 28].Value = "TIPO MATERIAL ORIGEN";
                    oWs.Cells[3, 29].Value = "LARGO ORIGEN";
                    oWs.Cells[3, 30].Value = "ANCHO ORIGEN";
                    oWs.Cells[3, 31].Value = "ESPESOR ORIGEN";
                    oWs.Cells[3, 32].Value = "TOTAL ORIGEN";

                    oWs.Cells[3, 33].Value = "CANT PAÑOS ORIGEN ";
                    oWs.Cells[3, 34].Value = "MEDIDA HORIZONTAL ORIGEN";
                    oWs.Cells[3, 35].Value = "MEDIDA VERTICAL ORIGEN";


                    int ac = 0;
                    foreach (DataRow item in listDetalle.Rows)
                    {
                        ac += 1;


                        oWs.Cells[_fila, 1].Value = item["Servicio"].ToString();
                        oWs.Cells[_fila, 2].Value = item["TipoOrden"].ToString();
                        oWs.Cells[_fila, 3].Value = item["NroOrden"].ToString();
                        oWs.Cells[_fila, 4].Value = item["Suminstro"].ToString();
                        oWs.Cells[_fila, 5].Value = item["NroSed"].ToString();

                        oWs.Cells[_fila, 6].Value = item["Direccion"].ToString();
                        oWs.Cells[_fila, 7].Value = item["Distrito"].ToString();
                        oWs.Cells[_fila, 8].Value = item["Proovedor"].ToString();
                        oWs.Cells[_fila, 9].Value = item["FechaAsignacionProovedor"].ToString();
                        oWs.Cells[_fila, 10].Value = item["JefeCuadrilla"].ToString();

                        oWs.Cells[_fila, 11].Value = item["TipoTrabajo"].ToString();
                        oWs.Cells[_fila, 12].Value = item["TipoMaterial"].ToString();
                        oWs.Cells[_fila, 13].Value = item["Largo"].ToString();
                        oWs.Cells[_fila, 13].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 14].Value = item["Ancho"].ToString();
                        oWs.Cells[_fila, 14].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 15].Value = item["Espesor"].ToString();
                        oWs.Cells[_fila, 15].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 16].Value = item["FactorMultiplo"].ToString();
                        oWs.Cells[_fila, 16].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 17].Value = item["Total"].ToString();
                        oWs.Cells[_fila, 17].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 18].Value = item["Precio"].ToString();
                        oWs.Cells[_fila, 18].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 19].Value = item["TotalImporte"].ToString();
                        oWs.Cells[_fila, 19].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 20].Value = item["CantPanios"].ToString();
                        oWs.Cells[_fila, 21].Value = item["MedidaHorizontal"].ToString();
                        oWs.Cells[_fila, 22].Value = item["MedidaVertical"].ToString();


                        oWs.Cells[_fila, 23].Value = item["FechaEjecucionVereda"].ToString();
                        oWs.Cells[_fila, 24].Value = item["Estado"].ToString();

                        oWs.Cells[_fila, 25].Value = item["observacion"].ToString();


                        oWs.Cells[_fila, 26].Value = item["OrdenOrigen"].ToString();

                        oWs.Cells[_fila, 27].Value = item["EmpresaOrigen"].ToString();
                        oWs.Cells[_fila, 28].Value = item["TipoMaterialOrigen"].ToString();

                        oWs.Cells[_fila, 29].Value = item["LargoOrigen"].ToString();
                        oWs.Cells[_fila, 29].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 30].Value = item["AnchoOrigen"].ToString();
                        oWs.Cells[_fila, 30].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 31].Value = item["EspesorOrigen"].ToString();
                        oWs.Cells[_fila, 31].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 32].Value = item["TotalOrigen"].ToString();
                        oWs.Cells[_fila, 32].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 33].Value = item["CantPaniosOrigen"].ToString();
                        oWs.Cells[_fila, 34].Value = item["MedidaHorizontalOrigen"].ToString();
                        oWs.Cells[_fila, 35].Value = item["MedidaVerticalOrigen"].ToString();

                        _fila++;
                    }


                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 35 ; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }
        
        public object get_descargar_detalleContratista(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario)
        {
            Resultado res = new Resultado();
            DataTable listaDetallado = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_ANALISIS_DETALLADO_PROVEEDOR", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;

                        cmd.Parameters.Add("@fechaIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(listaDetallado);
                        }

                    }
                }

                if (listaDetallado.Rows.Count <= 0)
                {
                    res.ok = false;
                    res.data = "0|No hay informacion disponible";
                }
                else
                {
                    res.ok = true;
                    res.data = generarArchivoExcel_detalleContratista(listaDetallado, idUsuario);
                }

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

        public string generarArchivoExcel_detalleContratista(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_detalleContratista.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_detalleContratista.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("detalleContratista");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));

                    oWs.Cells[1, 1].Style.Font.Size = 15; //letra tamaño  2
                    oWs.Cells[1, 1].Value = listDetalle.Rows[0]["tituloReporte"].ToString();
                    oWs.Cells[1, 1, 1, 36].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Cells[3, 1].Value = "SERVICIO";
                    oWs.Cells[3, 2].Value = "TIPO DE ORDEN";
                    oWs.Cells[3, 3].Value = "NRO ORDEN ";
                    oWs.Cells[3, 4].Value = "SUMINISTRO";
                    oWs.Cells[3, 5].Value = "NRO SED";

                    oWs.Cells[3, 6].Value = "DIRECCION";
                    oWs.Cells[3, 7].Value = "DISTRITO";
                    oWs.Cells[3, 8].Value = "PROVEEDOR ";
                    oWs.Cells[3, 9].Value = "FECHA ASIGNACION PROVEEDOR";
                    oWs.Cells[3, 10].Value = "JEFE DE CUADRILLA";

                    oWs.Cells[3, 11].Value = "TIPO DE TRABAJO";
                    oWs.Cells[3, 12].Value = "TIPO DE MATERIAL";
                    oWs.Cells[3, 13].Value = "LARGO";
                    oWs.Cells[3, 14].Value = "ANCHO";
                    oWs.Cells[3, 15].Value = "ESPESOR";

                    oWs.Cells[3, 16].Value = "FACTOR MULTIPLO";
                    oWs.Cells[3, 17].Value = "TOTAL";
                    oWs.Cells[3, 18].Value = "PRECIO";
                    oWs.Cells[3, 19].Value = "TOTAL IMPORTE";


                    oWs.Cells[3, 20].Value = "CANT PAÑOS";
                    oWs.Cells[3, 21].Value = "MEDIDA HORIZONTAL";
                    oWs.Cells[3, 22].Value = "MEDIDA VERTICAL";

                    oWs.Cells[3, 23].Value = "FECHA EJECUCION VEREDA";
                    oWs.Cells[3, 24].Value = "ESTADO";
                    oWs.Cells[3, 25].Value = "OBSERVACION";

                    oWs.Cells[3, 26].Value = "ORDEN DE ORIGEN";

                    oWs.Cells[3, 27].Value = "EMPRESA ORIGEN";
                    oWs.Cells[3, 28].Value = "TIPO MATERIAL ORIGEN";
                    oWs.Cells[3, 29].Value = "LARGO ORIGEN";
                    oWs.Cells[3, 30].Value = "ANCHO ORIGEN";
                    oWs.Cells[3, 31].Value = "ESPESOR ORIGEN";
                    oWs.Cells[3, 32].Value = "TOTAL ORIGEN";

                    oWs.Cells[3, 33].Value = "CANT PAÑOS ORIGEN ";
                    oWs.Cells[3, 34].Value = "MEDIDA HORIZONTAL ORIGEN";
                    oWs.Cells[3, 35].Value = "MEDIDA VERTICAL ORIGEN";
                    oWs.Cells[3, 36].Value = "FECHA ORIGEN";




                    int ac = 0;
                    foreach (DataRow item in listDetalle.Rows)
                    {
                        ac += 1;

                        oWs.Cells[_fila, 1].Value = item["Servicio"].ToString();
                        oWs.Cells[_fila, 2].Value = item["TipoOrden"].ToString();
                        oWs.Cells[_fila, 3].Value = item["NroOrden"].ToString();
                        oWs.Cells[_fila, 4].Value = item["Suminstro"].ToString();
                        oWs.Cells[_fila, 5].Value = item["NroSed"].ToString();

                        oWs.Cells[_fila, 6].Value = item["Direccion"].ToString();
                        oWs.Cells[_fila, 7].Value = item["Distrito"].ToString();
                        oWs.Cells[_fila, 8].Value = item["Proovedor"].ToString();
                        oWs.Cells[_fila, 9].Value = item["FechaAsignacionProovedor"].ToString();
                        oWs.Cells[_fila, 10].Value = item["JefeCuadrilla"].ToString();

                        oWs.Cells[_fila, 11].Value = item["TipoTrabajo"].ToString();
                        oWs.Cells[_fila, 12].Value = item["TipoMaterial"].ToString();
                        oWs.Cells[_fila, 13].Value = item["Largo"].ToString();
                        oWs.Cells[_fila, 13].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 14].Value = item["Ancho"].ToString();
                        oWs.Cells[_fila, 14].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 15].Value = item["Espesor"].ToString();
                        oWs.Cells[_fila, 15].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 16].Value = item["FactorMultiplo"].ToString();
                        oWs.Cells[_fila, 16].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 17].Value = item["Total"].ToString();
                        oWs.Cells[_fila, 17].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 18].Value = item["Precio"].ToString();
                        oWs.Cells[_fila, 18].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 19].Value = item["TotalImporte"].ToString();
                        oWs.Cells[_fila, 19].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 20].Value = item["CantPanios"].ToString();
                        oWs.Cells[_fila, 21].Value = item["MedidaHorizontal"].ToString();
                        oWs.Cells[_fila, 22].Value = item["MedidaVertical"].ToString();


                        oWs.Cells[_fila, 23].Value = item["FechaEjecucionVereda"].ToString();
                        oWs.Cells[_fila, 24].Value = item["Estado"].ToString();

                        oWs.Cells[_fila, 25].Value = item["observacion"].ToString();


                        oWs.Cells[_fila, 26].Value = item["OrdenOrigen"].ToString();
                        oWs.Cells[_fila, 27].Value = item["EmpresaOrigen"].ToString();
                        oWs.Cells[_fila, 28].Value = item["TipoMaterialOrigen"].ToString();

                        oWs.Cells[_fila, 29].Value = item["LargoOrigen"].ToString();
                        oWs.Cells[_fila, 29].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 30].Value = item["AnchoOrigen"].ToString();
                        oWs.Cells[_fila, 30].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 31].Value = item["EspesorOrigen"].ToString();
                        oWs.Cells[_fila, 31].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 32].Value = item["TotalOrigen"].ToString();
                        oWs.Cells[_fila, 32].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 33].Value = item["CantPaniosOrigen"].ToString();
                        oWs.Cells[_fila, 34].Value = item["MedidaHorizontalOrigen"].ToString();
                        oWs.Cells[_fila, 35].Value = item["MedidaVerticalOrigen"].ToString();
                        oWs.Cells[_fila, 36].Value = item["FechaOrigen"].ToString();

                        _fila++;
                    }
                    
                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 36; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }

        public object get_descargar_detalleOT_dataGeneral(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario)
        {
            Resultado res = new Resultado();
            DataTable listaDetallado = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_ANALISIS_DETALLADO_PROVEEDOR_nuevo_DAA", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;

                        cmd.Parameters.Add("@fechaIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(listaDetallado);
                        }

                    }
                }

                if (listaDetallado.Rows.Count <= 0)
                {
                    res.ok = false;
                    res.data = "0|No hay informacion disponible";
                }
                else
                {
                    res.ok = true;
                    res.data = generarArchivoExcel_detalleOT_dataGeneral(listaDetallado, idUsuario);
                }

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

        public string generarArchivoExcel_detalleOT_dataGeneral(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 4;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_detalleOT.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_detalleOT.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);

                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("detalleOT_DataGeneral");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));

                    oWs.Cells[1, 1].Style.Font.Size = 15; //letra tamaño  2
                    oWs.Cells[1, 1].Value = listDetalle.Rows[0]["tituloReporte"].ToString();
                    oWs.Cells[1, 1, 1, 35].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Cells[3, 1].Value = "SERVICIO";
                    oWs.Cells[3, 2].Value = "TIPO DE ORDEN";
                    oWs.Cells[3, 3].Value = "NRO ORDEN ";
                    oWs.Cells[3, 4].Value = "SUMINISTRO";
                    oWs.Cells[3, 5].Value = "NRO SED";

                    oWs.Cells[3, 6].Value = "DIRECCION";
                    oWs.Cells[3, 7].Value = "DISTRITO";
                    oWs.Cells[3, 8].Value = "PROVEEDOR ";
                    oWs.Cells[3, 9].Value = "FECHA ASIGNACION PROVEEDOR";
                    oWs.Cells[3, 10].Value = "JEFE DE CUADRILLA";

                    oWs.Cells[3, 11].Value = "TIPO DE TRABAJO";
                    oWs.Cells[3, 12].Value = "TIPO DE MATERIAL";
                    oWs.Cells[3, 13].Value = "LARGO";
                    oWs.Cells[3, 14].Value = "ANCHO";
                    oWs.Cells[3, 15].Value = "ESPESOR";

                    oWs.Cells[3, 16].Value = "FACTOR MULTIPLO";
                    oWs.Cells[3, 17].Value = "TOTAL";
                    oWs.Cells[3, 18].Value = "PRECIO";
                    oWs.Cells[3, 19].Value = "TOTAL IMPORTE";


                    oWs.Cells[3, 20].Value = "CANT PAÑOS";
                    oWs.Cells[3, 21].Value = "MEDIDA HORIZONTAL";
                    oWs.Cells[3, 22].Value = "MEDIDA VERTICAL";

                    oWs.Cells[3, 23].Value = "FECHA EJECUCION VEREDA";
                    oWs.Cells[3, 24].Value = "ESTADO";

                    oWs.Cells[3, 25].Value = "OBSERVACION";

                    oWs.Cells[3, 26].Value = "ORDEN DE ORIGEN";

                    oWs.Cells[3, 27].Value = "EMPRESA ORIGEN";
                    oWs.Cells[3, 28].Value = "TIPO MATERIAL ORIGEN";
                    oWs.Cells[3, 29].Value = "LARGO ORIGEN";
                    oWs.Cells[3, 30].Value = "ANCHO ORIGEN";
                    oWs.Cells[3, 31].Value = "ESPESOR ORIGEN";
                    oWs.Cells[3, 32].Value = "TOTAL ORIGEN";

                    oWs.Cells[3, 33].Value = "CANT PAÑOS ORIGEN ";
                    oWs.Cells[3, 34].Value = "MEDIDA HORIZONTAL ORIGEN";
                    oWs.Cells[3, 35].Value = "MEDIDA VERTICAL ORIGEN";


                    int ac = 0;
                    foreach (DataRow item in listDetalle.Rows)
                    {
                        ac += 1;


                        oWs.Cells[_fila, 1].Value = item["Servicio"].ToString();
                        oWs.Cells[_fila, 2].Value = item["TipoOrden"].ToString();
                        oWs.Cells[_fila, 3].Value = item["NroOrden"].ToString();
                        oWs.Cells[_fila, 4].Value = item["Suminstro"].ToString();
                        oWs.Cells[_fila, 5].Value = item["NroSed"].ToString();

                        oWs.Cells[_fila, 6].Value = item["Direccion"].ToString();
                        oWs.Cells[_fila, 7].Value = item["Distrito"].ToString();
                        oWs.Cells[_fila, 8].Value = item["Proovedor"].ToString();
                        oWs.Cells[_fila, 9].Value = item["FechaAsignacionProovedor"].ToString();
                        oWs.Cells[_fila, 10].Value = item["JefeCuadrilla"].ToString();

                        oWs.Cells[_fila, 11].Value = item["TipoTrabajo"].ToString();
                        oWs.Cells[_fila, 12].Value = item["TipoMaterial"].ToString();
                        oWs.Cells[_fila, 13].Value = item["Largo"].ToString();
                        oWs.Cells[_fila, 13].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 14].Value = item["Ancho"].ToString();
                        oWs.Cells[_fila, 14].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 15].Value = item["Espesor"].ToString();
                        oWs.Cells[_fila, 15].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 16].Value = item["FactorMultiplo"].ToString();
                        oWs.Cells[_fila, 16].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 17].Value = item["Total"].ToString();
                        oWs.Cells[_fila, 17].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 18].Value = item["Precio"].ToString();
                        oWs.Cells[_fila, 18].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 19].Value = item["TotalImporte"].ToString();
                        oWs.Cells[_fila, 19].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 20].Value = item["CantPanios"].ToString();
                        oWs.Cells[_fila, 21].Value = item["MedidaHorizontal"].ToString();
                        oWs.Cells[_fila, 22].Value = item["MedidaVertical"].ToString();


                        oWs.Cells[_fila, 23].Value = item["FechaEjecucionVereda"].ToString();
                        oWs.Cells[_fila, 24].Value = item["Estado"].ToString();

                        oWs.Cells[_fila, 25].Value = item["observacion"].ToString();


                        oWs.Cells[_fila, 26].Value = item["OrdenOrigen"].ToString();

                        oWs.Cells[_fila, 27].Value = item["EmpresaOrigen"].ToString();
                        oWs.Cells[_fila, 28].Value = item["TipoMaterialOrigen"].ToString();

                        oWs.Cells[_fila, 29].Value = item["LargoOrigen"].ToString();
                        oWs.Cells[_fila, 29].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 30].Value = item["AnchoOrigen"].ToString();
                        oWs.Cells[_fila, 30].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 31].Value = item["EspesorOrigen"].ToString();
                        oWs.Cells[_fila, 31].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  

                        oWs.Cells[_fila, 32].Value = item["TotalOrigen"].ToString();
                        oWs.Cells[_fila, 32].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto  


                        oWs.Cells[_fila, 33].Value = item["CantPaniosOrigen"].ToString();
                        oWs.Cells[_fila, 34].Value = item["MedidaHorizontalOrigen"].ToString();
                        oWs.Cells[_fila, 35].Value = item["MedidaVerticalOrigen"].ToString();

                        _fila++;
                    }


                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    oWs.Row(3).Style.Font.Bold = true;
                    oWs.Row(3).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(3).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 35; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }
                     
        public object get_descargar_detalleContratista_New(int idServicio, int idTipoOT, int idProveedor, string fechaIni, string fechaFin, int idEstado, int idUsuario)
        {
            Resultado res = new Resultado();
            DataTable listaDetallado = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REPORTE_ANALISIS_DETALLADO_PROVEEDOR_NEW", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idServicio", SqlDbType.Int).Value = idServicio;
                        cmd.Parameters.Add("@idTipoOT", SqlDbType.Int).Value = idTipoOT;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;

                        cmd.Parameters.Add("@fechaIni", SqlDbType.VarChar).Value = fechaIni;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(listaDetallado);
                        }

                    }
                }

                if (listaDetallado.Rows.Count <= 0)
                {
                    res.ok = false;
                    res.data = "0|No hay informacion disponible";
                }
                else
                {
                    res.ok = true;
                    res.data = generarArchivoExcel_detalleContratista_new(listaDetallado, idUsuario);
                }

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

        public string generarArchivoExcel_detalleContratista_new(DataTable listDetalle, int id_usuario)
        {
            string Res = "";
            int _fila = 3;
            string FileRuta = "";
            string FileExcel = "";

            try
            {

                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Excel/" + id_usuario + "_detalleContratista.xlsx");
                string rutaServer = ConfigurationManager.AppSettings["ServerFilesReporte"];

                FileExcel = rutaServer + id_usuario + "_detalleContratista.xlsx";

                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }

                Thread.Sleep(1);
                int pos = 1;


                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("detalleContratista");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 8));


                    oWs.Cells[1, 1].Style.Font.Size = 14; //letra tamaño  2
                    oWs.Cells[1, 1].Value = "1- ZONA DE REGISTRO DE TRABAJO - ETAPA : ROTURAS";
                    oWs.Cells[1, 1, 1, 20].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 1].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 1].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;
                    oWs.Cells[1, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;   // fondo de celda
                    oWs.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray); // fondo de celda
                    oWs.Cells[1, 1].Style.Font.Bold = true; //Letra negrita

                    oWs.Cells[1, 21].Style.Font.Size = 14; //letra tamaño  2
                    oWs.Cells[1, 21].Value = "2 - ETAPA : RECOJO DE DESMONTE DE ROTURA";
                    oWs.Cells[1, 21, 1, 34].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 21].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 21].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;
                    oWs.Cells[1, 21].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;   // fondo de celda
                    oWs.Cells[1, 21].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DeepSkyBlue); // fondo de celda
                    oWs.Cells[1, 21].Style.Font.Bold = true; //Letra negrita

                    oWs.Cells[1, 35].Style.Font.Size = 14; //letra tamaño  2
                    oWs.Cells[1, 35].Value = "3 - ETAPA : REPARACIÓN ";
                    oWs.Cells[1, 35, 1, 60].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 35].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 35].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;
                    oWs.Cells[1, 35].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;   // fondo de celda
                    oWs.Cells[1, 35].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen); // fondo de celda
                    oWs.Cells[1, 35].Style.Font.Bold = true; //Letra negrita

                    oWs.Cells[1, 61].Style.Font.Size = 14; //letra tamaño  2
                    oWs.Cells[1, 61].Value = "4 - ETAPA : RECOJO DE DESMONTES DE REPARACIÓN ";
                    oWs.Cells[1, 61, 1, 75].Merge = true;  // combinar celdaS
                    oWs.Cells[1, 61].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Cells[1, 61].Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;
                    oWs.Cells[1, 61].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;   // fondo de celda
                    oWs.Cells[1, 61].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow); // fondo de celda
                    oWs.Cells[1, 61].Style.Font.Bold = true; //Letra negrita

                    oWs.Cells[2, pos].Value = "ITEMS"; pos += 1;
                    oWs.Cells[2, pos].Value = "MES"; pos += 1;
                    oWs.Cells[2, pos].Value = "ANIO"; pos += 1;
                    oWs.Cells[2, pos].Value = "TIPO TRABAJO"; pos += 1;
                    oWs.Cells[2, pos].Value = "SERVICIO"; pos += 1;
                    oWs.Cells[2, pos].Value = "NRO ORDEN"; pos += 1;
                    oWs.Cells[2, pos].Value = "NRO SED"; pos += 1;
                    oWs.Cells[2, pos].Value = "NRO SUMINISTRO"; pos += 1;
                    oWs.Cells[2, pos].Value = "DIRECCION"; pos += 1;
                    oWs.Cells[2, pos].Value = "DISTRITO"; pos += 1;
                    oWs.Cells[2, pos].Value = "PROVEEDOR"; pos += 1;
                    oWs.Cells[2, pos].Value = "JEFECUADRILLA"; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA INICIO TRABAJO"; pos += 1;
                    oWs.Cells[2, pos].Value = "DEMOLICION VEREDAS M2"; pos += 1;
                    oWs.Cells[2, pos].Value = "DESMONTE GENERADO M3"; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA FINALIZACION TRABAJO"; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA REQUERIMIENTO APROBACION COORDINADOR"; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA APROBACION COORDINADOR"; pos += 1;
                    oWs.Cells[2, pos].Value = "NOMBRE COORDINADOR RESPONSABLE"; pos += 1;
                    oWs.Cells[2, pos].Value = "ESTADO"; pos += 1;  ///----20

                    oWs.Cells[2, pos].Value = "FECHA HORA ASIGNACION PROVEEDOR DESMONTE ROTURA"; pos += 1;
                    oWs.Cells[2, pos].Value = "TIPO TRABAJO"; pos += 1;
                    oWs.Cells[2, pos].Value = "EMPRESA CONTRATISTA"; pos += 1;
                    oWs.Cells[2, pos].Value = "JEFE CUADRILLA"; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA EJECUCION RECOJO"; pos += 1;
                    oWs.Cells[2, pos].Value = "DESMONTE RECOGIDO M3"; pos += 1;
                    oWs.Cells[2, pos].Value = "MOTIVO"; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA REQUERIMIENTO APROBACION COORDINADOR"; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA APROBACION COORDINADOR"; pos += 1;
                    oWs.Cells[2, pos].Value = "NOMBRE COORDINADOR RESPONSABLE"; pos += 1;
                    oWs.Cells[2, pos].Value = "ESTADO "; pos += 1;
                    oWs.Cells[2, pos].Value = "P.U"; pos += 1;
                    oWs.Cells[2, pos].Value = "TOTAL RECOGIDO M3"; pos += 1;
                    oWs.Cells[2, pos].Value = "VALORIZACION TOTAL S/"; pos += 1;  /// 34

                    oWs.Cells[2, pos].Value = "FECHA HORA ASIGNACION PROVEEDOR REPARACION"; pos += 1;
                    oWs.Cells[2, pos].Value = "TIPO TRABAJO"; pos += 1;
                    oWs.Cells[2, pos].Value = "EMPRESA CONTRATISTA"; pos += 1;
                    oWs.Cells[2, pos].Value = "REPARACION VEREDAS M2"; pos += 1;
                    oWs.Cells[2, pos].Value = "DESMONTE GENERADO M3 "; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA INICIO REPARACION"; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA TERMINO REPARACION"; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA REQUERIMIENTO APROBACION COORDINADOR"; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA APROBACION COORDINADOR "; pos += 1;
                    oWs.Cells[2, pos].Value = "NOMBRE COORDINADOR RESPONSABLE"; pos += 1;
                    oWs.Cells[2, pos].Value = "ESTADO"; pos += 1;

                    //---NUEVOS CAMPOS ---

                    oWs.Cells[2, pos].Value = "VEREDA"; pos += 1;
                    oWs.Cells[2, pos].Value = "PISO ESPECIAL"; pos += 1;
                    oWs.Cells[2, pos].Value = "PISTA CONCRETO / ASFALTO"; pos += 1;
                    oWs.Cells[2, pos].Value = "GRASS"; pos += 1;
                    oWs.Cells[2, pos].Value = "PRECIO VEREDA"; pos += 1;
                    oWs.Cells[2, pos].Value = "PRECIO PISO ESPECIAL"; pos += 1;
                    oWs.Cells[2, pos].Value = "PRECIO PISTA CONCRETO / ASFALTO"; pos += 1;
                    oWs.Cells[2, pos].Value = "PRECIO GRASS"; pos += 1;
                    oWs.Cells[2, pos].Value = "TOTAL VEREDA"; pos += 1;
                    oWs.Cells[2, pos].Value = "TOTAL PISO ESPECIAL"; pos += 1;
                    oWs.Cells[2, pos].Value = "TOTAL PISTA CONCRETO / ASFALTO"; pos += 1;
                    oWs.Cells[2, pos].Value = "TOTAL GRASS"; pos += 1;

                    //---- FIN


                    oWs.Cells[2, pos].Value = "P.U"; pos += 1;
                    oWs.Cells[2, pos].Value = "TOTAL REPARACION VEREDAS M2"; pos += 1;
                    oWs.Cells[2, pos].Value = "VALORIZACION TOTAL S/"; pos += 1; // 60

                    oWs.Cells[2, pos].Value = "FECHA HORA ASIGNACION PROVEEDOR RECOGO DESMONTE REPARACION"; pos += 1; // 61
                    oWs.Cells[2, pos].Value = "TIPO TRABAJO"; pos += 1;
                    oWs.Cells[2, pos].Value = "EMPRESA CONTRATISTA"; pos += 1;
                    oWs.Cells[2, pos].Value = "JEFE CUADRILLA"; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA INICIO TRABAJO"; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA FINALIZACION TRABAJO"; pos += 1;
                    oWs.Cells[2, pos].Value = "DESMONTE RECOGIDO M3"; pos += 1;
                    oWs.Cells[2, pos].Value = "MOTIVO"; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA REQUERIMIENTO APROBACION COORDINADOR "; pos += 1;
                    oWs.Cells[2, pos].Value = "FECHA HORA APROBACION COORDINADOR"; pos += 1;
                    oWs.Cells[2, pos].Value = "APROBACION COORDINADOR RESPONSABLE"; pos += 1;
                    oWs.Cells[2, pos].Value = "ESTADO"; pos += 1;
                    oWs.Cells[2, pos].Value = "PU"; pos += 1;
                    oWs.Cells[2, pos].Value = "TOTAL RECOGIDO M3"; pos += 1;
                    oWs.Cells[2, pos].Value = "VALORIZACION TOTAL S/"; pos += 1;

                    //oWs.Cells[2, pos].Value = "LARGO"; pos += 1;
                    //oWs.Cells[2, pos].Value = "ANCHO"; pos += 1;
                    //oWs.Cells[2, pos].Value = "ALTURA"; pos += 1;
                    //oWs.Cells[2, pos].Value = "COMENTARIO DEL GESTOR"; pos += 1;
                    //oWs.Cells[2, pos].Value = "ESTATUS"; pos += 1;



                    int ac = 0;
                    foreach (DataRow item in listDetalle.Rows)
                    {

                        pos = 1;
                        oWs.Cells[_fila, pos].Value = item["items"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["mes"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["anio"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["tipotrabajo"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["servicio"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["nroorden"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["nrosed"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["nrosuministro"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["direccion"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["distrito"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["proveedor"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["jefecuadrilla"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechahorainiciotrabajo"].ToString(); pos += 1;

 
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["demolicionveredasm2"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;

                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["desmontegeneradom3"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;

                        oWs.Cells[_fila, pos].Value = item["fechahorafinalizaciontrabajo"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechahorarequerimientoaprobacioncoordinador"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechahoraaprobacioncoordinador"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["nombrecoordinadorresponsable"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["estado"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechahoraasignacionproveedordesmonterotura"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["tipotrabajo2"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["empresacontratista"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["jefecuadrilla2"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechaejecucionrecojo"].ToString(); pos += 1;

                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["desmonterecogidom3"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;

                        oWs.Cells[_fila, pos].Value = item["motivo"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechahorarequerimientoaprobacioncoordinador2"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechahoraaprobacioncoordinador2"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["nombrecoordinadorresponsable2"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["estado2"].ToString(); pos += 1;
                                
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["pu"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;

                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["totalrecogidom3"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;

                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["valorizaciontotalsol"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;

                        oWs.Cells[_fila, pos].Value = item["fechahoraasignacionproveedorreparacion"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["tipotrabajo3"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["empresacontratista2"].ToString(); pos += 1;
 
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["reparacionveredasm2"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;
 
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["desmontegeneradom3_2"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;

                        oWs.Cells[_fila, pos].Value = item["fechahorainicioreparacion"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechahoraterminoreparacion"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechahorarequerimientoaprobacioncoordinador3"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechahoraaprobacioncoordinador3"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["nombrecoordinadorresponsable3"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["estado3"].ToString(); pos += 1;

                        //-----campos nuevos---

                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["vereda"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["pisoespecial"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["pistaconcretoasfalto"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["grass"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["preciovereda"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["preciopisoespecial"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["preciopistaconcretoasfalto"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["preciograss"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["totalvereda"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["totalpisoespecial"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["totalpistaconcretoasfalto"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["totalgrass"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;
                     
                        //-----fincampos nuevos---
                        
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["pu2"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;

                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["totalreparacionveredasm2"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;

                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["valorizaciontotalsol_2"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;

                        oWs.Cells[_fila, pos].Value = item["fechahoraasignacionproveedorrecogodesmontereparacion"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["tipotrabajo4"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["empresacontratista3"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["jefecuadrilla3"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechahorainiciotrabajo2"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechahorafinalizaciontrabajo2"].ToString(); pos += 1;
 
                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["desmonterecogidom32"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;


                        oWs.Cells[_fila, pos].Value = item["motivo2"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechahorarequerimientoaprobacioncoordinador4"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["fechahoraaprobacioncoordinador4"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["aprobacioncoordinadorresponsable"].ToString(); pos += 1;
                        oWs.Cells[_fila, pos].Value = item["estado4"].ToString(); pos += 1;

                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["pu3"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;

                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["totalrecogidom3_2"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;

                        oWs.Cells[_fila, pos].Style.Numberformat.Format = "#,##0.00";
                        oWs.Cells[_fila, pos].Value = Math.Round(Convert.ToDecimal(item["valorizaciontotalsol_3"]), 2);
                        oWs.Cells[_fila, pos].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right;
                        pos += 1;

                        //oWs.Cells[_fila, pos].Value = item["largo"].ToString(); pos += 1;
                        //oWs.Cells[_fila, pos].Value = item["ancho"].ToString(); pos += 1;
                        //oWs.Cells[_fila, pos].Value = item["altura"].ToString(); pos += 1;
                        //oWs.Cells[_fila, pos].Value = item["comentarioGestor"].ToString(); pos += 1;
                        //oWs.Cells[_fila, pos].Value = item["estatus"].ToString(); pos += 1;

                        _fila++;
                    }


                    oWs.Row(2).Style.Font.Bold = true;
                    oWs.Row(2).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(2).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;

                    for (int k = 1; k <= 75; k++)
                    {
                        oWs.Column(k).AutoFit();
                    }
                    oEx.Save();
                }

                Res = FileExcel;
            }
            catch (Exception)
            {
                throw;
            }
            return Res;
        }



    }
}
