# 🔧 DETALLES TÉCNICOS - ApiFacebookMeta

**Documento Técnico Complementario**  
**Versión:** 1.0.0  
**Fecha:** Enero 2024  

---

## 🏗️ ARQUITECTURA IMPLEMENTADA

### Patrón Clean Architecture
```
🔄 Flujo de Datos:
Cliente Web/API → Controllers → Services → Repositories → Facebook API
```

### Inyección de Dependencias Configurada
```csharp
// API Configuration (Program.cs)
builder.Services.AddScoped<AnuncioService>();
builder.Services.AddScoped<IAnuncioRepository, AnuncioRepository>();
builder.Services.AddHttpClient<AnuncioRepository>();

builder.Services.AddScoped<FacebookAuthAppService>();
builder.Services.AddScoped<IFacebookAuthRepository, FacebookAuthRepository>();
builder.Services.AddHttpClient<FacebookAuthRepository>();
```

---

## 📊 ENDPOINTS API IMPLEMENTADOS

### 🔐 FacebookAuth Controller
```http
POST /api/FacebookAuth/renovar-token
Content-Type: application/json
{
  "shortLivedToken": "EAABwzLixnjYBO..."
}
```

```http
POST /api/FacebookAuth/obtener-lista-fanpage
Content-Type: application/json
"EAABwzLixnjYBO...USER_ACCESS_TOKEN"
```

```http
POST /api/FacebookAuth/obtener-token-page
Content-Type: application/json
{
  "userAccessToken": "EAABwzLixnjYBO...",
  "pageId": "123456789"
}
```

### 📝 Anuncios Controller
```http
POST /api/Anuncios/facebook
Content-Type: application/json
{
  "nombre": "Mi Anuncio",
  "adsetId": 123456789,
  "creativeId": "987654321",
  "estado": "ACTIVE"
}
```

```http
POST /api/Anuncios/fanpage/post-texto
Content-Type: application/json
{
  "accessTokenPage": "PAGE_TOKEN",
  "pageId": "123456789",
  "mensaje": "Texto del post"
}
```

```http
POST /api/Anuncios/fanpage/post-personalizado
Content-Type: application/json
{
  "accessTokenPage": "PAGE_TOKEN",
  "pageId": "123456789",
  "mensaje": "Texto del post",
  "link": "https://ejemplo.com",
  "photoId": "PHOTO_ID"
}
```

```http
POST /api/Anuncios/fanpage/upload-image
Content-Type: application/json
{
  "accessTokenPage": "PAGE_TOKEN",
  "imageUrl": "https://ejemplo.com/imagen.jpg",
  "mensaje": "Descripción de la imagen"
}
```

```http
POST /api/Anuncios/fanpage/upload-local-image
Content-Type: multipart/form-data
accessTokenPage: PAGE_TOKEN (query parameter)
mensaje: Descripción (form field)
archivo: [FILE] (form field)
```

---

## 🏛️ ENTIDADES DE DOMINIO

### Anuncio.cs
```csharp
public class Anuncio
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public long AdsetId { get; set; }
    public string CreativeId { get; set; }
    public string Estado { get; set; }
}
```

### FanPage.cs
```csharp
public class FanPage
{
    public string access_token { get; set; }
    public string category { get; set; }
    public List<Category> category_list { get; set; }
    public string name { get; set; }
    public string id { get; set; }
    public List<string> tasks { get; set; }
}

public class Category
{
    public string id { get; set; }
    public string name { get; set; }
}
```

---

## 🔌 CONTRATOS E INTERFACES

### IAnuncioRepository.cs
```csharp
public interface IAnuncioRepository
{
    Task CrearAnuncioFacebookAsync(Anuncio anuncio);
    Task PublicarEnFanpageAsync(string accessTokenPage, string pageId, string mensaje);
    Task PublicarEnFanpageAsync(string accessTokenPage, string pageId, string mensaje, string link, string photoId);
    Task<string> SubirImagenAFanpageAsync(string accessTokenPage, string imageUrl, string mensaje);
    Task<string> SubirImagenLocalAFanpageAsync(string accessTokenPage, Stream imageStream, string fileName, string mensaje);
}
```

### IFacebookAuthRepository.cs
```csharp
public interface IFacebookAuthRepository
{
    Task<string> RenovarTokenLargoPlazoAsync(string shortLivedToken);
    Task<List<FanPage>> ListaPaginasAsync(string userAccessToken);
    Task<string> ObtenerTokenPaginaAsync(string userAccessToken, string pageId);
}
```

---

## 📱 COMPONENTES BLAZOR IMPLEMENTADOS

### Home.razor
- Página principal con información del proyecto
- Enlaces a documentación
- Resumen de características

### Facebook.razor
- Gestión de autenticación
- Renovación de tokens
- Listado de fan pages

### PublicarPost.razor
- Formularios para publicación de posts
- Validación de datos
- Feedback de resultados

### FacebookUpload.razor
- Upload de imágenes desde URL
- Upload de archivos locales
- Preview de imágenes

---

## 🔧 SERVICIOS DE APLICACIÓN

