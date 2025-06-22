using System;
using System.Threading.Tasks;

namespace Projeto.Domain;

/// <summary>
/// Interface para Unit of Work.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Usuario> Usuarios { get; }
    IGenericRepository<Produto> Produtos { get; }
    Task<int> CommitAsync();
    Task<int> SaveAsync();
} 