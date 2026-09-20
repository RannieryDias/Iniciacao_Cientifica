using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Requests.Exceptions;
using Requests.SerializerNewArchtecture;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Proposicao = Requests.DTO.Proposicao.Dado;
using ProposicaoResponse = Requests.DTO.Proposicao.Root;

namespace Requests
{
    public class EntryPoint
    {
        static bool lastPageHit = false;

        static async Task Main(string[] args)
        {
            int pageIndex = 15;
            string PROJETO_URL = $"https://dadosabertos.camara.leg.br/api/v2/proposicoes?pagina={pageIndex}&itens=100&ordem=ASC&ordenarPor=id";

            List<Proposicao> _proposicoes = new List<Proposicao>();

            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // Registra o HttpClient tipado para IApiClient
                    services.AddHttpClient<ApiClient, ApiClient>(client =>
                    {
                        client.BaseAddress = new Uri(PROJETO_URL);
                        client.DefaultRequestHeaders.Accept.Add(
                            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    });
                })
                .Build();

            // Obtém uma instância do serviço
            ApiClient apiClient = host.Services.GetRequiredService<ApiClient>();

            // Usa o client
            try
            {
                var proposicaoResponse = await apiClient.GetAsync<ProposicaoResponse>(PROJETO_URL);

                _proposicoes.AddRange(proposicaoResponse.dados);

                Console.WriteLine($"Produto: {proposicaoResponse.dados.Count}");
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            // O host é descartado automaticamente ao final do bloco 'using'

            //await FetchEntitiesAsync(PROJETO_URL, pageIndex);

            //var proposicoesList = await FetchProposicoesAsync(PROJETO_URL, pageIndex);

            //await SerializeProposicao(proposicoesList);
        }

        private static async Task TaskFetchProjetos(string url, int pageIndex)
        {
            //EntityFetcher<Proposicao> entityFetcher = new EntityFetcher<Proposicao>();
        }

        private static async Task FetchEntitiesAsync(string url, int pageIndex)
        {
            //const string PROJETO_URL = "https://dadosabertos.camara.leg.br/api/v2/proposicoes?pagina=&itens=100&ordem=ASC&ordenarPor=id";

            //EntityFetcher<Proposicao> entityFetcher = new EntityFetcher<Proposicao>();
            //RequestEntityType entityType = RequestEntityType.PROPOSICOES;

            //var opa = await entityFetcher.FetchEntitiesAsync(url: url, pageIndex: pageIndex, requestEntityType: entityType, threads: 2);
        }

        private static async Task<List<Proposicao>> FetchProposicoesAsync(string PROJETO_URL, int pageIndex)
        {
            List<Proposicao> proposicoesList = new List<Proposicao>();
            while (!lastPageHit)
            {
                (List<Proposicao> proposicoesListAux, lastPageHit) = await DeserializeProjetosTesteAsync(url: PROJETO_URL, pageIndex, threads: 5);
                proposicoesList.AddRange(proposicoesListAux);
            }

            return proposicoesList;
        }

        public static async Task<Tuple<List<Proposicao>, bool>> DeserializeProjetosTesteAsync(string url,
                int pageIndex, int threads = 1)
        {
            bool isLastPage = false;
            object lockObject = new object();
            ConcurrentBag<List<Proposicao>> proposicoesBag = new ConcurrentBag<List<Proposicao>>();
            List<Task> tasks = new List<Task>();
            SemaphoreSlim semaphore = new SemaphoreSlim(1);

            for (int i = 0; i < threads; i++)
            {
                if (isLastPage)
                {
                    break;
                }

                int localPageIndex = 0;
                await semaphore.WaitAsync();

                try
                {
                    localPageIndex = pageIndex;
                    pageIndex++;
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.Message);
                }
                finally
                {
                    semaphore.Release();
                }

                tasks.Add(Task.Run(async () =>
                {
                    var proposicao = await DeserializeProjetosAsync(GetFormatedString(localPageIndex, url));
                    await Console.Out.WriteLineAsync($"Index: {localPageIndex}");
                    if (!isLastPage) { isLastPage = proposicao.Count < 100; }
                    proposicoesBag.Add(proposicao);
                }));
            }

            await Task.WhenAll(tasks);

            return Tuple.Create(proposicoesBag.SelectMany(x => x)
                                 .ToList()
                                 , isLastPage);
        }

        private static async Task<List<Proposicao>> DeserializeProjetosAsync(string url)
        {
            List<Proposicao> proposicoesList;

            //TODO: log the current page that is being requested
            //using (var httpClient = new HttpClient())
            //{
            //    var ProposicoesResponse = await DeserializeListAsync<ProposicaoResponse>(url);
            //    proposicoesList = ProposicoesResponse.Select(x => x.dados)
            //                                         .First()
            //                                         .ToList();
            //}

            //return proposicoesList;

            return default;
        }

        static async Task SerializeProposicao(List<Proposicao> proposicoesList) => await NewSerializer.SerializeEntityAsync(proposicoesList);

        private static string GetFormatedString(int i, string url)
        {
            int indexOfPage = url.IndexOf("pagina=") + "pagina=".Length;
            return url.Substring(0, indexOfPage) + i + url.Substring(indexOfPage);
        }
    }
}
