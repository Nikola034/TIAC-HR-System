using Application.Commands;
using Application.Commands.Client;
using Core.Entities;
using ProjectServiceApplication.Commands.Client;
using ProjectServicePresentation.Contracts;

namespace ProjectServicePresentation.Mapper
{
    public static class ClientMapper
    {
        public static ClientByIdResponse ToApiResponse(this Client client)
        {
            return new ClientByIdResponse
            {
                Id = client.Id,
                Name = client.Name,
                Country = client.Country
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
    }
    
}