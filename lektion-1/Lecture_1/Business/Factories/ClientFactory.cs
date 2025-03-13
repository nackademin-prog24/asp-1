using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ClientFactory
{
    public static ClientRegistrationForm Create() => new();

    public static ClientEntity? Map(ClientRegistrationForm form)
    {
        if (form == null)
            return null!;

        var entity = new ClientEntity()
        {
            ClientName = form.ClientName,
            Email = form.Email,
            Location = form.Location,
            Phone = form.Phone
        };

        return entity;
    }

    public static Client Map(ClientEntity entity)
    {
        if (entity == null) return null!;

        var client = new Client
        {
            Id = entity.Id,
            ClientName = entity.ClientName,
            Email = entity.Email,
            Location = entity.Location,
            Phone = entity.Phone,
            Created = entity.Created,
            IsActive = entity.IsActive,
        };

        return client;
    }
}
