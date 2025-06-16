using Projeto.Domain;
using Projeto.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Projeto.Infrastructure;

/// <summary>
/// Implementação da Unit of Work.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ProjetoDbContext _context;
    private IGenericRepository<Usuario>? _usuarios;

    public UnitOfWork(ProjetoDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<Usuario> Usuarios => _usuarios ??= new UsuarioRepository(_context);

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
} 