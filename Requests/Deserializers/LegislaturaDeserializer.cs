using AutoMapper;
using IC_API.Models;
using IC_API.Models.Responses.Legislatura;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Deserializers
{
    class LegislaturaDeserializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public List<Legislatura> DeserializeLegislatura()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IC_API.Models.Responses.Legislatura.Dado, IC_API.Models.Legislatura>();
                cfg.CreateMap<Legislatura, LegislaturaResponse>();
            });
            IMapper mapper = config.CreateMapper();

            List<Legislatura> legislaturas = new List<Legislatura>();

            timer.Start();
            now = DateTime.Now;
            log.LogIt("***********************************");
            log.LogIt("Started to deserialize Legislaturas at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");

            using (var webClient = new System.Net.WebClient())
            {
                string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/legislaturas?ordem=DESC&ordenarPor=id");
                try
                {
                    LegislaturaResponse legislaturaResponse = JsonConvert.DeserializeObject<LegislaturaResponse>(json, settings);
                    foreach (var response in legislaturaResponse.dados)
                    {
                        Legislatura legislatura = mapper.Map<Legislatura>(response);
                        legislaturas.Add(legislatura);
                        if (legislaturas.Count % 500 == 0)
                        {
                            log.LogIt(legislaturas.Count + " Legislaturas was deserialized! ");
                        }
                    }
                }
                catch (Exception e)
                {
                    log.LogIt("Could not parse! error: " + e.Message);
                }
            }

            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();
            now = DateTime.Now;

            log.LogIt("The total of " + legislaturas.Count + " Autores was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

            return legislaturas;
        }
    }
}
