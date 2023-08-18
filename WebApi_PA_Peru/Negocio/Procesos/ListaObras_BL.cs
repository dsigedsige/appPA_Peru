using Negocio.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Resultados;
using System.Configuration;
using System.Threading;
using System.Web;
using System.IO;
using Entidades.Procesos;

namespace Negocio.Procesos
{
    public class ListaObras_BL
    {

        public DataTable get_Areas()
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Combo_Area", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@Usuario", SqlDbType.Int).Value = idUsuario;
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

        public DataTable get_Cuadrillas(string area)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Combo_Cuadrillas", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Area", SqlDbType.VarChar).Value = area;
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


        public DataTable get_Estado()
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_Combo_Estado", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@Usuario", SqlDbType.Int).Value = idUsuario;
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


        public DataTable get_ListaObras(string servicio,string cuadrilla,string fechaInicio, string fechaFin, string estado)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_ListaObra_Foto", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Servicio", SqlDbType.VarChar).Value = servicio;
                        cmd.Parameters.Add("@Cuadrilla", SqlDbType.VarChar).Value = cuadrilla;
                        cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = fechaInicio;
                        cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = fechaFin;
                        cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = estado;
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


        public object get_fotosObras(string GesObraCodigo, string Usuario)
        {
            string vUsuario = "";
            Resultado res = new Resultado();
            //List<AprobarOT_E> obj_List = new List<AprobarOT_E>();
            DataTable dt_detalle = new DataTable();
            try
            {
                if (Usuario == null)
                    vUsuario = "";

                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_Tbl_Obra_Ejecucion_Listar_Foto", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@GesObraCodigo", SqlDbType.VarChar).Value = GesObraCodigo;                        
                        cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = vUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            res.ok = true;
                            res.data = dt_detalle;
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


        public string set_actualizar_obrasFoto(string IdObraEjecucion, string Usuario)
        {
            string resultado = "";
            string vUsuario = "";
            int vIdObraEjecucion= Convert.ToInt32(IdObraEjecucion.ToString());
            try
            {
                if (Usuario == null)
                    vUsuario = "";
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_Tbl_Obra_Ejecucion_Listar_Foto_Actualizar", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdObraEjecucion", SqlDbType.Int).Value = vIdObraEjecucion;
                        cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = vUsuario;                        

                        cmd.ExecuteNonQuery();
                        resultado = "OK";
                    }
                }
            }
            catch (Exception e)
            {
                resultado = e.Message;
            }
            return resultado;
        }


        public string set_insert_obrasFoto(ObraEjecucion obraEjecucion)
        {
            string resultado = "";            
            try
            {
               
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_Tbl_Obra_Ejecucion_InsertarActualizar", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@GesObraCodigo", SqlDbType.Int).Value = obraEjecucion.GesObraCodigo;
                        cmd.Parameters.Add("@LatitudFoto", SqlDbType.VarChar).Value = obraEjecucion.GesObraCodigo;
                        cmd.Parameters.Add("@LongitudFoto", SqlDbType.Int).Value = obraEjecucion.GesObraCodigo;
                        cmd.Parameters.Add("@NombreFoto", SqlDbType.VarChar).Value = obraEjecucion.GesObraCodigo;
                        cmd.Parameters.Add("@Usuario", SqlDbType.Int).Value = obraEjecucion.GesObraCodigo;
                        cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = obraEjecucion.GesObraCodigo;

                        cmd.ExecuteNonQuery();
                        resultado = "OK";      
                    }
                }
            }
            catch (Exception e)
            {
                resultado = e.Message;





            }
            return resultado;
        }





        public class download
        {
            public string nombreFile { get; set; }
            public string nombreBd { get; set; }
            public string ubicacion { get; set; }
        }

        public string get_descargar_Todos_fotosObras(string GesObraCodigo, string Usuario)
        {
            DataTable dt_detalle = new DataTable();
            List<download> list_files = new List<download>();
            string pathfile = "";
            string ruta_descarga = "";
            string vUsuario = "";

            try
            {
                if (Usuario == null)
                    vUsuario = "";
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_Tbl_Obra_Ejecucion_Listar_Foto", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@GesObraCodigo", SqlDbType.VarChar).Value = GesObraCodigo;
                        cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = vUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                            pathfile = HttpContext.Current.Server.MapPath("~/Archivos/Fotos/");

                            foreach (DataRow Fila in dt_detalle.Rows)
                            {
                                download obj_entidad = new download();
                                obj_entidad.nombreFile = Fila["NombreFoto"].ToString();
                                obj_entidad.ubicacion = pathfile;
                                list_files.Add(obj_entidad);
                            }

                            if (list_files.Count > 0)
                            {
                                if (list_files.Count == 1)
                                {
                                    ruta_descarga = ConfigurationManager.AppSettings["Archivos"] + "Fotos/" + list_files[0].nombreFile;
                                }
                                else
                                {
                                    ruta_descarga = comprimir_Files(list_files, vUsuario);
                                }
                            }
                            else
                            {
                                throw new System.InvalidOperationException("No hay archivo para Descargar");
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ruta_descarga;
        }

        public string comprimir_Files(List<download> list_download, string usuario_creacion)
        {
            string resultado = "";
            try
            {
                string ruta_destino = "";
                string ruta_descarga = "";
                string pathFoto = "";


                ruta_destino = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Descargas/Fotos_OBRA" + usuario_creacion + "Descarga.zip");
                ruta_descarga = ConfigurationManager.AppSettings["Archivos"] + "Descargas/Fotos_OBRA" + usuario_creacion + "Descarga.zip";

                if (File.Exists(ruta_destino)) /// verificando si existe el archivo zip
                {
                    System.IO.File.Delete(ruta_destino);
                }
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                {
                    foreach (download item in list_download)
                    {
                        pathFoto = item.ubicacion + item.nombreFile;
                        if (System.IO.File.Exists(pathFoto))
                        {
                            zip.AddFile(pathFoto, "");
                        }
                    }
                    // Guardando el archivo zip 
                    zip.Save(ruta_destino);
                }
                Thread.Sleep(2000);

                if (File.Exists(ruta_destino))
                {
                    resultado = ruta_descarga;
                }
                else
                {
                    throw new System.InvalidOperationException("No se pudo generar la Descarga del Archivo");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }


    }
}
