using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Projeto.Infrastructure;
using Projeto.Infrastructure.Repositories;
using Projeto.Domain;
using Projeto.Application.Interfaces;
using Projeto.Application.Services;
using Projeto.Application.Validators;
using Projeto.Application.DTOs;
using FluentValidation;
using Serilog;
using AutoMapper;
using Projeto.Application.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

// DbContext
builder.Services.AddDbContext<ProjetoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// UnitOfWork e Repositórios
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

// AutoMapper
builder.Services.AddAutoMapper(typeof(UsuarioProfile));

// FluentValidation
builder.Services.AddScoped<IValidator<UsuarioCreateDto>, UsuarioCreateValidator>();
builder.Services.AddScoped<IValidator<UsuarioUpdateDto>, UsuarioUpdateValidator>();
builder.Services.AddScoped<IValidator<UsuarioLoginDto>, UsuarioLoginValidator>();
builder.Services.AddScoped<IValidator<UsuarioRecuperarSenhaDto>, UsuarioRecuperarSenhaValidator>();
builder.Services.AddScoped<IValidator<ProdutoCreateDto>, ProdutoCreateValidator>();
builder.Services.AddScoped<IValidator<ProdutoUpdateDto>, ProdutoUpdateValidator>();

// Serviços de aplicação
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

// JWT
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSection["Key"]!);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSection["Issuer"],
        ValidAudience = jwtSection["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Projeto API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Middleware de erros
app.UseMiddleware<Projeto.Api.Middlewares.ErrorHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
