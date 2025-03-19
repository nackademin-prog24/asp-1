using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class ClientInformationRepository(DataContext context) : BaseRepository<ClientInformationEntity>(context)
{
}
