using Projeto.Application.DTOs;

namespace Projeto.Application.Interfaces;

/// <summary>
/// Interface para serviço de aplicação de produto.
/// </summary>
public interface IProdutoService
{
    Task<ProdutoDto> CriarAsync(ProdutoCreateDto dto);
    Task<ProdutoDto?> BuscarPorIdAsync(int id);
    Task<IEnumerable<ProdutoDto>> ListarAsync(int page = 1, int pageSize = 20);
    Task AtualizarAsync(int id, ProdutoUpdateDto dto);
    Task RemoverAsync(int id);
} 