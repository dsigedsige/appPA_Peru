using Entidades.Mantenimientos;
using Negocio.Conexion;
using Negocio.Resultados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Mantenimientos
{
    public class Usuarios_BL
    {
        public DataTable get_usuariosAccesos(string idOpciones)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_ACCESOS_MENU_USUARIO_MENU", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idOpciones", SqlDbType.VarChar).Value = idOpciones;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
                return dt_detalle;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable get_perfilAccesos(string idOpciones)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_ACCESOS_MENU_PERFIL_MENU", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idOpciones", SqlDbType.VarChar).Value = idOpciones;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
                return dt_detalle;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public DataTable get_eventosUsuarioMarcados(string idOpciones, int idUsuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_ACCESOS_MENU_LIST_EVENTOS_USUARIO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idOpciones", SqlDbType.VarChar).Value = idOpciones;
                        cmd.Parameters.Add("@id_Usuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
                return dt_detalle;
            }
            catch (Exception)
            {
                throw;
            }
        }
               
        public DataTable get_eventosPerfilMarcados(string idOpciones, int idPerfil)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_ACCESOS_MENU_LIST_EVENTOS_PERFIL", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idOpciones", SqlDbType.VarChar).Value = idOpciones;
                        cmd.Parameters.Add("@idPerfil", SqlDbType.Int).Value = idPerfil;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
                return dt_detalle;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public string set_grabandoEventos(string idOpciones, string idEventos, int idUsuario)
        {
            string res = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_ACCESOS_MENU_GRABAR_ACCESOS", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idOpciones", SqlDbType.VarChar).Value = idOpciones;
                        cmd.Parameters.Add("@idEventos", SqlDbType.VarChar).Value = idEventos;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
                        cmd.ExecuteNonQuery();
                        res = "OK";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        public string set_grabandoEventosPerfiles(string idOpciones, string idEventos, int idPerfil)
        {
            string res = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_ACCESOS_MENU_GRABAR_ACCESOS_PEFIL", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idOpciones", SqlDbType.VarChar).Value = idOpciones;
                        cmd.Parameters.Add("@idEventos", SqlDbType.VarChar).Value = idEventos;
                        cmd.Parameters.Add("@idPerfil", SqlDbType.Int).Value = idPerfil;
                        cmd.ExecuteNonQuery();
                        res = "OK";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }


        public DataTable get_usuariosMantenimiento(int idEmpresa, int idArea, int idEstado)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_USUARIO_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = idEmpresa;
                        cmd.Parameters.Add("@idArea", SqlDbType.Int).Value = idArea;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;

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


        public object get_empresas()
        {
            Resultado res = new Resultado();
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_PRECIO_EMPRESA_COMBO_EMPRESA", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@idOT", SqlDbType.Int).Value = idOt;
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
        
        public object get_tiposMaterial()
        {
            Resultado res = new Resultado();
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_PRECIO_EMPRESA_COMBO_TIPO_MATERIAL", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@idOT", SqlDbType.Int).Value = idOt;
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

        public object get_baremos()
        {
            Resultado res = new Resultado();
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_PRECIO_EMPRESA_COMBO_BAREMOS", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@idOT", SqlDbType.Int).Value = idOt;
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


        public object get_listadoPrecioEmpresas(int idEmpresa, int idEstado)
        {
            Resultado res = new Resultado();
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_PRECIO_EMPRESA_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = idEmpresa;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;

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

        public string set_grabandoPrecioEmpresa(PrecioMaterial_E PrecioMaterial_E)
        {
            string res = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_PRECIO_EMPRESA_SAVE_UPDATE", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_precioMaterial", SqlDbType.Int).Value = PrecioMaterial_E.id_precioMaterial;
                        cmd.Parameters.Add("@id_empresa", SqlDbType.Int).Value = PrecioMaterial_E.id_empresa;

                        cmd.Parameters.Add("@id_servicio", SqlDbType.Int).Value = PrecioMaterial_E.id_servicio;
                        cmd.Parameters.Add("@id_tipoTrabajo", SqlDbType.Int).Value = PrecioMaterial_E.id_tipoTrabajo;
                        cmd.Parameters.Add("@id_TipoMaterial", SqlDbType.Int).Value = PrecioMaterial_E.id_TipoMaterial;

                        cmd.Parameters.Add("@precio", SqlDbType.VarChar).Value = PrecioMaterial_E.precio;
                        cmd.Parameters.Add("@tipo", SqlDbType.VarChar).Value = PrecioMaterial_E.tipo;
                        cmd.Parameters.Add("@id_Baremo", SqlDbType.Int).Value = PrecioMaterial_E.id_Baremo;
                        cmd.Parameters.Add("@estado", SqlDbType.Int).Value = PrecioMaterial_E.estado;
                        cmd.Parameters.Add("@usuario_creacion", SqlDbType.Int).Value = PrecioMaterial_E.usuario_creacion;

                        cmd.ExecuteNonQuery();
                        res = "OK";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        public object get_listadoPrecioEmpresas_detalle(int idEmpresa)
        {
            Resultado res = new Resultado();
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_PRECIO_EMPRESA_DET", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idEmpresa", SqlDbType.Int).Value = idEmpresa;

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


        public string set_agregar_Mantenimiento(string codigo, string descripcion, string precio, int tipoMantenimiento,int usuario_creacion )
        {
            string res = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_PRECIO_EMPRESA_AGREGAR_MAESTROS", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = codigo;
                        cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = descripcion;
                        cmd.Parameters.Add("@precio", SqlDbType.VarChar).Value = precio;

                        cmd.Parameters.Add("@tipoMantenimiento", SqlDbType.Int).Value = tipoMantenimiento;
                        cmd.Parameters.Add("@usuario_creacion", SqlDbType.Int).Value = usuario_creacion;

                        cmd.ExecuteNonQuery();
                        res = "OK";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }


        public string set_eliminarAccesos(string idOpciones,int idUsuario)
        {
            string res = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_ACCESOS_MENU_ELIMINAR_ACCESOS", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idOpciones", SqlDbType.VarChar).Value = idOpciones;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
                        cmd.ExecuteNonQuery();
                        res = "OK";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

    }
}
