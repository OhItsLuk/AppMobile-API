using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Application.DTOs;
using Projeto.Application.Interfaces;

namespace Projeto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public AuthController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    /// <summary>
    /// Realiza login e retorna um token JWT.
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDto dto)
    {
        var token = await _usuarioService.LoginAsync(dto);
        return Ok(new { token });
    }

    /// <summary>
    /// Realiza logout do usuário (exemplo prático, pode ser expandido para blacklist de tokens).
    /// </summary>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var usuarioId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        if (usuarioId == null) return Unauthorized();
        await _usuarioService.LogoutAsync(Guid.Parse(usuarioId));
        return NoContent();
    }

    /// <summary>
    /// Envia e-mail de recuperação de senha (exemplo prático).
    /// </summary>
    [HttpPost("recuperar-senha")]
    [AllowAnonymous]
    public async Task<IActionResult> RecuperarSenha([FromBody] UsuarioRecuperarSenhaDto dto)
    {
        await _usuarioService.RecuperarSenhaAsync(dto);
        return NoContent();
    }
} 