using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Common.HttpCLients.Implementation
{
    public class AccountHttpClient : IAccountHolidayHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }

        public async Task<bool> DeleteEmployeeAccount(Guid accountId)
        {
            var httpClient = _httpClientFactory.CreateClient("EmployeeHolidayServiceClient");
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = JsonContent.Create(accountId),
                Method = HttpMethod.Delete,
                RequestUri = new Uri("api/accounts", UriKind.Relative)
            };
            var response = await httpClient.SendAsync(request);

            return response.StatusCode != System.Net.HttpStatusCode.InternalServerError;

        }
    }
}
