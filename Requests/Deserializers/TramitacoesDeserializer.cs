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

        string pattern = "Aprovada, em";
        string pattern3 = "Aprovada a proposta";
        string pattern4 = "APROVAÇÃO DESTA PROPOSTa";
        string pattern2 = "Rejeitada, em";
        // Create a Regex  


        public List<Tramitacao> DeserializeTramitacoes(ref List<ProjetoDetalhado> projDet)
        {
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
                cfg.CreateMap<IC_API.Models.Responses.Tramitacoes.Dado, Tramitacao>();
                cfg.CreateMap<IC_API.Models.Responses.ProjetoDetalhado.Dados, ProjetoDetalhado>();
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
                                    tramitacao.id = (int)Int64.Parse(projeto.id.ToString() + tramitacao.sequencia.ToString());
                                    listaTramitacoes.Add(tramitacao);
                                    tramitacao.projetoId = projeto.id;

                                    if (listaTramitacoes.Count % 500 == 0)
                                    {
                                        now = DateTime.Now;
                                        log.LogIt(listaTramitacoes.Count + " Tramitações deserialized at " + now);
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
            now = DateTime.Now;

            DeserializeTramitacoesCod(ref projDet, listaTramitacoes);

            log.LogIt("The total of " + listaTramitacoes.Count + " Tramitações was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);



            return listaTramitacoes;
        }
        public List<Tramitacao> DeserializeTramitacoes(List<Projeto> proj)
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
            });
            IMapper mapper = config.CreateMapper();

            List<Tramitacao> listaTramitacoes = new List<Tramitacao>();

            timer.Start();
            now = DateTime.Now;

            log.LogIt("***********************************");
            log.LogIt("Started to deserialize Tramitações at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");

            foreach (var projeto in proj)
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
                                try
                                {
                                    Tramitacao tramitacao = mapper.Map<Tramitacao>(response);

                                    tramitacao.id = (int)Int64.Parse(projeto.id.ToString() + tramitacao.sequencia.ToString());
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
            now = DateTime.Now;

            log.LogIt("The total of " + listaTramitacoes.Count + " Tramitações was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

            return listaTramitacoes;
        }
        public List<Tramitacao> DeserializeTramitacoesCod(ref List<ProjetoDetalhado> projDet)
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

            List<Tramitacao> listaTramitacoes = new List<Tramitacao>();

            timer.Start();
            now = DateTime.Now;

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
                                if (int.Parse(response.codTipoTramitacao) == 237 || int.Parse(response.codTipoTramitacao) == 238 ||
                                    int.Parse(response.codTipoTramitacao) == 240 || int.Parse(response.codTipoTramitacao) == 244 ||
                                    int.Parse(response.codTipoTramitacao) == 1235)
                                {
                                    projeto.codPlenario = true;
                                    projeto.codAprovado = true;
                                    break;
                                }

                                else if (int.Parse(response.codTipoTramitacao) == 129 || int.Parse(response.codTipoTramitacao) == 130 ||
                                    int.Parse(response.codTipoTramitacao) == 504)
                                {
                                    projeto.apensado = true;
                                    break;
                                }

                                else if (int.Parse(response.codTipoTramitacao) == 1231 || int.Parse(response.codTipoTramitacao) == 231 ||
                                             int.Parse(response.codTipoTramitacao) == 232 || int.Parse(response.codTipoTramitacao) == 233)
                                {
                                    projeto.foiAPlenario = true;
                                    projeto.foiAprovado = false;
                                }
                                try
                                {
                                    Tramitacao tramitacao = mapper.Map<Tramitacao>(response);
                                    tramitacao.id = (int)Int64.Parse(projeto.id.ToString() + tramitacao.sequencia.ToString());
                                    listaTramitacoes.Add(tramitacao);
                                    tramitacao.projetoId = projeto.id;

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
            now = DateTime.Now;

            log.LogIt("The total of " + listaTramitacoes.Count + " Tramitações was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

            return listaTramitacoes;
        }

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
                            projeto.codPlenario = true;
                            projeto.codAprovado = true;
                            break;
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
                            projeto.foiAPlenario = true;
                            projeto.foiAprovado = false;
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
    }
}
