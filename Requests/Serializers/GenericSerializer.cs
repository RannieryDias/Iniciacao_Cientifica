﻿using IC_API.Model;
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
    class GenericSerializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;
        int total;

        public void SerializeProjetoDetalhado<T>(List<T> entities)
        {
            total = 0;

            log.LogIt("***********************************");
            log.LogIt($"Saving entities on DB - Started at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");

            timer.Start();

            if (entities.GetType() == typeof(List<ProjetoDetalhado>))
            {
                PutIntoAPI("https://localhost:44378/api/ProjetoDetalhados", entities);
            }
            else if (entities.GetType() == typeof(List<Autor>))
            {
                PutIntoAPI("https://localhost:44378/api/Autores", entities);
            }
            else if (entities.GetType() == typeof(List<Deputado>))
            {
                PutIntoAPI("https://localhost:44378/api/Deputados", entities);
            }
            else if (entities.GetType() == typeof(List<Partido>))
            {
                PutIntoAPI("https://localhost:44378/api/Partidos", entities);
            }
            //TODO Tramitação
            else if (entities.GetType() == typeof(List<Tramitacao>))
            {
                PutIntoAPI("https://localhost:44378/api/Tramitacoes", entities);
            }

            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();

            log.LogIt("***********************************");
            try
            {
                log.LogIt("The total of " + total + " " + entities.FirstOrDefault().GetType() + " was serialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);
            }
            catch (NullReferenceException)
            {
                log.LogIt("No item to add to the database");   
            }
        }

        public void PutIntoAPI<T>(string url, List<T> entities)
        {
            foreach (var entity in entities)
            {
                var Json = JsonConvert.SerializeObject(entity);
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

                    total++;
                    if (total % 500 == 0)
                    {
                        log.LogIt(total + " saved " + entity.GetType() + "s on DB");
                    }
                }
                catch (Exception e)
                {
                    log.LogIt($"Could not parse response: " + entity + "to object type of " + entity.GetType() + ", at " + now + "Error: " + e.Message);
                }
            }
        }
    }

}
