using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Projeto.Domain;

/// <summary>
/// Interface de repositório genérico para entidades.
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
/// <typeparam name="TKey">Tipo da chave primária.</typeparam>
public interface IGenericRepository<TEntity, TKey> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<IEnumerable<TEntity>> GetAllAsync(int page = 1, int pageSize = 20);
    Task<IEnumerable<TEntity>> GetPagedAsync(int page = 1, int pageSize = 20);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
} 