using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Requests.SerializerNewArchtecture
{
    internal static class NewSerializer
    {
        internal static async Task SerializeEntityAsync<T>(List<T> entities)
        {
            Dictionary<Type, string> entityAPIDict = new Dictionary<Type, string>()
            {
                {   typeof(List<DTO.Proposicao.Dado>),
                    "https://localhost:44378/api/ProjetoDetalhados"
                },

            };

            try
            {
                await PutIntoAPIAsync(entityAPIDict[entities.GetType()], entities);
            }
            catch (Exception ex)
            {
                //TODO: replace to a log
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task PutIntoAPIAsync<T>(string v, List<T> entities)
        {
            Console.WriteLine($"Url: {v}, Entity count: {entities.Count}");
            throw new NotImplementedException();
        }
    }
}
