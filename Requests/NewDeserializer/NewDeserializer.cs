using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Requests.DeserializerNewArchtecture
{
    public static class NewDeserializer
    {
        //TODO:
        //Stopwatch timer = new Stopwatch();
        //Logger log = new Logger();
        //DateTime now = DateTime.Now;

        public enum RequestEntityType
        {
            AUTOR,
            DEPUTADO,
            LEGISLATURA,
            MESA,
            PARTIDO,
            PROPOSICOES,
            PROJETOS,
            PROJETO,
            TEMA,
            TRAMITACOES
        }

        /// <summary>
        /// Request and deserialize a GET endpoint and returns a list of the desired entity from the url sent
        /// </summary>
        /// <typeparam name="T">Desired entity</typeparam>
        /// <param name="url">String containing the URL to the REST API request</param>
        /// <param name="httpClient">Instance of the HttpClient</param>
        /// <returns>A list of the desired entity</returns>
        internal static async Task<List<T>> DeserializeListAsync<T>(string url)
        {
            List<T> entities = new List<T>();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    //var entityType = EntityDecider(requestEntityType);

                    var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead);

                    if (response.Content is object && response.Content.Headers.ContentType.MediaType == "application/json")
                    {
                        var contentStream = await response.Content.ReadAsStreamAsync();

                        using var streamReader = new StreamReader(contentStream);
                        using var jsonReader = new JsonTextReader(streamReader);

                        JsonSerializer serializer = new JsonSerializer();

                        try
                        {
                            T entity = serializer.Deserialize<T>(jsonReader);
                            PropertyInfo firstField = entity.GetType()
                                                            .GetProperties()[0];
                            entities.Add(entity);
                            return entities;
                        }
                        catch (JsonReaderException)
                        {
                            Console.WriteLine($"Invalid JSON. at URL: {url}");
                            return entities;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Response at URL: {url} was invalid and cannot be deserialised.");
                        return entities;
                    }
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return entities;
            }
        }
    }
}
