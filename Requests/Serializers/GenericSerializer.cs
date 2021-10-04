using IC_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Serializers
{
    class GenericSerializer
    {
        public void SerializeProjetoDetalhado<T>(List<T> entities)
        {
            if (entities.GetType() == typeof(List<ProjetoDetalhado>))
            {
                Console.WriteLine("aeHOOO");
                foreach(var projeto in entities)
                {
                    PutIntoAPI("https://localhost:44378/api/ProjetoDetalhados", projeto);
                }
            }
            //int total = 0;
            //foreach (var projeto in projetos)
            //{
           
            //}
        }

        public void PutIntoAPI(string url, object o)
        {
            var Json = JsonConvert.SerializeObject(o);
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    streamWriter.Write(Json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
            }
        }
    }

    //enum Tipos
    //{
    //    ProjetoDetalhado = 
    //}
}
