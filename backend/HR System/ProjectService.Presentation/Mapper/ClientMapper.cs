using Application.Commands;
using Application.Commands.Client;
using Application.Queries.Client;
using Core.Entities;
using ProjectServiceApplication.Commands.Client;
using ProjectServicePresentation.Contracts;

namespace ProjectServicePresentation.Mapper
{
    public static class ClientMapper
    {
        public static ClientByIdResponse ToApiResponse(this GetClientByIdQueryResponse response)
        {
            return new ClientByIdResponse
            {
                Id = response.Client.Id,
                Name = response.Client.Name,
                Country = response.Client.Country,
                Projects = response.Projects
            };
        }
        
        public static CreateClientCommand ToCommand(this CreateClientRequest request) => new CreateClientCommand(request.Name, request.Country);
        
        public static CreateClientResponse ToApiResponseFromCreate(this Client client)
        {
            return new CreateClientResponse()
            {
                Id = client.Id,
                Name = client.Name,
                Country = client.Country
            };
        }
        
        public static UpdateClientCommand ToCommand(this UpdateClientRequest request) => new UpdateClientCommand(request.Id,request.Name, request.Country);
        
        public static UpdateClientResponse ToApiResponseFromUpdate(this Client client)
        {
            return new UpdateClientResponse()
            {
                Id = client.Id,
                Name = client.Name,
                Country = client.Country
            };
        }
        
        public static GetAllClientsResponse ToApiResponse(this GetAllClientsQueryResponse response)
        {
            return new GetAllClientsResponse()
            {
                Clients = response.Clients,
                Page = response.PageNumber,
                ItemsPerPage = response.ItemNumber,
                TotalPages = response.TotalPages
            };
        }
    }
    
}