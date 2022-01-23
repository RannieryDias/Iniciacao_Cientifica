using AutoMapper;
using IC_API.Models;
using IC_API.Models.Responses.TemasResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Deserializers
{
    class TemaDeserializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public List<Tema> DeserializeTema()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            //Mapping objects
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IC_API.Models.Responses.TemasResponse.Dados, Tema>();
            });
            IMapper mapper = config.CreateMapper();

            List<Tema> temas = new List<Tema>();

            timer.Start();
            now = DateTime.Now;
            log.LogIt("***********************************");
            log.LogIt("Started to deserialize Temas at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");
            using (var webClient = new System.Net.WebClient())
            {
                string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/referencias/proposicoes/codTema");
                try
                {
                    TemasResponse temasResponse = JsonConvert.DeserializeObject<TemasResponse>(json, settings);

                    foreach (var response in temasResponse.dados)
                    {
                        Tema tema = mapper.Map<Tema>(response);

                        temas.Add(tema);
                    }
                }
                catch (Exception e)
                {
                    log.LogIt("Could not parse tema! " + "error: " + e.Message);
                }
            }

            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();
            now = DateTime.Now;

            log.LogIt("The total of " + temas.Count + " Temas was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

            return temas;
        }
    }
}
