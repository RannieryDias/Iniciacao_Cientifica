using AutoMapper;
using IC_API.Model;
using IC_API.Models;
using IC_API.Models.Responses.Autores_Response;
using IC_API.Models.Responses.Deputado;
using IC_API.Models.Responses.ProjetoDetalhado;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Requests
{
    class Teste
    {
        public void Enviar()
        {
            Logger log = new Logger();
            List<Projeto> projetos = new List<Projeto>();
            Console.WriteLine("Trying to parse to API");

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IC_API.Models.Responses.Autores_Response.Dado, Autor>().ForMember(x => x.id, opt => opt.Ignore());
                cfg.CreateMap<IC_API.Models.Responses.Autores_Response.AutoresResponse, Autores>();
            });
            IMapper mapper = config.CreateMapper();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            List<Autores> autores = new List<Autores>();
            using (var webClient = new System.Net.WebClient())
            {

                string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/204554/autores");
                try
                {
                    List<Autor> aux = new List<Autor>();
                    AutoresResponse autoresResponse = JsonConvert.DeserializeObject<AutoresResponse>(json, settings);
                    foreach (var response in autoresResponse.dados)
                    {
                        Autor auxAutor = mapper.Map<Autor>(response);
                        aux.Add(auxAutor);
                        //autores.Add(aux);
                    }
                    //Autores aux = mapper.Map<Autores>(autoresResponse.dados);

                    //autores.Add();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            //List<Deputado> deputados = new List<Deputado>();

            //using (var webClient = new System.Net.WebClient())
            //{
            //    string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/deputados/204554");
            //    try
            //    {
            //        DeputadoResponse dep = JsonConvert.DeserializeObject<DeputadoResponse>(json);
            //        //for
            //        deputados.Add(mapper.Map<Deputado>(dep.dados));
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //    }
            //}

            //using (var webClient = new System.Net.WebClient())
            //{
            //    string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/576322");
            //    try
            //    {
            //        ProjetoDetalhadoResponse propo = JsonConvert.DeserializeObject<ProjetoDetalhadoResponse>(json, settings);

            //        IC_API.Models.StatusProposicao status = mapper.Map<IC_API.Models.StatusProposicao>(propo.dados.statusProposicao);

            //        //status.projetoDetalhadoId = projeto.id;
            //        statusResponseList.Add(status);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //    }
            //}

            //using (var webClient = new System.Net.WebClient())
            //{
            //    string json = webClient.DownloadString("https://dadosabertos.camara.leg.br/api/v2/proposicoes/2190325");
            //    //JObject o = JObject.Parse(json);
            //    var found = json.IndexOf("dados");
            //    //Console.WriteLine("   {0}", json.Substring(found + 5));
            //    IC_API.Models.Responses.ProjetoDetalhado.StatusProposicao propo = JsonConvert.DeserializeObject<IC_API.Models.Responses.ProjetoDetalhado.StatusProposicao>(json.Substring(found + 5));


            //    //var dst = mapper.Map<ProjetoDetalhado>(propo.dados);

            //    //Console.WriteLine(dst.id);

            //    Console.WriteLine(propo);

            //}

            //int index = 0;
            //string[] elements = new string[14];

            //using (var webClient = new System.Net.WebClient())
            //{
            //    string json = webClient.DownloadString("https://dadosabertos.camara.leg.br/api/v2/proposicoes/2190325");
            //    var o = JObject.Parse(json);
            //    foreach (var resposta in o.SelectTokens("$.dados.statusProposicao"))
            //    {
            //        elements[index] = resposta.ToString();
            //        index++;
            //    }
            //    //elements[elements.Length - 1] = ;
            //}


        }
    }
}
