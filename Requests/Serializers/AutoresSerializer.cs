using IC_API.Models;
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
    class AutoresSerializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public void SerializeAutor(List<Autor> autores)
        {
            int total = 0;
            log.LogIt("***********************************");
            log.LogIt("Saving Projetos Detalhados on DB - Started at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");
            timer.Start();
            foreach (var autor in autores)
            {
                var Json = JsonConvert.SerializeObject(autor);
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44378/api/Autores");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {

                        streamWriter.Write(Json);
                    }
                    total++;

                    if (total % 500 == 0)
                    {
                        log.LogIt(total + " Autores saved on DB");
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    log.LogIt("Could not parse response: " + autor + "to object type of Autores, at " + now + "Error: " + e.Message);
                }

            }
            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();

            log.LogIt("***********************************");
            log.LogIt("The total of " + total + " Autores was serialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);
        }
    }
}
