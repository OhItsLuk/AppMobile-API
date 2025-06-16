# Projeto API

API RESTful desenvolvida em .NET 8 com Clean Architecture, PostgreSQL, autenticação JWT, validação, logging e documentação interativa.

## Sumário

- [Visão Geral](#visão-geral)
- [Arquitetura](#arquitetura)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Configuração do Projeto](#configuração-do-projeto)
- [Banco de Dados e Migrations](#banco-de-dados-e-migrations)
- [Execução Local](#execução-local)
- [Testes de Endpoints (Swagger)](#testes-de-endpoints-swagger)
- [Estrutura de Pastas](#estrutura-de-pastas)
- [Exemplo de Uso dos Endpoints](#exemplo-de-uso-dos-endpoints)
- [Boas Práticas e Segurança](#boas-práticas-e-segurança)

---

## Visão Geral

Esta é a ProjetoAPI API base com gerenciamento de usuários, com autenticação segura, CRUD completo, soft delete, paginação, logging, documentação e tratamento global de erros.

## Arquitetura

- **Clean Architecture**: Separação em camadas (Domain, Application, Infrastructure, Presentation).
- **SOLID, DRY, KISS**: Código limpo, modular e fácil de manter.

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (PostgreSQL, Code First, Migrations)
- AutoMapper
- FluentValidation
- Serilog
- JWT (JSON Web Token)
- Swagger (OpenAPI)
- BCrypt (hash de senha)

## Configuração do Projeto

1. **Pré-requisitos:**

   - [.NET 8 SDK](https://dotnet.microsoft.com/download)
   - [PostgreSQL](https://www.postgresql.org/download/)

2. **Configuração do banco:**

   - Crie um banco chamado `nomebanco` no PostgreSQL.
   - Ajuste a string de conexão em `Projeto.Api/appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=nomebanco;Username=postgres;Password=SuaSenhaAqui"
     }
     ```

3. **Secrets (opcional):**
   - Para segredos sensíveis, use [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets):
     ```bash
     dotnet user-secrets init --project Projeto.Api
     dotnet user-secrets set "Jwt:Key" "SUA_CHAVE_FORTE_AQUI" --project Projeto.Api
     ```

## Banco de Dados e Migrations

- Para criar e atualizar o banco:
  ```bash
  dotnet ef database update --project Projeto.Infrastructure --startup-project Projeto.Api
  ```
- Para criar novas migrations:
  ```bash
  dotnet ef migrations add NOME_DA_MIGRATION --project Projeto.Infrastructure --startup-project Projeto.Api
  ```

## Execução Local

1. Compile a solution:
   ```bash
   dotnet build
   ```
2. Execute a API:
   ```bash
   dotnet run --project Projeto.Api
   ```
3. Acesse a documentação interativa em:
   - [https://localhost:5001/swagger](https://localhost:5001/swagger)

## Testes de Endpoints (Swagger)

- Todos os endpoints estão documentados e podem ser testados via Swagger UI.
- Exemplos:
  - **POST /api/auth/login**: Login e obtenção de JWT
  - **POST /api/usuarios**: Cadastro de usuário
  - **GET /api/usuarios**: Listagem paginada (requer JWT)
  - **PUT /api/usuarios/{id}**: Atualização
  - **DELETE /api/usuarios/{id}**: Soft delete

## Estrutura de Pastas

```
backend/
  Projeto.Api/           # Presentation (Controllers, Middlewares, Program.cs)
  Projeto.Domain/        # Entidades e contratos de domínio
  Projeto.Application/   # DTOs, serviços, validações, mapeamentos
  Projeto.Infrastructure/ # EF Core, repositórios, UnitOfWork, serviços infra
```

## Exemplo de Uso dos Endpoints

### Cadastro de Usuário

```http
POST /api/usuarios
Content-Type: application/json
{
  "nome": "João Silva",
  "email": "joao@email.com",
  "senha": "123456"
}
```

### Login

```http
POST /api/auth/login
Content-Type: application/json
{
  "email": "joao@email.com",
  "senha": "123456"
}
```

### Listagem de Usuários (com JWT)

```http
GET /api/usuarios?page=1&pageSize=10
Authorization: Bearer {seu_token_jwt}
```

## Boas Práticas e Segurança

- Senhas nunca são armazenadas em texto puro (BCrypt).
- JWT seguro e com expiração configurável.
- Validação centralizada com FluentValidation.
- Logging detalhado com Serilog.
- Tratamento global de erros com middleware customizado.
- Soft delete para usuários.

---

**Dúvidas ou sugestões?**
Abra uma issue ou contribua com o projeto!
