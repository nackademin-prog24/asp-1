using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Mappers;

public static class ClientMapper
{
    public static ClientEntity ToEntity(AddClientFormData? formData)
    {
        if (formData == null) return null!;
        return new ClientEntity { ClientName = formData.ClientName };
    }

    public static ClientEntity ToEntity(UpdateClientFormData? formData)
    {
        if (formData == null) return null!;
        return new ClientEntity { Id = formData.Id, ClientName = formData.ClientName };
    }

    public static Client ToModel(ClientEntity? entity)
    {
        if (entity == null) return null!;
        return new Client
        {
            Id = entity.Id,
            ClientName = entity.ClientName
        };
    }
}