namespace Projeto.Application.DTOs;

/// <summary>
/// DTO para login de usuário.
/// </summary>
public class UsuarioLoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
} 