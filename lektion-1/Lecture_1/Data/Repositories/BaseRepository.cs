﻿using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }


    public virtual async Task<bool> AddAsync(TEntity entity)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(entity);

            _dbSet.Add(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }


    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return entities;
    }


    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entities = await _dbSet.Where(expression).ToListAsync();
        return entities;
    }


    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(expression);
        return entity;
    }


    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        var result = await _dbSet.AnyAsync(expression);
        return result;
    }


    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(entity);

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }


    public virtual async Task<bool> RemoveAsync(TEntity entity)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(entity);

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

}