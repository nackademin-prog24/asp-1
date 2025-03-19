using Data.Contexts;
using Data.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ClientRepository(DataContext context) : BaseRepository<ClientEntity>(context)
{

    public override Task<IEnumerable<Client>> GetAllAsync()
    {
        _dbSet
            .Include(x => x.ContactInformation)
            .Include(x => x.Address)
            .Select(x => new Client
            {
                x.Id,
                x.ClientName,
                x.Created,
                x.Modified,
                x.IsActive,
                x.ContactInformation.Email,
                x.ContactInformation.Phone,
                x.ContactInformation.Reference,
                x.Address.StreetName,
                x.Address.StreetNumber,
                x.Address.PostalCode,
                x.Address.City
            })
            .ToListAsync();
            

    }
}
