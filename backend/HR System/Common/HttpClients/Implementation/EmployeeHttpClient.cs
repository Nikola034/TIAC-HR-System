using System.Net.Http.Json;
using Common.HttpCLients;
using Newtonsoft.Json.Linq;

namespace Common.HttpClients.Implementation;

public class EmployeeHttpClient : IEmployeeHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EmployeeHttpClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;

    }

    public async Task<string> GetEmployeeByIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        var httpClient = _httpClientFactory.CreateClient("EmployeeServiceClient");
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("employees/" + employeeId, UriKind.Relative)
        };
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetEmployeeRole(Guid employeeAccountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        var httpClient = _httpClientFactory.CreateClient("EmployeeServiceClient");
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("employees/getRole/" + employeeAccountId, UriKind.Relative)
        };
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
        var jsonObj = JObject.Parse(responseString);
        return jsonObj["role"].ToString();
    }
}