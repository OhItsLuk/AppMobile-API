using Projeto.Application.DTOs;

namespace Projeto.Application.Interfaces;

/// <summary>
/// Interface para serviço de aplicação de usuário.
/// </summary>
public interface IUsuarioService
{
    Task<UsuarioDto> RegistrarAsync(UsuarioCreateDto dto);
    Task<string> LoginAsync(UsuarioLoginDto dto);
    Task LogoutAsync(Guid usuarioId);
    Task RecuperarSenhaAsync(UsuarioRecuperarSenhaDto dto);
    Task<UsuarioDto?> BuscarPorIdAsync(Guid id);
    Task<IEnumerable<UsuarioDto>> ListarAsync(int page = 1, int pageSize = 20);
    Task AtualizarAsync(Guid id, UsuarioUpdateDto dto);
    Task RemoverAsync(Guid id);
} 