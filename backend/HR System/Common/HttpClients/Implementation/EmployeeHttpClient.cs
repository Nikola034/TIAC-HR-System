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
        var response = await httpClient.SendAsync(request,cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<string> GetEmployeeByAccountIdAsync(Guid employeeAccountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        var httpClient = _httpClientFactory.CreateClient("EmployeeServiceClient");
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("employees/getByAccountId/" + employeeAccountId, UriKind.Relative)
        };
        var response = await httpClient.SendAsync(request,cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public async Task<string> GetAllDevelopersAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        var httpClient = _httpClientFactory.CreateClient("EmployeeServiceClient");
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("employees/developers", UriKind.Relative)
        };
        var response = await httpClient.SendAsync(request,cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}