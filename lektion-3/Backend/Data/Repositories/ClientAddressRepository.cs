using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class ClientAddressRepository(DataContext context) : BaseRepository<ClientAddressEntity>(context)
{
}
