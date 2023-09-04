using Entidades.SAP;
using Negocio.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.SAP
{
    public class MigrationBL
    {

        public void set_guardar_articulo(ArticuloSapE objArticulo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MIGRACION_SAP_INSERT_UPDATE_ARTICULO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@ID_Articulo", SqlDbType.VarChar).Value = objArticulo.ID_Articulo;
                        cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = objArticulo.Codigo;
                        cmd.Parameters.Add("@Categoria", SqlDbType.VarChar).Value = objArticulo.Categoria;
                        cmd.Parameters.Add("@Linea", SqlDbType.VarChar).Value = objArticulo.Linea;

                        cmd.Parameters.Add("@SubLinea", SqlDbType.VarChar).Value = objArticulo.SubLinea;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = objArticulo.Descripcion;
                        cmd.Parameters.Add("@Abreviatura", SqlDbType.VarChar).Value = objArticulo.Abreviatura;
                        cmd.Parameters.Add("@UnidadMedida", SqlDbType.VarChar).Value = objArticulo.UnidadMedida;

                        cmd.Parameters.Add("@StockMinimo", SqlDbType.VarChar).Value = objArticulo.StockMinimo;
                        cmd.Parameters.Add("@TiempoVida", SqlDbType.VarChar).Value = objArticulo.TiempoVida;
                        cmd.Parameters.Add("@ExigeNroSerieKardex", SqlDbType.VarChar).Value = objArticulo.ExigeNroSerieKardex;
                        cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = objArticulo.Estado;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void set_guardar_proveedor(ProveedorSapE objProveedor)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MIGRACION_SAP_INSERT_UPDATE_PROVEEDOR", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_Proveedor", objProveedor.ID_Proveedor);
                        cmd.Parameters.AddWithValue("@Codigo", objProveedor.Codigo);
                        cmd.Parameters.AddWithValue("@NroRUC", objProveedor.NroRUC);
                        cmd.Parameters.AddWithValue("@RazonSocial", objProveedor.RazonSocial);
                        cmd.Parameters.AddWithValue("@Direccion", objProveedor.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono1", objProveedor.Telefono1);
                        cmd.Parameters.AddWithValue("@Contacto", objProveedor.Contacto);
                        cmd.Parameters.AddWithValue("@Email", objProveedor.Email);
                        cmd.Parameters.AddWithValue("@Estado", objProveedor.Estado);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void set_guardar_ordenCompra(CompraSapE objOrdenCompra)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand command = new SqlCommand("DSIGE_PROY_W_MIGRACION_SAP_INSERT_UPDATE_COMPRA_CAB", cn))
                    {
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar los parámetros necesarios al comando
                        command.Parameters.AddWithValue("@Id_Compra", objOrdenCompra.Id_Compra);
                        command.Parameters.AddWithValue("@Almacen", objOrdenCompra.Almacen);
                        command.Parameters.AddWithValue("@Localidad", objOrdenCompra.Localidad);
                        command.Parameters.AddWithValue("@TipoDocumento", objOrdenCompra.TipoDocumento);
                        command.Parameters.AddWithValue("@NumeroDoc", objOrdenCompra.NumeroDoc);
                        command.Parameters.AddWithValue("@NroGuiaRemision", objOrdenCompra.NroGuiaRemision);
                        command.Parameters.AddWithValue("@OCompra", objOrdenCompra.OCompra);
                        command.Parameters.AddWithValue("@Observacion", objOrdenCompra.Observacion);
                        command.Parameters.AddWithValue("@FechaGuia", objOrdenCompra.FechaGuia);
                        command.Parameters.AddWithValue("@FechaEmision", objOrdenCompra.FechaEmision);
                        command.Parameters.AddWithValue("@Moneda", objOrdenCompra.Moneda);
                        command.Parameters.AddWithValue("@Proveedor", objOrdenCompra.Proveedor);
                        command.Parameters.AddWithValue("@TipoCambio", objOrdenCompra.TipoCambio);

                        // Ejecutar el comando
                        command.ExecuteNonQuery();  
                    }

                    // Crear el comando para la inserción o actualización de la tabla DetalleCompras

                    using (SqlCommand commandDetalle = new SqlCommand("DSIGE_PROY_W_MIGRACION_SAP_INSERT_UPDATE_COMPRA_DET", cn))
                    {
                        commandDetalle.CommandTimeout = 0;
                        commandDetalle.CommandType = CommandType.StoredProcedure;    
                        // Agregar los parámetros necesarios al comando
                        commandDetalle.Parameters.AddWithValue("@Id_Compra", null);
                        commandDetalle.Parameters.AddWithValue("@Id_Compra_Det", null);
                        commandDetalle.Parameters.AddWithValue("@CodigoArticulo", null);
                        commandDetalle.Parameters.AddWithValue("@Cantidad", null);
                        commandDetalle.Parameters.AddWithValue("@Precio", null);
                        commandDetalle.Parameters.AddWithValue("@Estado", null);

                        foreach (DetalleCompras detalle in objOrdenCompra.DetalleCompras)
                        {
                            // Actualizar los valores de los parámetros para cada detalle
                            commandDetalle.Parameters["@Id_Compra"].Value = detalle.Id_Compra;
                            commandDetalle.Parameters["@Id_Compra_Det"].Value = detalle.Id_Compra_Det;
                            commandDetalle.Parameters["@CodigoArticulo"].Value = detalle.CodigoArticulo;
                            commandDetalle.Parameters["@Cantidad"].Value = detalle.Cantidad;
                            commandDetalle.Parameters["@Precio"].Value = detalle.Precio;
                            commandDetalle.Parameters["@Estado"].Value = detalle.Estado;

                            // Ejecutar el comando para cada detalle
                            commandDetalle.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void set_guardar_persona(PersonalSap_E objPersona)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MIGRACION_SAP_INSERT_UPDATE_empleados", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@CodigoInterno", SqlDbType.VarChar).Value = objPersona.CodigoInterno;
                        cmd.Parameters.Add("@Ges_Empl_Dni", SqlDbType.VarChar).Value = objPersona.Dni;
                        cmd.Parameters.Add("@Ges_Empl_Apellidos", SqlDbType.VarChar).Value = objPersona.Apellidos;
                        cmd.Parameters.Add("@Ges_Empl_Nombres", SqlDbType.VarChar).Value = objPersona.Nombres ;
                        cmd.Parameters.Add("@Ges_Empl_Direccion", SqlDbType.VarChar).Value = objPersona.Direccion;

                        cmd.Parameters.Add("@Ges_Empl_Telefono", SqlDbType.VarChar).Value = objPersona.Telefono;
                        cmd.Parameters.Add("@Ges_Empl_FechaIngreso", SqlDbType.VarChar).Value = (object)objPersona.FechaIngreso ?? DBNull.Value;
                        cmd.Parameters.Add("@Ges_Empl_FechaCese", SqlDbType.VarChar).Value = (object)objPersona.FechaCese ?? DBNull.Value;
                        cmd.Parameters.Add("@Cargo", SqlDbType.VarChar).Value = objPersona.Cargo;
                        cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = objPersona.Estado; 

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
