using Microsoft.EntityFrameworkCore;
using Projeto.Domain;
using Projeto.Infrastructure.Configurations;

namespace Projeto.Infrastructure;

/// <summary>
/// DbContext principal do Projeto.
/// </summary>
public class ProjetoDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; } = null!;

    public ProjetoDbContext(DbContextOptions<ProjetoDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        // Seed de usuário inicial
        modelBuilder.Entity<Usuario>().HasData(new Usuario
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Nome = "Administrador",
            Email = "admin@admin.com",
            SenhaHash = "$2a$11$u1Wc6QwQwQwQwQwQwQwQwOQwQwQwQwQwQwQwQwQwQwQwQwQwQwQw", // senha: admin123
            DataCriacao = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            Ativo = true,
            Excluido = false
        });
        base.OnModelCreating(modelBuilder);
    }
} 