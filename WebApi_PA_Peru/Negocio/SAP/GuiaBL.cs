using Entidades.Procesos;
using Entidades.SAP;
using Negocio.Conexion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.SAP
{
    public class GuiaBL
    {



        public DataTable get_Movimiento()
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_Tbl_Obra_Ejecucion_Listar_Movimiento", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;                        
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


        public DataTable get_Guias(string movimiento,string fecha)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_Tbl_Obra_Ejecucion_Listar_Guias", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Movimiento", SqlDbType.VarChar).Value = movimiento;
                        cmd.Parameters.Add("@fecha", SqlDbType.VarChar).Value = fecha;
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



        public DataTable get_DetalleGuias(string guia)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_Tbl_Obra_Ejecucion_Listar_Guias_Det", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Guia", SqlDbType.VarChar).Value = guia;                        
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






        public string Insert_Guias(GuiaSap guias)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApiDocInventario"];
                httpClient.BaseAddress = new Uri(url);

                string endpoint = "SalidaInventario";

                string documentsJson = JsonConvert.SerializeObject(guias);
                StringContent content = new StringContent(documentsJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = httpClient.PostAsync(endpoint, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    return responseContent;
                }
                else
                {
                    return $"Error al llamar al API: {response.StatusCode}";
                }
               
            }
        }
    }
}
