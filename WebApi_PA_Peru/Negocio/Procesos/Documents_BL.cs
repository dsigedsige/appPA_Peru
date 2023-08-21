using Entidades.Procesos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Procesos
{
    public class Documents_BL
    {
        public string Insert_Documents(Documents documents)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlApiDocInventario"];
                httpClient.BaseAddress = new Uri(url);

                string endpoint = "SalidaInventario";

                string documentsJson = JsonConvert.SerializeObject(documents);
                StringContent content = new StringContent(documentsJson, Encoding.UTF8, "application/json");


                //HttpResponseMessage response = httpClient.PostAsync(endpoint, content).Result;

                //if (response.IsSuccessStatusCode)
                //{
                //    string responseContent = response.Content.ReadAsStringAsync().Result;
                //    return responseContent;
                //}
                //else
                //{
                //    return $"Error al llamar al API: {response.StatusCode}";
                //}
                return "";
            }
        }
    }
}
