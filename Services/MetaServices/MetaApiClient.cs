using System.Text;

namespace Spill.Core.Web.Services.MetaServices
{
    public class MetaApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId;
        private readonly string _clientSecret;

        // Centralização das URLs
        private const string BaseUrl = "https://graph.facebook.com";
        private const string AccessTokenEndpoint = "/oauth/access_token";
        private const string DebugTokenEndpoint = "/debug_token";

        public MetaApiClient(HttpClient httpClient, string clientId, string clientSecret)
        {
            _httpClient = httpClient;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        // Método para criar a request
        private HttpRequestMessage CreateRequest(HttpMethod method, string url, string jsonContent = null)
        {
            var request = new HttpRequestMessage(method, url);

            if (!string.IsNullOrEmpty(jsonContent))
            {
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            return request;
        }

        // Método para executar a request (monta a URL completa)
        private async Task<string> ExecuteRequestAsync(HttpMethod method, string resource, string queryParams = "", string jsonContent = null)
        {
            // Monta a URL completa aqui
            var url = $"{BaseUrl}{resource}";

            if (!string.IsNullOrEmpty(queryParams))
            {
                url += $"?{queryParams}";
            }

            // Cria a requisição
            var request = CreateRequest(method, url, jsonContent);

            // Envia a requisição
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // Método GET
        public async Task<string> GetAsync(string resource, string queryParams = "")
        {
            return await ExecuteRequestAsync(HttpMethod.Get, resource, queryParams);
        }

        // Método POST
        public async Task<string> PostAsync(string resource, string jsonContent)
        {
            return await ExecuteRequestAsync(HttpMethod.Post, resource, jsonContent: jsonContent);
        }

        // Método PUT
        public async Task<string> PutAsync(string resource, string jsonContent)
        {
            return await ExecuteRequestAsync(HttpMethod.Put, resource, jsonContent: jsonContent);
        }

        // Método DELETE
        public async Task<string> DeleteAsync(string resource)
        {
            return await ExecuteRequestAsync(HttpMethod.Delete, resource);
        }

        // Método para obter o app_access_token
        public async Task<string> GetAppAccessTokenAsync()
        {
            var queryParams = $"grant_type=client_credentials&client_id={_clientId}&client_secret={_clientSecret}";
            return await GetAsync(AccessTokenEndpoint, queryParams);
        }

        // Método para validar o token do usuário
        public async Task<string> ValidateUserTokenAsync(string userAccessToken)
        {
            var appAccessToken = await GetAppAccessTokenAsync();
            var queryParams = $"input_token={userAccessToken}&access_token={appAccessToken}";
            return await GetAsync(DebugTokenEndpoint, queryParams);
        }

        // Classe auxiliar para desserializar a resposta do token de acesso
        private class AccessTokenResponse
        {
            public string AccessToken { get; set; }
            public string TokenType { get; set; }
        }
    }
}
