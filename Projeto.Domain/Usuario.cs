using System;

namespace Projeto.Domain;

/// <summary>
/// Entidade de domínio para Usuário.
/// </summary>
public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataAtualizacao { get; set; }
    public bool Ativo { get; set; } = true;
    public bool Excluido { get; set; } = false;
} 