using FluentValidation;
using Projeto.Application.DTOs;

namespace Projeto.Application.Validators;

/// <summary>
/// Validador para recuperação de senha de usuário.
/// </summary>
public class UsuarioRecuperarSenhaValidator : AbstractValidator<UsuarioRecuperarSenhaDto>
{
    public UsuarioRecuperarSenhaValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("E-mail inválido.");
    }
} 