namespace Projeto.Application.DTOs;

/// <summary>
/// DTO para atualização de usuário.
/// </summary>
public class UsuarioUpdateDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Senha { get; set; }
} 