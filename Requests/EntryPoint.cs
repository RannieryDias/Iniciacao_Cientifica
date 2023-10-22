using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Requests.DeserializerNewArchtecture;
using Requests.SerializerNewArchtecture;
using ProposicaoResponse = Requests.DTO.Proposicao.Root;
using Proposicao = Requests.DTO.Proposicao.Dado;
using System.Threading;
using System.Collections.Concurrent;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace Requests
{
    public class EntryPoint
    {
        const string PROJETO_URL = "https://dadosabertos.camara.leg.br/api/v2/proposicoes?pagina=&itens=100&ordem=ASC&ordenarPor=id";

        static async Task Main(string[] args)
        {
            int pageIndex = 125;
            bool lastPageHit = false;
            List<Proposicao> proposicoesList = new List<Proposicao>();
            while (!lastPageHit)
            {
                (List<Proposicao> proposicoesListAux, lastPageHit) = await DeserializeProjetosTesteAsync(PROJETO_URL, pageIndex, 5);
                proposicoesList.AddRange(proposicoesListAux);
            }

            await SerializeProposicao(proposicoesList);
        }

        public static async Task<Tuple<List<Proposicao>, bool>> DeserializeProjetosTesteAsync(string url, int pageIndex, int threads = 1)
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
            using (var httpClient = new HttpClient())
            {
                var ProposicoesResponse = await NewDeserializer.DeserializeListAsync<ProposicaoResponse>(url, httpClient);
                proposicoesList = ProposicoesResponse.Select(x => x.dados)
                                                     .First()
                                                     .ToList();
            }

            return proposicoesList;
        }

        static async Task SerializeProposicao(List<Proposicao> proposicoesList) => await NewSerializer.SerializeEntityAsync(proposicoesList);

        private static string GetFormatedString(int i, string url)
        {
            int indexOfPage = url.IndexOf("pagina=") + "pagina=".Length;
            return url.Substring(0, indexOfPage) + i + url.Substring(indexOfPage);
        }
    }
}
