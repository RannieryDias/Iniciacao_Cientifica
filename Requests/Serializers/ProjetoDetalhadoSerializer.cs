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
    class ProjetoDetalhadoSerializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public void SerializeProjetoDetalhado(List<ProjetoDetalhado> projetos)
        {
            int total = 0;
            log.LogIt("***********************************");
            log.LogIt("Saving Projetos Detalhados on DB - Started at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");
            timer.Start();
            foreach (var projeto in projetos)
            {
                var Json = JsonConvert.SerializeObject(projeto);
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44378/api/ProjetoDetalhados");
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
                    log.LogIt("Could not parse response: " + projeto + "to object type of Projeto, at " + now + "Error: " + e.Message);
                }

            }
            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();

            log.LogIt("***********************************");
            log.LogIt("The total of " + total + " Projetos was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);
        }

        public void SerializeStatusProjeto(List<StatusProposicao> status)
        {
            int total = 0;
            log.LogIt("***********************************");
            log.LogIt("Saving Projetos Detalhados on DB - Started at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");
            timer.Start();
            foreach (var projetoStatus in status)
            {
                var Json = JsonConvert.SerializeObject(projetoStatus);
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44378/api/StatusProposicaos");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {

                        streamWriter.Write(Json);
                    }
                    total++;

                    if (total % 500 == 0)
                    {
                        log.LogIt(total + " Status saved on DB");
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    log.LogIt("Could not parse response: " + projetoStatus + "to object type of Projeto, at " + now + "Error: " + e.Message);
                }

            }
            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();

            log.LogIt("***********************************");
            log.LogIt("The total of " + total + " inserted into database " + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);
        }


    }
}
