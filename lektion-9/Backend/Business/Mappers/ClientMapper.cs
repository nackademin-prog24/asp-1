using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Mappers;

public static class ClientMapper
{
    public static ClientEntity ToEntity(AddClientForm? formData, string? newImageFileName = null)
    {
        if (formData == null) return null!;
        return new ClientEntity 
        { 
            ImageFileName = newImageFileName,
            ClientName = formData.ClientName 
        };
    }

    public static ClientEntity ToEntity(UpdateClientForm? formData, string? newImageFileName = null)
    {
        if (formData == null) return null!;
        return new ClientEntity 
        { 
            Id = formData.Id, 
            ImageFileName = newImageFileName ?? formData.ImageFileName,
            ClientName = formData.ClientName 
        };
    }

    public static Client ToModel(ClientEntity? entity)
    {
        if (entity == null) return null!;
        return new Client
        {
            Id = entity.Id,
            ImageFileName = entity.ImageFileName,
            ClientName = entity.ClientName
        };
    }
}