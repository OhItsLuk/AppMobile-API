using System;
using System.Threading.Tasks;

namespace Projeto.Domain;

/// <summary>
/// Interface para Unit of Work.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Usuario> Usuarios { get; }
    Task<int> CommitAsync();
} 