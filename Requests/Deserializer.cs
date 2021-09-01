using IC_API.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests
{
    class Deserializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public List<Projeto> DeserializeProjeto()
        {
            List<Projeto> projetos = new List<Projeto>();
            using (var webClient = new System.Net.WebClient())
            {
                log.LogIt("***********************************");
                log.LogIt("Started at: " + now);
                log.LogIt("***********************************");
                log.LogIt("Trying to connect to the URL...");
                log.LogIt("***********************************");
                timer.Start();
                for (int i = 1; i <= 62; i++)
                {
                    try
                    {
                        string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes?pagina={i}&itens=100&ordem=ASC&ordenarPor=id");
                        JObject o = JObject.Parse(json);
                        foreach (var resposta in o.SelectToken("$.dados"))
                        {
                            try
                            {
                                var pl = resposta.ToObject<Projeto>();
                                if (pl.siglaTipo == "PEC" || pl.siglaTipo == "PL" || pl.siglaTipo == "PRC")
                                {
                                    projetos.Add(resposta.ToObject<Projeto>());
                                    if (projetos.Count % 500 == 0)
                                    {
                                        log.LogIt(projetos.Count + " Projetos deserialized");
                                    }
                                }
                            }
                            catch
                            {
                                log.LogIt("Could not parse response: " + resposta + "to object type of Projeto");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        log.LogIt("Could not connect to the url: " + e.Message);
                    }
                }
            }

            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();

            log.LogIt("The total of " + projetos.Count + " Projetos was deserialized" + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

            return projetos;
        }

        //public List<StatusProposicao> DeserializeStatusProposicoes(List<Projeto> projetos)
        //{
        //    List<StatusProposicao> statusProposicoes = new List<StatusProposicao>();
        //    timer.Start();
        //    log.LogIt("***********************************");
        //    log.LogIt("Started to deserialize Status Proposicoes at: " + now);
        //    log.LogIt("***********************************");
        //    log.LogIt("Trying to connect to the URL...");
        //    log.LogIt("***********************************");
        //    foreach (var projeto in projetos)
        //    {
        //        using (var webClient = new System.Net.WebClient())
        //        {

        //            try
        //            {
        //                string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/{projeto.id}");
        //                var o = JObject.Parse(json);

        //                foreach (var resposta in o.SelectToken("$.dados.statusProposicao"))
        //                {
        //                    try
        //                    {
        //                        var aux = resposta.ToObject<StatusProposicao>();
        //                        aux.id = projeto.id;
        //                        statusProposicoes.Add(aux);
        //                        if (statusProposicoes.Count % 500 == 0)
        //                        {
        //                            log.LogIt(statusProposicoes.Count + " Status was deserialized");
        //                        }
        //                    }
        //                    catch
        //                    {
        //                        log.LogIt("Could not parse response: " + resposta + "to object type of Projeto");
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                log.LogIt("Could not connect to the url: " + e.Message);
        //            }
        //        }
        //    }
        //    timer.Stop();
        //    TimeSpan ts = timer.Elapsed;
        //    timer.Reset();

        //    log.LogIt("The total of " + statusProposicoes.Count + " Autores was deserialized " + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);

        //    return statusProposicoes;
        //}

        public List<Autores> DeserializeProjetoAutores(List<Projeto> projetos)
        {
            List<Autores> autores = new List<Autores>();
            timer.Start();
            log.LogIt("***********************************");
            log.LogIt("Started at: " + now);
            log.LogIt("***********************************");
            log.LogIt("Trying to connect to the URL...");
            log.LogIt("***********************************");
            foreach (var projeto in projetos)
            {
                using (var webClient = new System.Net.WebClient())
                {
                    try
                    {
                        string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/{projeto.id}/autores");
                        JObject o = JObject.Parse(json);

                        foreach (var resposta in o.SelectToken("$.dados"))
                        {
                            try
                            {
                                autores.Add(resposta.ToObject<Autores>());
                                if (autores.Count % 500 == 0)
                                {
                                    log.LogIt(autores.Count + " Autores was deserialized");
                                }
                            }
                            catch
                            {
                                log.LogIt("Could not parse response: " + resposta + "to object type of Projeto");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        log.LogIt("Could not connect to the url: " + e.Message);
                    }
                }
            }
            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();

            log.LogIt("The total of " + autores.Count + " Autores was deserialized " + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);
            return autores;
        }
    }
}
