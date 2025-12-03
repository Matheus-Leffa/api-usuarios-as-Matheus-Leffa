using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Application.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioUpdateDtoValidator>();

var app = builder.Build();

// Get para listar todos os usuários
app.MapGet("/usuarios", async (IUsuarioService service, CancellationToken ct) => {
    var usuarios = await service.ListarAsync(ct);
    return Results.Ok(usuarios);
});

// Get para listar usuario por id
app.MapGet("/usuarios/{id:int}", async (int id, IUsuarioService service, CancellationToken ct) =>
{
    var usuario = await service.ObterAsync(id, ct);
    return usuario != null ? Results.Ok(usuario) : Results.NotFound(new { message = $"Usuário com id {id} não foi encontrado." });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
