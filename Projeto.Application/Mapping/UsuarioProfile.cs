using AutoMapper;
using Projeto.Domain;
using Projeto.Application.DTOs;

namespace Projeto.Application.Mapping;

/// <summary>
/// Profile do AutoMapper para usuário.
/// </summary>
public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioDto>();
        CreateMap<UsuarioCreateDto, Usuario>();
        CreateMap<UsuarioUpdateDto, Usuario>();
    }
} 