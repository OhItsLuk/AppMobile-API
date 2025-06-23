namespace Projeto.Application.DTOs;

/// <summary>
/// DTO para exibição de dados do produto.
/// </summary>
public class ProdutoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public int Estoque { get; set; }
    public decimal Preco { get; set; }
} 