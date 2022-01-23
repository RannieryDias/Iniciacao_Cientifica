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
    class TramitacoesDeserializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public void DeserializeTramitacoesCod(ref List<ProjetoDetalhado> projDet, List<Tramitacao> listaTramitacoes)
        {
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

            timer.Start();
            now = DateTime.Now;

            log.LogIt("***********************************");
            log.LogIt("Started to deserialize Tramitações at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");

            foreach (var projeto in projDet)
            {
                try
                {
                    foreach (var element in listaTramitacoes)
                    {
                        if (int.Parse(element.codTipoTramitacao) == 237 || int.Parse(element.codTipoTramitacao) == 238 ||
                            int.Parse(element.codTipoTramitacao) == 240 || int.Parse(element.codTipoTramitacao) == 244 ||
                            int.Parse(element.codTipoTramitacao) == 1235)
                        {
                            Console.WriteLine("chegou em aprovado! " + element.codTipoTramitacao + " Id: " + element.projetoId);
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
                            Console.WriteLine("chegou em reprovado! " + element.codTipoTramitacao + " Id: " + element.projetoId);
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

            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();
            now = DateTime.Now;

            log.LogIt("The total of " + listaTramitacoes.Count + " Tramitações was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);
        }

        public List<Tramitacao> DeserializeTramitacoes(List<Projeto> projetos = null)
        {
            List<Tramitacao> listaTramitacoes = new List<Tramitacao>();

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

            timer.Start();
            now = DateTime.Now;

            log.LogIt("***********************************");
            log.LogIt("Started to deserialize Tramitações at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");

            List<int> projetos2 = new List<int>() { 2207241, 2207257, 2207278, 2207311 };

            foreach (var projeto in projetos2)
            {
                using (var webClient = new System.Net.WebClient())
                {
                    try
                    {
                        string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/{projeto}/tramitacoes");
                        //string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/{projeto.id}/tramitacoes");
                        try
                        {
                            TramitacoesResponse tramitacoes = JsonConvert.DeserializeObject<TramitacoesResponse>(json, settings);
                            foreach (var response in tramitacoes.dados)
                            {
                                try
                                {
                                    Tramitacao tramitacao = mapper.Map<Tramitacao>(response);
                                    tramitacao.projetoId = projeto;
                                    //tramitacao.projetoId = projeto.id;

                                    listaTramitacoes.Add(tramitacao);
                                    if (listaTramitacoes.Count % 500 == 0)
                                    {
                                        log.LogIt(listaTramitacoes.Count + " Tramitações deserialized");
                                    }
                                }
                                catch (Exception e)
                                {
                                    log.LogIt("Could not map item tramitação of the projeto id " + projeto + " Error message: " + e.Message);
                                    //log.LogIt("Could not map item tramitação of the projeto id " + projeto.id + " Error message: " + e.Message);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            log.LogIt("Could not parse tramitação of response: " + projeto + "error: " + e.Message);
                            //log.LogIt("Could not parse tramitação of response: " + projeto.id + "error: " + e.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        log.LogIt("Could not parse tramitação of response: " + projeto + "error: " + e.Message);
                        //    log.LogIt("Could not parse tramitação of response: " + projeto.id + "error: " + e.Message);
                        //}
                    }
                }

            }

            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();
            now = DateTime.Now;

            log.LogIt("The total of " + listaTramitacoes.Count + " Tramitações was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);
            return listaTramitacoes;
        }
    }
}
