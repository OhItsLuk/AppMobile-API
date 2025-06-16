using BCrypt.Net;

namespace Projeto.Infrastructure.Services;

/// <summary>
/// Serviço para hash e verificação de senha usando BCrypt.
/// </summary>
public static class PasswordHasher
{
    public static string Hash(string senha)
    {
        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public static bool Verify(string senha, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(senha, hash);
    }
} 