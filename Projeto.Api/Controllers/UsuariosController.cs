using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Application.DTOs;
using Projeto.Application.Interfaces;

namespace Projeto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    /// <summary>
    /// Cadastra um novo usuário.
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Registrar([FromBody] UsuarioCreateDto dto)
    {
        var usuario = await _usuarioService.RegistrarAsync(dto);
        return CreatedAtAction(nameof(BuscarPorId), new { id = usuario.Id }, usuario);
    }

    /// <summary>
    /// Lista usuários com paginação.
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Listar([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var usuarios = await _usuarioService.ListarAsync(page, pageSize);
        return Ok(usuarios);
    }

    /// <summary>
    /// Busca usuário por ID.
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> BuscarPorId(Guid id)
    {
        var usuario = await _usuarioService.BuscarPorIdAsync(id);
        if (usuario == null) return NotFound();
        return Ok(usuario);
    }

    /// <summary>
    /// Atualiza dados do usuário.
    /// </summary>
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] UsuarioUpdateDto dto)
    {
        await _usuarioService.AtualizarAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Remove logicamente (soft delete) um usuário.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _usuarioService.RemoverAsync(id);
        return NoContent();
    }
} 