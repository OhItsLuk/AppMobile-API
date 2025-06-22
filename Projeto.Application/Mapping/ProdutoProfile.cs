using AutoMapper;
using Projeto.Domain;
using Projeto.Application.DTOs;

namespace Projeto.Application.Mapping;

/// <summary>
/// Profile do AutoMapper para produto.
/// </summary>
public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<Produto, ProdutoDto>();
        CreateMap<ProdutoCreateDto, Produto>();
        CreateMap<ProdutoUpdateDto, Produto>();
    }
} 