using AutoMapper;
using FluentValidation;
using Projeto.Application.DTOs;
using Projeto.Application.Interfaces;
using Projeto.Domain;

namespace Projeto.Application.Services;

/// <summary>
/// Serviço de aplicação para produto.
/// </summary>
public class ProdutoService : IProdutoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<ProdutoCreateDto> _createValidator;
    private readonly IValidator<ProdutoUpdateDto> _updateValidator;

    public ProdutoService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<ProdutoCreateDto> createValidator,
        IValidator<ProdutoUpdateDto> updateValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<ProdutoDto> CriarAsync(ProdutoCreateDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var produto = _mapper.Map<Produto>(dto);
        await _unitOfWork.Produtos.AddAsync(produto);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<ProdutoDto>(produto);
    }

    public async Task<ProdutoDto?> BuscarPorIdAsync(int id)
    {
        var produto = await _unitOfWork.Produtos.GetByIdAsync(id);
        return produto != null ? _mapper.Map<ProdutoDto>(produto) : null;
    }

    public async Task<IEnumerable<ProdutoDto>> ListarAsync(int page = 1, int pageSize = 20)
    {
        var produtos = await _unitOfWork.Produtos.GetPagedAsync(page, pageSize);
        return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
    }

    public async Task AtualizarAsync(int id, ProdutoUpdateDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var produto = await _unitOfWork.Produtos.GetByIdAsync(id);
        if (produto == null) throw new ArgumentException("Produto não encontrado.");

        _mapper.Map(dto, produto);
        produto.DataAtualizacao = DateTime.UtcNow;

        _unitOfWork.Produtos.Update(produto);
        await _unitOfWork.SaveAsync();
    }

    public async Task RemoverAsync(int id)
    {
        var produto = await _unitOfWork.Produtos.GetByIdAsync(id);
        if (produto == null) throw new ArgumentException("Produto não encontrado.");

        produto.Excluido = true;
        produto.DataAtualizacao = DateTime.UtcNow;

        _unitOfWork.Produtos.Update(produto);
        await _unitOfWork.SaveAsync();
    }
} 