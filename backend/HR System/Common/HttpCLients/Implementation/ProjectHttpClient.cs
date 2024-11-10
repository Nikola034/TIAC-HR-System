using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.HttpCLients.Implementation
{
    public class ProjectHttpClient : IProjectHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProjectHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<Guid>> GetTeamLeadsForEmployee(Guid employeeId, CancellationToken cancellationToken = default)
        {
            var httpClient = _httpClientFactory.CreateClient("ProjectServiceClient");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("projects/allForEmployee/" + employeeId, UriKind.Relative)
            };
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
    }
}
