using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class UserAddressRepository(DataContext context) : BaseRepository<UserAddressEntity>(context)
{
}
