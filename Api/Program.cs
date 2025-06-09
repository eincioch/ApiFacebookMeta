using Api.Filter;
using ApiFacebook.Dominio.Contracts;
using Aplicacion.Services;
using Dominio.Contracts;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración de dependencias
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuración de Swagger para manejar archivos en formularios
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Facebook API", Version = "v1" });

    // Configuración para manejar archivos en formularios
    c.OperationFilter<SwaggerFileOperationFilter>();
});

//builder.Services.AddSwaggerGen();

// Configurar límites para la subida de archivos
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10 MB
});

// Inyección de dependencias personalizada
builder.Services.AddHttpClient<AnuncioRepository>(); // Registro de HttpClient para AnuncioRepository
builder.Services.AddScoped<AnuncioService>();
builder.Services.AddScoped<IAnuncioRepository, AnuncioRepository>();

builder.Services.AddHttpClient<FacebookAuthRepository>(); // Registro de HttpClient para FacebookAuthService
builder.Services.AddScoped<FacebookAuthAppService>();
builder.Services.AddScoped<IFacebookAuthRepository, FacebookAuthRepository>();

var app = builder.Build();

// Configuración del pipeline HTTP (si aplica)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();