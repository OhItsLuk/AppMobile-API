using System;

namespace Projeto.Application.DTOs;

/// <summary>
/// DTO para exibição de dados do usuário.
/// </summary>
public class UsuarioDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Ativo { get; set; }
} 