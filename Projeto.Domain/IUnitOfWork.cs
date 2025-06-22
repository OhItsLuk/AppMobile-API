using System;
using System.Threading.Tasks;

namespace Projeto.Domain;

/// <summary>
/// Interface para Unit of Work.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Usuario, Guid> Usuarios { get; }
    IGenericRepository<Produto, int> Produtos { get; }
    Task<int> CommitAsync();
    Task<int> SaveAsync();
} 