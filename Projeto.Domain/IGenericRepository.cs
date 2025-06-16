using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Projeto.Domain;

/// <summary>
/// Interface de repositório genérico para entidades.
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync(int page = 1, int pageSize = 20);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
} 