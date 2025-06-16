namespace Projeto.Application.DTOs;

/// <summary>
/// DTO para criação de usuário.
/// </summary>
public class UsuarioCreateDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
} 