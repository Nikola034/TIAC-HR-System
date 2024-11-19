using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Common.HttpCLients.Dtos;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Common.HttpCLients.Implementation
{
    public class ProjectHttpClient : IProjectHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProjectHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<Guid>> GetTeamLeadsForEmployeeAsync(Guid employeeId, string token, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient("ProjectServiceClient");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("projects/allForEmployee/" + employeeId, UriKind.Relative)
            };
            request.Headers.Add("Authorization",token);
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

            var teamLeadIds = new List<Guid>();
            var jsonObject = JObject.Parse(responseString);

            foreach (var project in jsonObject["projects"])
            {
                var teamLeadIdString = (string)project["teamLeadId"];
                if(teamLeadIdString != null)
                {
                    Guid teamLeadId = Guid.Parse(teamLeadIdString);
                    teamLeadIds.Add(teamLeadId);
                }
            }

            return teamLeadIds;
        }
        public async Task<IEnumerable<Guid>> GetLeadingProjectIdsForEmployeeAsync(Guid employeeId, string token, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient("ProjectServiceClient");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("projects/without-paging", UriKind.Relative)
            };
            request.Headers.Add("Authorization",token);
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

            var leadingProjectsIds = new List<Guid>();
            var jsonObject = JObject.Parse(responseString);

            foreach (var project in jsonObject["projects"])
            {
                var teamLeadIdString = (string)project["teamLeadId"];
                if (teamLeadIdString != null)
                {
                    Guid teamLeadId = Guid.Parse(teamLeadIdString);
                    if(teamLeadId == employeeId)
                    {
                        Guid projectId = Guid.Parse((string)project["id"]);
                        leadingProjectsIds.Add(projectId);
                    }
                }
            }

            return leadingProjectsIds;
        }
        public async Task<IEnumerable<Guid>> GetProjectsForEmployeeAsync(Guid employeeId, string token, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient("ProjectServiceClient");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("projects/allForEmployee/" + employeeId, UriKind.Relative)
            };
            request.Headers.Add("Authorization",token);
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

            var projectIds = new List<Guid>();
            var jsonObject = JObject.Parse(responseString);

            foreach (var project in jsonObject["projects"])
            {
                var projectIdString = (string)project["id"];
                if (projectIdString != null)
                {
                    Guid projectId = Guid.Parse(projectIdString);
                    projectIds.Add(projectId);
                }
            }

            return projectIds;
        }

        public async Task<bool> RemoveEmployeeFromProjectAsync(RemoveEmployeeFromProjectDto dto, string token, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient("ProjectServiceClient");
            var request = new HttpRequestMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Put,
                RequestUri = new Uri("projects/removeFromProject", UriKind.Relative)
            };
            request.Headers.Add("Authorization",token);
            var response = await httpClient.SendAsync(request, cancellationToken);

            return response.StatusCode != System.Net.HttpStatusCode.InternalServerError;
        }
        public async Task<HttpResponseMessage> GetProjectByIdAsync(Guid projectId, string token, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient("ProjectServiceClient");
            var projectRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("projects/" + projectId, UriKind.Relative)
            };
            projectRequest.Headers.Add("Authorization",token);
            var projectResponse = await httpClient.SendAsync(projectRequest, cancellationToken);
            
            return projectResponse;
        }
        public async Task<bool> RemoveTeamLeadFromProjectAsync(Guid projectId, string token, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient("ProjectServiceClient");
            
            var projectResponse = await GetProjectByIdAsync(projectId, token, cancellationToken);

            var jsonObject = JObject.Parse(await projectResponse.Content.ReadAsStringAsync(cancellationToken));
            jsonObject["teamLeadId"] = JValue.CreateNull();
            jsonObject.Add("clientId", jsonObject["client"]["id"]);
            jsonObject.Remove("client");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                Content = JsonContent.Create(jsonObject),
                RequestUri = new Uri(httpClient.BaseAddress, "projects")
            };
            request.Headers.Add("Authorization",token);
            var response = await httpClient.SendAsync(request, cancellationToken);

            return response.StatusCode != System.Net.HttpStatusCode.InternalServerError;
        }
    }
}
