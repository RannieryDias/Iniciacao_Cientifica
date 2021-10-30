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

namespace Requests
{
    class Serializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public void SerializeProjeto(List<Projeto> projetos)
        {
            int total = 0;
            log.LogIt("***********************************");
            log.LogIt("Saving on DB - Started at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");
            timer.Start();
            foreach (var projeto in projetos)
            {
                var Json = JsonConvert.SerializeObject(projeto);
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44378/api/projetos");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {

                        streamWriter.Write(Json);
                    }
                    total++;

                    if (total % 500 == 0)
                    {
                        log.LogIt(total + " Projetos saved on DB");
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    now = DateTime.Now;
                    log.LogIt("Could not parse response: " + projeto + "to object type of Projeto, at " + now + "Error: " + e.Message);
                }

            }
            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();
            now = DateTime.Now;

            log.LogIt("***********************************");
            log.LogIt("The total of " + total + " Projetos was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);
        }
    }
}
