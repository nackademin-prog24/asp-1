﻿using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context)
{
    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        var entities = await _table
            .Include(x => x.Client)
            .Include(x => x.User)
                .ThenInclude(x => x.Address)
            .Include(x => x.Status)
            .ToListAsync();

        return entities;
    }

    public override async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _table
            .Include(x => x.Client)
            .Include(x => x.User)
                .ThenInclude(x => x.Address)
            .Include(x => x.Status)
            .FirstOrDefaultAsync(expression);

        return entity;
    }
}
