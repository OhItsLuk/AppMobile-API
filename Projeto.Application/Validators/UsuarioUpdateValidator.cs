using FluentValidation;
using Projeto.Application.DTOs;

namespace Projeto.Application.Validators;

/// <summary>
/// Validador para atualização de usuário.
/// </summary>
public class UsuarioUpdateValidator : AbstractValidator<UsuarioUpdateDto>
{
    public UsuarioUpdateValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter até 100 caracteres.");

        RuleFor(x => x.Senha)
            .MinimumLength(6).When(x => !string.IsNullOrEmpty(x.Senha))
            .WithMessage("A senha deve ter pelo menos 6 caracteres.");
    }
} 