### AnuncioService.cs
```csharp
public class AnuncioService
{
    private readonly IAnuncioRepository _anuncioRepository;

    public async Task CrearAnuncioEnFacebookAsync(Anuncio anuncio);
    public async Task PublicarEnFanpageAsync(string accessTokenPage, string pageId, string mensaje);
    public async Task PublicarEnFanpageAsync(string accessTokenPage, string pageId, string mensaje, string link, string photoId);
    public async Task<string> SubirImagenAFanpageAsync(string accessTokenPage, string imageUrl, string mensaje);
    public async Task<string> SubirImagenLocalAFanpageAsync(string accessTokenPage, Stream imageStream, string fileName, string mensaje);
}
```

### FacebookAuthAppService.cs
```csharp
public class FacebookAuthAppService
{
    private readonly IFacebookAuthRepository _facebookAuthService;

    public async Task<string> RenovarTokenLargoPlazoAsync(string shortLivedToken);
    public async Task<string> ObtenerAccessTokenPageId(string userAccessToken, string pageId);
    public async Task<List<FanPage>> ObtenerListadoFanPage(string userAccessToken);
}
```

---

## 🌐 INTEGRACIÓN FACEBOOK API

### Graph API v23.0
- **Base URL:** `https://graph.facebook.com/v23.0/`
- **Autenticación:** OAuth 2.0 con tokens de acceso
- **Formato:** JSON para requests y responses

### Endpoints Utilizados
```
Token Renewal: /oauth/access_token
User Pages: /me/accounts
Page Posts: /{page-id}/feed
Page Photos: /{page-id}/photos
Ads Creation: /act_{ad-account-id}/ads
```

### Manejo de Errores
```csharp
if (!response.IsSuccessStatusCode)
{
    var errorContent = await response.Content.ReadAsStringAsync();
    throw new Exception($"Error Facebook API: {response.StatusCode} - {errorContent}");
}
```

---

## 📊 DTOs IMPLEMENTADOS

### Request DTOs
```csharp
// Token renovation
public class RenovarTokenDto
{
    public string ShortLivedToken { get; set; }
}

// Page data
public class DataUserDto
{
    public string UserAccessToken { get; set; }
    public string PageId { get; set; }
}

// Text post
public class PublicarFanpageTextoDto
{
    public required string AccessTokenPage { get; set; }
    public required string PageId { get; set; }
    public string Mensaje { get; set; }
}

// Custom post
public class PublicarFanpageDto
{
    public required string AccessTokenPage { get; set; }
    public required string PageId { get; set; }
    public string Mensaje { get; set; }
    public string Link { get; set; }
    public string PhotoId { get; set; }
}

// Image upload
public class SubirImagenDto
{
    public required string AccessTokenPage { get; set; }
    public string ImageUrl { get; set; }
    public string Mensaje { get; set; }
}

// Local file upload
public class SubirImagenLocalFanpageForm
{
    public string? Mensaje { get; set; }
    public IFormFile? Archivo { get; set; }
}
```

---

## 🔐 CONFIGURACIÓN Y SEGURIDAD

### appsettings.json Structure
```json
{
  "Facebook": {
    "AccessToken": "USER_ACCESS_TOKEN",
    "AdAccountId": "AD_ACCOUNT_ID",
    "PageId": "DEFAULT_PAGE_ID",
    "AccessTokenPage": "PAGE_ACCESS_TOKEN"
  }
}
```

### Validaciones Implementadas
- Validación de tokens de acceso
- Verificación de archivos en uploads
- Manejo de errores HTTP
- Sanitización de parámetros

---

## 📚 DOCUMENTACIÓN SWAGGER

### Configuración
```csharp
builder.Services.AddSwaggerGen();

// En el pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

### Acceso
- **URL:** `https://localhost:7296/swagger`
- **Documentación interactiva** de todos los endpoints
- **Ejemplos de request/response**
- **Esquemas de datos**

---

## 🚀 CONFIGURACIÓN DE BLAZOR

### Program.cs Configuration
```csharp
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7296/");
});

builder.Services.AddScoped<SessionState>();
```

### Session State
```csharp
public class SessionState
{
    public string? UserAccessToken { get; set; }
    public string? PageAccessToken { get; set; }
    public List<FanPage>? FanPages { get; set; }
}
```

---

## 🧪 PREPARACIÓN PARA TESTING

### Estructura Sugerida
```
Tests/
├── Unit/
│   ├── Services/
│   │   ├── AnuncioServiceTests.cs
│   │   └── FacebookAuthAppServiceTests.cs
│   ├── Controllers/
│   │   ├── AnunciosControllerTests.cs
│   │   └── FacebookAuthControllerTests.cs
│   └── Repositories/
│       ├── AnuncioRepositoryTests.cs
│       └── FacebookAuthRepositoryTests.cs
├── Integration/
│   ├── Api/
│   │   └── EndpointTests.cs
│   └── Web/
│       └── BlazorComponentTests.cs
└── E2E/
    └── Scenarios/
        └── PublishWorkflowTests.cs
```

---

## 🔄 CI/CD PREPARACIÓN

### Dockerfile (Recomendado)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Api/Api.csproj", "Api/"]
COPY ["WebFacebookAuth/WebFacebookAuth.csproj", "WebFacebookAuth/"]
RUN dotnet restore

COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
```

---

**🏆 IMPLEMENTACIÓN TÉCNICA COMPLETA Y PROFESIONAL**

*Todos los componentes técnicos implementados siguiendo mejores prácticas de desarrollo .NET*