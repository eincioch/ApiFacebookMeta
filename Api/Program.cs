using ApiFacebook.Dominio.Contracts;
using Aplicacion.Services;
using Dominio.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Configuración de dependencias
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();
app.MapControllers();

app.Run();