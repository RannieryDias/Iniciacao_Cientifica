using AutoMapper;
using IC_API.Models;
using IC_API.Models.Responses.ListaPartido;
using IC_API.Models.Responses.Partido;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Deserializers
{
    class PartidoDeserializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public List<Partido> DeserializePartido()
        {
            //Mapping objects
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dados, Partido>();
            });
            IMapper mapper = config.CreateMapper();

            List<Partido> partidos = new List<Partido>();


            timer.Start();
            log.LogIt("***********************************");
            log.LogIt("Started to deserialize Partido at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");

            using (var webClient = new System.Net.WebClient())
            {
                string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/partidos?pagina=1&itens=100&ordem=ASC&ordenarPor=sigla");

                ListaPartidosResponse listaPartidos = JsonConvert.DeserializeObject<ListaPartidosResponse>(json);

                foreach (var response in listaPartidos.dados)
                {
                    try
                    {
                        json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/partidos/{response.id}");

                        try
                        {
                            PartidoResponse partido = JsonConvert.DeserializeObject<PartidoResponse>(json);

                            partidos.Add(mapper.Map<Partido>(partido.dados));
                        }
                        catch (Exception e)
                        {
                            log.LogIt("Could not parse response: " + response.id + " to object type of StatusProposicao " + " error: " + e.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        log.LogIt("Could not connect to the url of the partido" + response.id + " error: " + e.Message);
                    }

                }
            }

            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();

            log.LogIt("The total of " + partidos.Count + " Partidos was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

            return partidos;
        }

        public void DeserializeLider(int partidoId)
        {
            using (var webClient = new System.Net.WebClient())
            {
                string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/partidos/{partidoId}");

                //IC_API.Models.Responses.Partido.Lider lider = JsonConvert.DeserializeObject<IC_API.Models.Responses.Partido.Lider>(json.dados.status);
            }
        }

    }
}
