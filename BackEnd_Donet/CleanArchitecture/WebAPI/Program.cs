

using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//------------------------------------------------------------------------------------------------------------------

// Registrar la política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200") // Ruta de frontend - Angular
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// Registrar la cadena y la conexión sin Entity Framework Core
builder.Services.AddScoped<SqlConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("_connectionString")));


// Registro de repositorios y servicios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IFacturaService, FacturaService>();

//------------------------------------------------------------------------------------------------------------------



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

//Adicionar la política de CORS ---------------------------------------------------------------------------------
app.UseCors("DevPolicy");

app.MapControllers();

app.Run();
