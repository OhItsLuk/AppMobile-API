using Projeto.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Projeto.Infrastructure.Repositories;

/// <summary>
/// Repositório específico para Produto.
/// </summary>
public class ProdutoRepository : GenericRepository<Produto>, IGenericRepository<Produto>
{
    public ProdutoRepository(ProjetoDbContext context) : base(context) { }

    // Métodos específicos para produto podem ser adicionados aqui
} 