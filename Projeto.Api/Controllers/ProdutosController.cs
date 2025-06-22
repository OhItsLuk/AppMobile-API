using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Application.DTOs;
using Projeto.Application.Interfaces;

namespace Projeto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    /// <summary>
    /// Cadastra um novo produto.
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Criar([FromBody] ProdutoCreateDto dto)
    {
        var produto = await _produtoService.CriarAsync(dto);
        return CreatedAtAction(nameof(BuscarPorId), new { id = produto.Id }, produto);
    }

    /// <summary>
    /// Lista produtos com paginação.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Listar([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var produtos = await _produtoService.ListarAsync(page, pageSize);
        return Ok(produtos);
    }

    /// <summary>
    /// Busca produto por ID.
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var produto = await _produtoService.BuscarPorIdAsync(id);
        if (produto == null) return NotFound();
        return Ok(produto);
    }

    /// <summary>
    /// Atualiza dados do produto.
    /// </summary>
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ProdutoUpdateDto dto)
    {
        await _produtoService.AtualizarAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Remove logicamente (soft delete) um produto.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Remover(int id)
    {
        await _produtoService.RemoverAsync(id);
        return NoContent();
    }
} 