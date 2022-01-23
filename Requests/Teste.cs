using AutoMapper;
using IC_API.Model;
using IC_API.Models;
using IC_API.Models.Responses.Autores_Response;
using IC_API.Models.Responses.Deputado;
using IC_API.Models.Responses.ProjetoDetalhado;
using IC_API.Models.Responses.Tramitacoes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Requests.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Requests
{
    class Teste
    {
        SingleProjetoTramitacoesDeserializer singleProjeto = new SingleProjetoTramitacoesDeserializer();
        public void Enviar()
        {
            Logger log = new Logger();
            List<Projeto> projetos = new List<Projeto>();
            Console.WriteLine("Trying to parse to API");

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IC_API.Models.Responses.Autores_Response.Dado, Autor>().ForMember(x => x.idProjeto, opt => opt.Ignore());
                cfg.CreateMap<IC_API.Models.Responses.Autores_Response.AutoresResponse, Autores>();
            });
            IMapper mapper = config.CreateMapper();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            using (var webClient = new System.Net.WebClient())
            {

                string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/2220292/tramitacoes");
                //string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/1526944/tramitacoes");
                try
                {
                    string pattern = "Aprovada, em";
                    string pattern2 = "Rejeitada, em";
                    // Create a Regex  
                    Regex rg = new Regex(pattern);
                    Regex rg2 = new Regex(pattern2);

                    TramitacoesResponse tramitacoesResponse = JsonConvert.DeserializeObject<TramitacoesResponse>(json, settings);
                    foreach (var response in tramitacoesResponse.dados)
                    {
                        if (rg.IsMatch(response.despacho))
                        {
                            break;
                        }
                        else if (rg2.IsMatch(response.despacho))
                        {
                            Console.WriteLine("Foi triste");
                        }

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }


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


        }
        public List<ProjetoDetalhado> Receber()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            //Mapping objects
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IC_API.Models.Responses.ProjetoDetalhado.Dados, ProjetoDetalhado>();
                cfg.CreateMap<IC_API.Models.Responses.ProjetoDetalhado.StatusProposicao, IC_API.Models.StatusProposicao>();
            });
            IMapper mapper = config.CreateMapper();

            List<ProjetoDetalhado> projetosDetalhados = new List<ProjetoDetalhado>();

            using (var webClient = new System.Net.WebClient())
            {
                string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/2220292");

                try
                {
                    List<IC_API.Models.StatusProposicao> status = new List<IC_API.Models.StatusProposicao>();

                    ProjetoDetalhadoResponse propo = JsonConvert.DeserializeObject<ProjetoDetalhadoResponse>(json, settings);

                    ProjetoDetalhado projeto = mapper.Map<ProjetoDetalhado>(propo.dados);

                    singleProjeto.DeserializeTramitacoes(ref projeto);

                    projetosDetalhados.Add(projeto);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not parse response: " + " to object type of ProjetoDetalhado " + "error: " + e.Message);
                }
            }
            return projetosDetalhados;
        }
    }
}
