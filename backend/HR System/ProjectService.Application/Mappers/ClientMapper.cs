using Application.Commands.Client;
using Core.Entities;
using ProjectServiceApplication.Commands.Client;

namespace Application.Mappers;

public static class ClientMapper
{
    public static Client ToDomainEntity(this CreateClientCommand command)
    {
        return new Client
        {
            Name = command.Name,
            Country = command.Country
        };
    }
    
    public static Client ToDomainEntity(this UpdateClientCommand command)
    {
        return new Client
        {
            Id = command.Id,
            Name = command.Name,
            Country = command.Country
        };
    }
}