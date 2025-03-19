using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class ClientContactInformationRepository(DataContext context) : BaseRepository<ClientContactInformationEntity>(context)
{
}
