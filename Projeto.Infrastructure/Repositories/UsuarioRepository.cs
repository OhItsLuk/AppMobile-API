using Projeto.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Projeto.Infrastructure.Repositories;

/// <summary>
/// Repositório específico para Usuário.
/// </summary>
public class UsuarioRepository : GenericRepository<Usuario, Guid>, IGenericRepository<Usuario, Guid>
{
    public UsuarioRepository(ProjetoDbContext context) : base(context) { }
} 