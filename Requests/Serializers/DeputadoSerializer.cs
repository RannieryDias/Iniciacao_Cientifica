using IC_API.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Serializers
{
    class DeputadoSerializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public void SerializeDeputado(List<Deputado> deputados)
        {
            int total = 0;
            log.LogIt("***********************************");
            log.LogIt("Saving Projetos Detalhados on DB - Started at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");
            timer.Start();
            foreach (var deputado in deputados)
            {
                var Json = JsonConvert.SerializeObject(deputado);
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44378/api/Deputados");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {

                        streamWriter.Write(Json);
                    }
                    total++;

                    if (total % 500 == 0)
                    {
                        log.LogIt(total + " Deputados saved on DB");
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    log.LogIt("Could not parse response: " + deputado + "to object type of Deputado, at " + now + "Error: " + e.Message);
                }

            }
            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();

            log.LogIt("***********************************");
            log.LogIt("The total of " + total + " Projetos was serialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);
        }
    }
}
