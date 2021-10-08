using AutoMapper;
using IC_API.Models;
using IC_API.Models.Responses.Tramitacoes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Requests.Deserializers
{
    class TramitacoesDeserializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public List<Tramitacao> DeserializeTramitacoes(ref List<ProjetoDetalhado> projDet)
        {
            string pattern = "Aprovada, em";
            string pattern3 = "Aprovada a proposta";
            string pattern4 = "APROVAÇÃO DESTA PROPOSTa";
            string pattern2 = "Rejeitada, em";
            // Create a Regex  
            Regex rg = new Regex(pattern);
            Regex rg2 = new Regex(pattern2);
            Regex rg3 = new Regex(pattern3);
            Regex rg4 = new Regex(pattern4);

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            //Mapping objects
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IC_API.Models.Responses.Tramitacoes.Dado, Tramitacao>()
                                           .ForMember(x => x.Id, opt => opt.Ignore());
            });
            IMapper mapper = config.CreateMapper();

            List<Tramitacao> listaTramitacoes = new List<Tramitacao>();

            timer.Start();
            log.LogIt("***********************************");
            log.LogIt("Started to deserialize Tramitações at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");

            foreach (var projeto in projDet)
            {
                using (var webClient = new System.Net.WebClient())
                {
                    try
                    {
                        string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/{projeto.id}/tramitacoes");
                        try
                        {
                            TramitacoesResponse tramitacoes = JsonConvert.DeserializeObject<TramitacoesResponse>(json, settings);
                            foreach (var response in tramitacoes.dados)
                            {
                                if (rg.IsMatch(response.despacho) || rg3.IsMatch(response.despacho) || rg4.IsMatch(response.despacho))
                                {
                                    projeto.foiAPlenario = true;
                                    projeto.foiAprovado = true;
                                    break;
                                }
                                else if (rg2.IsMatch(response.despacho))
                                {
                                    projeto.foiAPlenario = true;
                                    projeto.foiAprovado = false;
                                }
                                try
                                {
                                    Tramitacao tramitacao = mapper.Map<Tramitacao>(response);
                                    listaTramitacoes.Add(tramitacao);
                                    if (listaTramitacoes.Count % 500 == 0)
                                    {
                                        log.LogIt(listaTramitacoes.Count + " Tramitações deserialized");
                                    }
                                }
                                catch (Exception e)
                                {
                                    log.LogIt("Could not map item tramitação of the projeto id" + projeto.id + " Error message: " + e.Message);
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
            }

            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();

            log.LogIt("The total of " + listaTramitacoes.Count + " Tramitações was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

            return listaTramitacoes;
        }
    }
}
