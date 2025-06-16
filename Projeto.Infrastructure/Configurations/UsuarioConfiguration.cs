using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Domain;

namespace Projeto.Infrastructure.Configurations;

/// <summary>
/// Configuração Fluent API para a entidade Usuario.
/// </summary>
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.SenhaHash)
            .IsRequired();

        builder.Property(u => u.DataCriacao)
            .IsRequired();

        builder.Property(u => u.Ativo)
            .IsRequired();

        builder.Property(u => u.Excluido)
            .IsRequired();
    }
} 