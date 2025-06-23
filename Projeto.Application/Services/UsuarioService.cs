using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Projeto.Application.DTOs;
using Projeto.Application.Interfaces;
using Projeto.Domain;
using Projeto.Infrastructure.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
    private readonly IConfiguration _configuration;

    public UsuarioService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<UsuarioCreateDto> createValidator,
        IValidator<UsuarioUpdateDto> updateValidator,
        IValidator<UsuarioLoginDto> loginValidator,
        IValidator<UsuarioRecuperarSenhaDto> recuperarSenhaValidator,
        IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _loginValidator = loginValidator;
        _recuperarSenhaValidator = recuperarSenhaValidator;
        _configuration = configuration;
    }

    public async Task<UsuarioDto> RegistrarAsync(UsuarioCreateDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        // Verificar se email já existe
        var usuariosExistentes = await _unitOfWork.Usuarios.FindAsync(u => u.Email == dto.Email);
        if (usuariosExistentes.Any())
            throw new ArgumentException("E-mail já está em uso.");

        var usuario = _mapper.Map<Usuario>(dto);
        usuario.SenhaHash = PasswordHasher.Hash(dto.Senha);

        await _unitOfWork.Usuarios.AddAsync(usuario);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<UsuarioDto>(usuario);
    }

    public async Task<string> LoginAsync(UsuarioLoginDto dto)
    {
        await _loginValidator.ValidateAndThrowAsync(dto);

        // Buscar usuário por email
        var usuarios = await _unitOfWork.Usuarios.FindAsync(u => u.Email == dto.Email);
        var usuario = usuarios.FirstOrDefault();

        if (usuario == null || !usuario.Ativo || usuario.Excluido)
            throw new UnauthorizedAccessException("Credenciais inválidas.");

        // Verificar senha - usar dto.Senha ao invés de dto.Password
        if (!PasswordHasher.Verify(dto.Senha, usuario.SenhaHash))
            throw new UnauthorizedAccessException("Credenciais inválidas.");

        // Gerar JWT
        return GenerateJwtToken(usuario);
    }

    public async Task LogoutAsync(Guid usuarioId)
    {
        // Implementação básica - em um cenário real, você poderia invalidar o token
        // ou manter uma blacklist de tokens
        await Task.CompletedTask;
    }

    public async Task RecuperarSenhaAsync(UsuarioRecuperarSenhaDto dto)
    {
        await _recuperarSenhaValidator.ValidateAndThrowAsync(dto);
        
        var usuarios = await _unitOfWork.Usuarios.FindAsync(u => u.Email == dto.Email);
        var usuario = usuarios.FirstOrDefault();

        if (usuario == null || !usuario.Ativo || usuario.Excluido)
            return; // Por segurança, não informar se o e-mail existe

        // Aqui você implementaria o envio de e-mail de recuperação
        // Por enquanto, apenas registra que foi solicitado
        await Task.CompletedTask;
    }

    public async Task<UsuarioDto?> BuscarPorIdAsync(Guid id)
    {
        var usuario = await _unitOfWork.Usuarios.GetByIdAsync(id);
        return usuario != null && !usuario.Excluido ? _mapper.Map<UsuarioDto>(usuario) : null;
    }

    public async Task<IEnumerable<UsuarioDto>> ListarAsync(int page = 1, int pageSize = 20)
    {
        var usuarios = await _unitOfWork.Usuarios.GetPagedAsync(page, pageSize);
        var usuariosAtivos = usuarios.Where(u => !u.Excluido);
        return _mapper.Map<IEnumerable<UsuarioDto>>(usuariosAtivos);
    }

    public async Task AtualizarAsync(Guid id, UsuarioUpdateDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var usuario = await _unitOfWork.Usuarios.GetByIdAsync(id);
        if (usuario == null || usuario.Excluido)
            throw new ArgumentException("Usuário não encontrado.");

        // UsuarioUpdateDto só tem Nome e Senha, não tem Email
        _mapper.Map(dto, usuario);
        
        // Se uma nova senha foi fornecida, fazer hash
        if (!string.IsNullOrEmpty(dto.Senha))
        {
            usuario.SenhaHash = PasswordHasher.Hash(dto.Senha);
        }
        
        usuario.DataAtualizacao = DateTime.UtcNow;

        _unitOfWork.Usuarios.Update(usuario);
        await _unitOfWork.SaveAsync();
    }

    public async Task RemoverAsync(Guid id)
    {
        var usuario = await _unitOfWork.Usuarios.GetByIdAsync(id);
        if (usuario == null || usuario.Excluido)
            throw new ArgumentException("Usuário não encontrado.");

        // Soft delete
        usuario.Excluido = true;
        usuario.DataAtualizacao = DateTime.UtcNow;

        _unitOfWork.Usuarios.Update(usuario);
        await _unitOfWork.SaveAsync();
    }

    private string GenerateJwtToken(Usuario usuario)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

        var claims = new[]
        {
            new Claim("sub", usuario.Id.ToString()),
            new Claim("email", usuario.Email),
            new Claim("name", usuario.Nome),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, 
                new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), 
                ClaimValueTypes.Integer64)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpireMinutes"]!)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
} 