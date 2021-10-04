using IC_API.Models;
using IC_API.Models.Responses.Tramitacoes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Requests.Deserializers
{
    class TramitacoesDeserializer
    {
        public void DeserializeTramitacoes(ref List<ProjetoDetalhado> projDet)
        {
            string pattern = "Aprovada, em";
            string pattern2 = "Rejeitada, em";
            // Create a Regex  
            Regex rg = new Regex(pattern);
            Regex rg2 = new Regex(pattern2);

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            foreach(var projeto in projDet)
            {
                using (var webClient = new System.Net.WebClient())
                {
                    string json = webClient.DownloadString($"https://dadosabertos.camara.leg.br/api/v2/proposicoes/{projeto.id}/tramitacoes");
                    //string json = webClient.DownloadString("https://dadosabertos.camara.leg.br/api/v2/proposicoes/2220292/tramitacoes");
                    try
                    {
                        TramitacoesResponse tramitacoes = JsonConvert.DeserializeObject<TramitacoesResponse>(json, settings);
                        foreach (var response in tramitacoes.dados)
                        {
                            if (rg.IsMatch(response.despacho))
                            {
                                Console.WriteLine("ae caralhaaa");
                                projeto.foiAPlenario = true;
                                projeto.foiAprovado = true;
                                break;
                            }
                            else if (rg2.IsMatch(response.despacho))
                            {
                                Console.WriteLine("Foi triste");
                                projeto.foiAPlenario = true;
                                projeto.foiAprovado = false;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        //log.LogIt("Could not parse autor of response: " + projeto.id + " to object type of ProjetoDetalhado " + "error: " + e.Message);
                    }

                }
            }
        }
    }
}
