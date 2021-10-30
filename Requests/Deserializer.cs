using AutoMapper;
using IC_API.Models;
using IC_API.Models.Responses;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Requests
{
    class Deserializer
    {
        Stopwatch timer = new Stopwatch();
        Logger log = new Logger();
        DateTime now = DateTime.Now;

        public List<Projeto> DeserializeProjeto()
        {
            //Mapping objects
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dados, Projeto>();
            });

            List<Projeto> projetos = new List<Projeto>();
            for (int ano = 2004; ano > 1999; ano--)
            {
                using (var webClient = new System.Net.WebClient())
                {
                    now = DateTime.Now;
                    log.LogIt("***********************************");
                    log.LogIt("Started to fetch from year: " + ano + " at: "  + now);
                    log.LogIt("***********************************");
                    log.LogIt("Trying to connect to the URL...");
                    log.LogIt("***********************************");
                    timer.Start();

                    for (int i = 1; ; i++)
                    {
                        try
                        {
                            string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes?siglaTipo=PEC&siglaTipo=PL&siglaTipo=PLP&ano={ano}&pagina={i}&itens=100&ordem=ASC&ordenarPor=id");
                            JObject o = JObject.Parse(json);
                            foreach (var resposta in o.SelectToken("$.dados"))
                            {
                                try
                                {

                                    var pl = resposta.ToObject<Projeto>();

                                    projetos.Add(resposta.ToObject<Projeto>());

                                    if (projetos.Count % 500 == 0)
                                    {
                                        now = DateTime.Now;
                                        log.LogIt(projetos.Count + " Projetos deserialized at " + now);
                                    }
                                }
                                catch
                                {
                                    log.LogIt("Could not parse response: " + resposta + " to object type of Projeto");
                                }
                            }
                            if (o.SelectToken("$.dados").First() == null) { }

                        }
                        catch (Exception e)
                        {
                            log.LogIt("Could not connect to the url: " + e.Message);
                            if (e.Message.Contains("no elements"))
                            {
                                break;
                            }
                            if (e.Message.Contains("404"))
                            {
                                break;
                            }
                        }
                    }
                }
            }

            timer.Stop();
            TimeSpan ts = timer.Elapsed;
            timer.Reset();
            now = DateTime.Now;

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
            now = DateTime.Now;
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
            now = DateTime.Now;

            log.LogIt("The total of " + autores.Count + " Autores was deserialized " + " during " + ts.TotalSeconds + " Seconds. Finished at: " + now);
            return autores;
        }
    }
}
