using AutoMapper;
using IC_API.Models;
using IC_API.Models.Responses.ProjetoDetalhado;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Deserializers
{
    class ProjetoDetalhadoDeserializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public List<ProjetoDetalhado> DeserializeProjetoDetalhado_NonIteractive(List<Projeto> projetos)
        {
            SingleProjetoTramitacoesDeserializer singleProjeto = new SingleProjetoTramitacoesDeserializer();

            //Mapping objects
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dados, ProjetoDetalhado>();
                cfg.CreateMap<IC_API.Models.Responses.ProjetoDetalhado.StatusProposicao, IC_API.Models.StatusProposicao>();
            });
            IMapper mapper = config.CreateMapper();

            List<ProjetoDetalhado> projetosDetalhados = new List<ProjetoDetalhado>();

            timer.Start();
            now = DateTime.Now;

            log.LogIt("***********************************");
            log.LogIt("Started to deserialize Proposicoes Detalhadas at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Will deserialize the total of : " + projetos.Count + " Projetos");
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");

            foreach (var projeto in projetos)
            {
                using (var webClient = new System.Net.WebClient())
                {
                    try
                    {
                        string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/{projeto.id}");

                        try
                        {
                            //List<IC_API.Models.StatusProposicao> status = new List<IC_API.Models.StatusProposicao>();

                            ProjetoDetalhadoResponse propo = JsonConvert.DeserializeObject<ProjetoDetalhadoResponse>(json, settings);

                            ProjetoDetalhado projetoDetalhado = mapper.Map<ProjetoDetalhado>(propo.dados);

                            singleProjeto.DeserializeTramitacoes(ref projetoDetalhado);

                            projetosDetalhados.Add(projetoDetalhado);

                            if (projetosDetalhados.Count % 500 == 0)
                            {
                                now = DateTime.Now;
                                log.LogIt(projetosDetalhados.Count + " ProjetosDetalhados deserialized, " + 
                                    (projetos.Count - projetosDetalhados.Count) + " Projetos remaining" + " at " + now);
                            }
                        }
                        catch (Exception e)
                        {
                            log.LogIt("Could not parse response: " + projeto.id + " to object type of ProjetoDetalhado " + "error: " + e.Message);

                        }
                    }
                    catch (Exception e)
                    {
                        log.LogIt("Could not connect to projeto id: " + projeto.id + " " + e.Message);
                    }
                }
            }
            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();
            now = DateTime.Now;

            log.LogIt("The total of " + projetosDetalhados.Count + " ProjetosDetalhados was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

            return projetosDetalhados;
        }

        public List<ProjetoDetalhado> DeserializeProjetoDetalhado_Iteractive()
        {
            //Mapping objects
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dados, ProjetoDetalhado>();
                cfg.CreateMap<IC_API.Models.Responses.ProjetoDetalhado.StatusProposicao, IC_API.Models.StatusProposicao>();
            });
            IMapper mapper = config.CreateMapper();

            List<ProjetoDetalhado> projetosDetalhados = new List<ProjetoDetalhado>();

            timer.Start();
            now = DateTime.Now;

            log.LogIt("***********************************");
            log.LogIt("Started to deserialize Proposicoes Detalhadas at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");

            for (int i = 10000; i <= 15000; i++)
            {
                using (var webClient = new System.Net.WebClient())
                {
                    List<Projeto> projToStatus = new List<Projeto>();
                    try
                    {
                        string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/{i}");

                        try
                        {
                            List<IC_API.Models.StatusProposicao> status = new List<IC_API.Models.StatusProposicao>();

                            ProjetoDetalhadoResponse propo = JsonConvert.DeserializeObject<ProjetoDetalhadoResponse>(json, settings);

                            if (propo.dados.siglaTipo == "PEC" || propo.dados.siglaTipo == "PL" || propo.dados.siglaTipo == "PLP")
                            {
                                //projToStatus.Add(projeto);

                                //status.Add(DeserializeProjetoStatus(projToStatus).First());

                                ProjetoDetalhado aux = mapper.Map<ProjetoDetalhado>(propo.dados);

                                //aux.statusProposicao = status.First();

                                projetosDetalhados.Add(mapper.Map<ProjetoDetalhado>(propo.dados));

                                if (projetosDetalhados.Count % 500 == 0)
                                {
                                    log.LogIt(projetosDetalhados.Count + " ProjetosDetalhados deserialized");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            log.LogIt("Could not parse response: " + i + " to object type of ProjetoDetalhado " + "error: " + e.Message);

                        }
                    }
                    catch (Exception e)
                    {
                        log.LogIt("Could not connect to projeto id: " + i + " " + e.Message);
                    }
                }
            }
            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();
            now = DateTime.Now;

            log.LogIt("The total of " + projetosDetalhados.Count + " ProjetosDetalhados was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

            return projetosDetalhados;
        }

        public List<IC_API.Models.StatusProposicao> DeserializeProjetoStatus(List<Projeto> projetos)
        {
            //Mapping objects
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IC_API.Models.Responses.ProjetoDetalhado.StatusProposicao, IC_API.Models.StatusProposicao>();
            });
            IMapper mapper = config.CreateMapper();



            List<IC_API.Models.StatusProposicao> statusResponseList = new List<IC_API.Models.StatusProposicao>();

            timer.Start();
            if (projetos.Count > 1)
            {
                now = DateTime.Now;
                log.LogIt("***********************************");
                log.LogIt("Started to deserialize StatusPreposicao at: " + now);
                log.LogIt("***********************************");
                log.LogIt("Trying to connect to the URL...");
                log.LogIt("***********************************");
            }

            foreach (var projeto in projetos)
            {
                using (var webClient = new System.Net.WebClient())
                {
                    string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/{projeto.id}");
                    try
                    {
                        ProjetoDetalhadoResponse propo = JsonConvert.DeserializeObject<ProjetoDetalhadoResponse>(json, settings);

                        IC_API.Models.StatusProposicao status = mapper.Map<IC_API.Models.StatusProposicao>(propo.dados.statusProposicao);

                        //status.projetoDetalhado = projeto.id;
                        statusResponseList.Add(status);

                        if (statusResponseList.Count % 500 == 0)
                        {
                            log.LogIt(statusResponseList.Count + " StatusProjeto deserialized");
                        }
                    }
                    catch (Exception e)
                    {
                        log.LogIt("Could not parse response: " + projeto.id + " to object type of StatusProposicao " + " error: " + e.Message);
                    }
                }
            }
            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();

            if (statusResponseList.Count > 1)
            {
                now = DateTime.Now;
                log.LogIt("The total of " + statusResponseList.Count + " StatusProjeto was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);
            }

            return statusResponseList;
        }
    }
}
