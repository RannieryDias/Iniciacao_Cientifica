using AutoMapper;
using IC_API.Model;
using IC_API.Models.Responses;
using IC_API.Models.Responses.Deputado;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Requests.Deserializers
{
    class DeputadoDeserializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public List<Deputado> DeserializeDeputado()
        {
            //Mapping objects
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IC_API.Models.Responses.Deputado.Dados, Deputado>();
                cfg.CreateMap<IC_API.Models.Responses.Deputado.UltimoStatus, IC_API.Model.UltimoStatus>();
            });
            IMapper mapper = config.CreateMapper();

            List<Deputado> deputados = new List<Deputado>();
            DeputadosResponse deputadosResponse;
            DeputadoResponse deputadoResponse;

            timer.Start();
            log.LogIt("***********************************");
            log.LogIt("Started to deserialize Deputados at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");

            using (var webClient = new System.Net.WebClient())
            {
                string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/deputados");
                try
                {
                    deputadosResponse = JsonConvert.DeserializeObject<DeputadosResponse>(json);
                    foreach (var dept in deputadosResponse.dados)
                    {
                        json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/deputados/{dept.id}");

                        try
                        {
                            deputadoResponse = JsonConvert.DeserializeObject<DeputadoResponse>(json);
                            deputados.Add(mapper.Map<Deputado>(deputadoResponse.dados));
                            if (deputados.Count % 100 == 0)
                            {
                                log.LogIt(deputados.Count + " Deputados deserialized");
                            }
                        }
                        catch (Exception e)
                        {
                            log.LogIt("Could not parse response: " + dept.id + " to object type of Deputado " + "error: " + e.Message);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();

            log.LogIt("The total of " + deputados.Count + " Deputados was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

            return deputados;
        }

        public void ReadFiles()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IC_API.Models.Responses.Deputado.Dados, Deputado>();
            });
            IMapper mapper = config.CreateMapper();

            List<Deputado> deputados = new List<Deputado>();
            DeputadoResponse deputadoResponse;

            string filepath = @"E:\DeputadosPulados\";
            DirectoryInfo d = new DirectoryInfo(filepath);

            foreach (var file in d.GetFiles("*.json"))
            {
                var json = File.ReadAllText($@"{file.FullName}");
                int indexOf = json.IndexOf(" [],");
                json = json.Remove(indexOf+1, 3);
                deputadoResponse = JsonConvert.DeserializeObject<DeputadoResponse>(json);
            }
        }
    }
}
