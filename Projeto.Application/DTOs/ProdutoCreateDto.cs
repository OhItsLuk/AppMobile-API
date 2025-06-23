namespace Projeto.Application.DTOs;

/// <summary>
/// DTO para criação de produto.
/// </summary>
public class ProdutoCreateDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public int Estoque { get; set; }
    public decimal Preco { get; set; }
} 