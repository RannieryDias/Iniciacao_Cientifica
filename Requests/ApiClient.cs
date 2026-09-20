using Microsoft.Extensions.Logging;
using Requests.Exceptions;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Requests
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        // IHttpClientFactory injetará o HttpClient já configurado
        public ApiClient(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            // 1. Faz a requisição GET
            HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);

            // 2. Verifica se a resposta foi sucesso
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogError("Erro ao chamar {Url}: {StatusCode} - {Error}", url, (int)response.StatusCode, errorContent);
                throw new ApiException($"Erro na API: {response.StatusCode}", (int)response.StatusCode, errorContent);
            }

            // 3. Lê o conteúdo como string
            string json = await response.Content.ReadAsStringAsync(cancellationToken);

            // 4. Desserializa para o tipo T
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // Recomendável para APIs
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                };

                T? result = JsonSerializer.Deserialize<T>(json, options);

                if (result is null)
                    throw new ApiException("Resposta da API retornou null inesperadamente.");

                return result;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Falha ao desserializar JSON para {Type}. JSON recebido: {Json}", typeof(T).Name, json);
                throw new ApiException("Formato de resposta inválido.", ex);
            }
        }
    }
}
