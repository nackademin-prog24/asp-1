using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class ProjectStatusRepository(DataContext context) : BaseRepository<ProjectStatusEntity>(context)
{
}
