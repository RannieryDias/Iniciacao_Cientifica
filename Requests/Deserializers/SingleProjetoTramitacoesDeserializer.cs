using AutoMapper;
using IC_API.Models;
using IC_API.Models.Responses.Tramitacoes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Deserializers
{
    class SingleProjetoTramitacoesDeserializer
    {
        private Logger log = new Logger();
        Stopwatch timer = new Stopwatch();

        public void DeserializeTramitacoes(ref ProjetoDetalhado projeto)
        {
            DateTime now = new DateTime();

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            //Mapping objects
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IC_API.Models.Responses.Tramitacoes.Dado, Tramitacao>();
                cfg.CreateMap<IC_API.Models.Responses.ProjetoDetalhado.Dados, ProjetoDetalhado>();
            });
            IMapper mapper = config.CreateMapper();

            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/{projeto.id}/tramitacoes");
                    try
                    {
                        TramitacoesResponse tramitacoes = JsonConvert.DeserializeObject<TramitacoesResponse>(json, settings);
                        foreach (var element in tramitacoes.dados)
                        {
                            if (int.Parse(element.codTipoTramitacao) == 237 || int.Parse(element.codTipoTramitacao) == 238 ||
                            int.Parse(element.codTipoTramitacao) == 240 || int.Parse(element.codTipoTramitacao) == 244 ||
                            int.Parse(element.codTipoTramitacao) == 1235)
                            {
                                projeto.codPlenario = true;
                                projeto.codAprovado = true;
                            }

                            else if (int.Parse(element.codTipoTramitacao) == 129 || int.Parse(element.codTipoTramitacao) == 130 ||
                                int.Parse(element.codTipoTramitacao) == 504)
                            {
                                projeto.apensado = true;
                                break;
                            }

                            else if (int.Parse(element.codTipoTramitacao) == 1231 || int.Parse(element.codTipoTramitacao) == 231 ||
                                         int.Parse(element.codTipoTramitacao) == 232 || int.Parse(element.codTipoTramitacao) == 233)
                            {
                                projeto.codPlenario = true;
                                projeto.codAprovado = false;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        log.LogIt("Could not parse tramitação of response: " + projeto.id + " to object type of ProjetoDetalhado " + "error: " + e.Message);
                    }
                }
                catch (Exception e)
                {
                    log.LogIt("Could not connect to the URL of the the projeto: " + projeto.id + " " + e.Message);
                }
            }

            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();
            now = DateTime.Now;

            //log.LogIt(" Tramitações was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);
        }
    }
}
