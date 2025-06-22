using Microsoft.EntityFrameworkCore;
using Projeto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Projeto.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório genérico.
/// </summary>
public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
{
    protected readonly ProjetoDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ProjetoDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(int page = 1, int pageSize = 20)
    {
        return await _dbSet.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetPagedAsync(int page = 1, int pageSize = 20)
    {
        return await GetAllAsync(page, pageSize);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
} 