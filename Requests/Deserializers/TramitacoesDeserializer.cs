using IC_API.Models;
using IC_API.Models.Responses.Tramitacoes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requests.Deserializers
{
    class TramitacoesDeserializer
    {
        public TramitacoesDeserializer()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            using (var webClient = new System.Net.WebClient())
            {

                string json = webClient.DownloadString("https://dadosabertos.camara.leg.br/api/v2/proposicoes/2190325/tramitacoes");
                try
                {
                    TramitacoesResponse tramitacoes = JsonConvert.DeserializeObject<TramitacoesResponse>(json, settings);
                    foreach (var response in tramitacoes.dados)
                    {
                        Console.WriteLine(response.siglaOrgao);
                        //Autor autor = mapper.Map<Autor>(response);
                        //autor.id = projeto.id;
                        //autores.Add(autor);
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
