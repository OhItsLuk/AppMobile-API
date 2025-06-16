using FluentValidation;
using Projeto.Application.DTOs;

namespace Projeto.Application.Validators;

/// <summary>
/// Validador para criação de usuário.
/// </summary>
public class UsuarioCreateValidator : AbstractValidator<UsuarioCreateDto>
{
    public UsuarioCreateValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter até 100 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("E-mail inválido.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
    }
} 