namespace Projeto.Application.DTOs;

/// <summary>
/// DTO para atualização de produto.
/// </summary>
public class ProdutoUpdateDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public int Estoque { get; set; }
} 