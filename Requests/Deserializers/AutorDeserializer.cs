using AutoMapper;
using IC_API.Models;
using IC_API.Models.Responses.Autores_Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Deserializers
{
    class AutorDeserializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public List<Autor> DeserializeAutor(List<Projeto> projetos)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            //Mapping objects
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IC_API.Models.Responses.Autores_Response.Dado, Autor>()
                                           .ForMember(x => x.id, opt => opt.Ignore());
                cfg.CreateMap<IC_API.Models.Responses.Autores_Response.AutoresResponse, Autores>();
            });
            IMapper mapper = config.CreateMapper();

            List<Autor> autores = new List<Autor>();

            timer.Start();
            log.LogIt("***********************************");
            log.LogIt("Started to deserialize Autores at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");
            using (var webClient = new System.Net.WebClient())
            {
                foreach (var projeto in projetos)
                {
                    string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/{projeto.id}/autores");
                    try
                    {
                        AutoresResponse autoresResponse = JsonConvert.DeserializeObject<AutoresResponse>(json, settings);
                        foreach (var response in autoresResponse.dados)
                        {
                            Autor autor = mapper.Map<Autor>(response);
                            var cod = response.uri.Substring(response.uri.LastIndexOf("/") + 1);
                            autor.codDeputado = int.Parse(cod);
                            autor.id = int.Parse(projeto.id.ToString() + autor.codDeputado.ToString());
                            autores.Add(autor);
                            if (autores.Count % 500 == 0)
                            {
                                log.LogIt(autores.Count + " Autores was deserialized!");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        log.LogIt("Could not parse autor of response: " + projeto.id + " to object type of Autores " + "error: " + e.Message);
                    }
                }
            }
            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();

            log.LogIt("The total of " + autores.Count + " Autores was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

            return autores;
        }
    }
}
