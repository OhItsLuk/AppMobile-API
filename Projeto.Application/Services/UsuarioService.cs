using AutoMapper;
using FluentValidation;
using Projeto.Application.DTOs;
using Projeto.Application.Interfaces;
using Projeto.Domain;
using Projeto.Infrastructure.Services;

namespace Projeto.Application.Services;

/// <summary>
/// Serviço de aplicação para usuário.
/// </summary>
public class UsuarioService : IUsuarioService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<UsuarioCreateDto> _createValidator;
    private readonly IValidator<UsuarioUpdateDto> _updateValidator;
    private readonly IValidator<UsuarioLoginDto> _loginValidator;
    private readonly IValidator<UsuarioRecuperarSenhaDto> _recuperarSenhaValidator;

    public UsuarioService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<UsuarioCreateDto> createValidator,
        IValidator<UsuarioUpdateDto> updateValidator,
        IValidator<UsuarioLoginDto> loginValidator,
        IValidator<UsuarioRecuperarSenhaDto> recuperarSenhaValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _loginValidator = loginValidator;
        _recuperarSenhaValidator = recuperarSenhaValidator;
    }

    // Métodos a serem implementados...
    public Task<UsuarioDto> RegistrarAsync(UsuarioCreateDto dto) => throw new NotImplementedException();
    public Task<string> LoginAsync(UsuarioLoginDto dto) => throw new NotImplementedException();
    public Task LogoutAsync(Guid usuarioId) => throw new NotImplementedException();
    public Task RecuperarSenhaAsync(UsuarioRecuperarSenhaDto dto) => throw new NotImplementedException();
    public Task<UsuarioDto?> BuscarPorIdAsync(Guid id) => throw new NotImplementedException();
    public Task<IEnumerable<UsuarioDto>> ListarAsync(int page = 1, int pageSize = 20) => throw new NotImplementedException();
    public Task AtualizarAsync(Guid id, UsuarioUpdateDto dto) => throw new NotImplementedException();
    public Task RemoverAsync(Guid id) => throw new NotImplementedException();
} 