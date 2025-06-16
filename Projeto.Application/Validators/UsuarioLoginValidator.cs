using FluentValidation;
using Projeto.Application.DTOs;

namespace Projeto.Application.Validators;

/// <summary>
/// Validador para login de usuário.
/// </summary>
public class UsuarioLoginValidator : AbstractValidator<UsuarioLoginDto>
{
    public UsuarioLoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("E-mail inválido.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.");
    }
} 