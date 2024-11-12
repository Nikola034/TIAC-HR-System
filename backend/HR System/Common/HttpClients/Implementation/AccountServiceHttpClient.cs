using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Common.HttpCLients;

namespace Common.HttpClients.Implementation
{
    public class AccountServiceHttpClient : IAccountServiceHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountServiceHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> DeleteEmployeeAccount(Guid accountId, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient("AccountServiceClient");
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("auth/" + accountId, UriKind.Relative)
            };
            var response = await httpClient.SendAsync(request, cancellationToken);

            return response.StatusCode != System.Net.HttpStatusCode.InternalServerError;

        }
    }
}
