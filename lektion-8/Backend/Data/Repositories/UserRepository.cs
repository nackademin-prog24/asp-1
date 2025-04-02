using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context)
{
    public override async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        var entities = await _table
            .Include(x => x.Address)
            .ToListAsync();

        return entities;
    }

    public override async Task<UserEntity?> GetAsync(Expression<Func<UserEntity, bool>> expression)
    {
        var entity = await _table
            .Include(x => x.Address)
            .FirstOrDefaultAsync(expression);

        return entity;
    }
}
