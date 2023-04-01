using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Requests.DeserializerNewArchtecture;
using Requests.SerializerNewArchtecture;
using ProposicaoResponse = Requests.DTO.Proposicao.Root;
using Proposicao = Requests.DTO.Proposicao.Dado;
using AutoMapper.Configuration.Conventions;
using System.Threading;
using System.Collections.Concurrent;

namespace Requests
{
    public class EntryPoint
    {
        static async Task Main(string[] args)
        {
            var proposicoesList = await DeserializeProjetosAsync(5);

            await SerializeProposicao(proposicoesList);
        }

        public static async Task<List<Proposicao>> DeserializeProjetosAsync(int threads = 1)
        {
            ConcurrentBag<List<Proposicao>> proposicoesBag = new ConcurrentBag<List<Proposicao>>();
            List<Task> tasks = new List<Task>();

            for (int i = 1; i <= threads; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var proposicao = await DeserializeProjetosAsync(i, "https://dadosabertos.camara.leg.br/api/v2/proposicoes?pagina=",
                                                                                                           "&itens=100&ordem=ASC&ordenarPor=id");
                    proposicoesBag.Add(proposicao);
                }));
            }

            await Task.WhenAll(tasks);

            return proposicoesBag.SelectMany(x => x)
                                 .ToList();
        }

        private static async Task<List<Proposicao>> DeserializeProjetosAsync(int index = 1, params string[] urls)
        {
            List<Proposicao> proposicoesList;

            using (var httpClient = new HttpClient())
            {
                string url = urls.Length > 1 ? $"{urls[0]}{index}{urls[1]}"
                                             : urls.ToString();

                var ProposicoesResponse = await NewDeserializer.DeserializeListAsync<ProposicaoResponse>(url, httpClient);
                proposicoesList = ProposicoesResponse.Select(x => x.dados)
                                                         .First()
                                                         .ToList();

            }

            return proposicoesList;
        }

        static async Task SerializeProposicao(List<Proposicao> proposicoesList) => await NewSerializer.SerializeEntityAsync(proposicoesList);

    }
}
