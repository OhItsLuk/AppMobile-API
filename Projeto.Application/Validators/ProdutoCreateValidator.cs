using FluentValidation;
using Projeto.Application.DTOs;

namespace Projeto.Application.Validators;

/// <summary>
/// Validador para criação de produto.
/// </summary>
public class ProdutoCreateValidator : AbstractValidator<ProdutoCreateDto>
{
    public ProdutoCreateValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(200).WithMessage("Nome deve ter no máximo 200 caracteres.");

        RuleFor(x => x.Descricao)
            .MaximumLength(500).WithMessage("Descrição deve ter no máximo 500 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.Descricao));

        RuleFor(x => x.Estoque)
            .GreaterThanOrEqualTo(0).WithMessage("Estoque deve ser maior ou igual a zero.");

        RuleFor(x => x.Preco)
            .GreaterThan(0).WithMessage("Preço deve ser maior que zero.");
    }
} 