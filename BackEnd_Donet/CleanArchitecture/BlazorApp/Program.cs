using Application.Interfaces;
using Application.Services;
using BlazorApp.Components;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


//--- Registrar HttpClient para llamadas a API
builder.Services.AddHttpClient();


//--- add connection string
builder.Services.AddDbContext<ApplicationDbContext>
        (options => options.UseSqlServer(builder.Configuration.GetConnectionString("_connectionString")));

//--- add services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
