# 💡 Ejemplos de Uso

Esta guía proporciona ejemplos prácticos de cómo usar ApiFacebookMeta para integrar con la API de Facebook Meta.

## 📋 Tabla de Contenidos

- [Configuración Inicial](#configuración-inicial)
- [Ejemplos de API](#ejemplos-de-api)
- [Ejemplos de Interfaz Web](#ejemplos-de-interfaz-web)
- [Casos de Uso Completos](#casos-de-uso-completos)
- [Mejores Prácticas](#mejores-prácticas)

## 🔧 Configuración Inicial

### Obtener Token de Usuario de Facebook

Antes de usar la API, necesitas obtener un token de acceso de usuario desde Facebook:

1. Ve a [Graph API Explorer](https://developers.facebook.com/tools/explorer/)
2. Selecciona tu aplicación
3. Genera un token con permisos: `pages_manage_posts`, `pages_read_engagement`
4. Usa este token en los ejemplos siguientes

## 🚀 Ejemplos de API

### 1. Renovar Token de Acceso

```bash
# Renovar un token de corta duración
curl -X POST "https://localhost:7296/api/FacebookAuth/renovar-token" \
  -H "Content-Type: application/json" \
  -d '{
    "shortLivedToken": "EAABwzLixnjYBOZCqh4ZCJmqFY..."
  }'
```

**Respuesta:**
```json
{
  "access_token": "EAABwzLixnjYBOZCqh4ZCJmqFY...LONG_LIVED_TOKEN"
}
```

### 2. Obtener Lista de Fan Pages

```bash
# Obtener todas las páginas del usuario
curl -X POST "https://localhost:7296/api/FacebookAuth/obtener-lista-fanpage" \
  -H "Content-Type: application/json" \
  -d '"EAABwzLixnjYBOZCqh4ZCJmqFY...USER_ACCESS_TOKEN"'
```

**Respuesta:**
```json
{
  "fanpages": [
    {
      "access_token": "EAABwzLixnjYBOZCqh4ZCJmqFY...PAGE_TOKEN",
      "category": "Local Business",
      "category_list": [
        {
          "id": "200600219953504",
          "name": "Shopping & Retail"
        }
      ],
      "name": "Mi Tienda Online",
      "id": "123456789012345",
      "tasks": ["ADVERTISE", "ANALYZE", "CREATE_CONTENT", "MANAGE", "MODERATE"]
    }
  ]
}
```

### 3. Publicar Post de Texto Simple

```bash
# Publicar un mensaje simple en la fan page
curl -X POST "https://localhost:7296/api/Anuncios/fanpage/post-texto" \
  -H "Content-Type: application/json" \
  -d '{
    "accessTokenPage": "EAABwzLixnjYBOZCqh4ZCJmqFY...PAGE_TOKEN",
    "pageId": "123456789012345",
    "mensaje": "¡Hola! Este es nuestro primer post desde la API. 🚀"
  }'
```

### 4. Subir y Publicar Imagen

#### Paso 1: Subir imagen desde URL
```bash
curl -X POST "https://localhost:7296/api/Anuncios/fanpage/upload-image" \
  -H "Content-Type: application/json" \
  -d '{
    "accessTokenPage": "EAABwzLixnjYBOZCqh4ZCJmqFY...PAGE_TOKEN",
    "imageUrl": "https://example.com/mi-imagen.jpg",
    "mensaje": "¡Mira esta increíble imagen!"
  }'
```

**Respuesta:**
```json
{
  "photoId": "987654321098765"
}
```

#### Paso 2: Usar la imagen en un post
```bash
curl -X POST "https://localhost:7296/api/Anuncios/fanpage/post-personalizado" \
  -H "Content-Type: application/json" \
  -d '{
    "accessTokenPage": "EAABwzLixnjYBOZCqh4ZCJmqFY...PAGE_TOKEN",
    "pageId": "123456789012345",
    "mensaje": "¡Nueva imagen en nuestra página! 📸",
    "photoId": "987654321098765"
  }'
```

### 5. Subir Imagen Local

```bash
# Subir una imagen desde tu computadora
curl -X POST "https://localhost:7296/api/Anuncios/fanpage/upload-local-image?accessTokenPage=EAABwzLixnjYBOZCqh4ZCJmqFY...PAGE_TOKEN" \
  -F "Archivo=@/ruta/a/tu/imagen.jpg" \
  -F "Mensaje=Imagen subida desde mi computadora"
```

### 6. Post con Enlace

```bash
# Publicar un post con enlace
curl -X POST "https://localhost:7296/api/Anuncios/fanpage/post-personalizado" \
  -H "Content-Type: application/json" \
  -d '{
    "accessTokenPage": "EAABwzLixnjYBOZCqh4ZCJmqFY...PAGE_TOKEN",
    "pageId": "123456789012345",
    "mensaje": "Visita nuestro nuevo sitio web para ofertas exclusivas! 🛍️",
    "link": "https://mitienda.com/ofertas"
  }'
```

## 🖥️ Ejemplos de Interfaz Web

### Acceso a la Aplicación Web

1. Ejecuta la aplicación web: `cd WebFacebookAuth && dotnet run`
2. Navega a: `https://localhost:5001`

### Publicar Post desde la Interfaz

1. **Ve a la página "Publicar Post"**: `/facebook-post`
2. **Completa el formulario**:
   - Access Token Page: Tu token de página
   - Page ID: ID de tu fan page
   - Mensaje: El contenido de tu post
   - Link (opcional): URL para incluir
   - Photo ID (opcional): ID de imagen previamente subida

3. **Haz clic en "Publicar"**

### Ejemplo de Formulario Completo

```html
<!-- Ejemplo de datos para el formulario web -->
Access Token Page: EAABwzLixnjYBOZCqh4ZCJmqFY...PAGE_TOKEN
Page ID: 123456789012345
Mensaje: ¡Oferta especial! 50% de descuento en todos nuestros productos
Link: https://mitienda.com/ofertas-especiales
☑️ Incluir PhotoId
Photo ID: 987654321098765
```

## 📖 Casos de Uso Completos

### Caso 1: Publicación Diaria Automatizada

```csharp
// C# Example - Service para publicaciones automáticas
public class PublicacionAutomaticaService
{
    private readonly AnuncioService _anuncioService;
    private readonly string _accessTokenPage;
    private readonly string _pageId;

    public async Task PublicarOfertaDiariaAsync()
    {
        var mensaje = $"¡Oferta del día {DateTime.Now:dd/MM/yyyy}! " +
                     "No te pierdas nuestras promociones especiales.";
        
        await _anuncioService.PublicarEnFanpageAsync(
            _accessTokenPage, 
            _pageId, 
            mensaje, 
            "https://mitienda.com/oferta-del-dia"
        );
    }
}
```

### Caso 2: Subida Masiva de Imágenes

```bash
#!/bin/bash
# Script para subir múltiples imágenes

ACCESS_TOKEN="EAABwzLixnjYBOZCqh4ZCJmqFY...PAGE_TOKEN"
API_BASE="https://localhost:7296/api/Anuncios/fanpage"

# Array de imágenes
images=("imagen1.jpg" "imagen2.jpg" "imagen3.jpg")

for image in "${images[@]}"
do
  echo "Subiendo $image..."
  
  # Subir imagen
  response=$(curl -s -X POST "$API_BASE/upload-local-image?accessTokenPage=$ACCESS_TOKEN" \
    -F "Archivo=@$image" \
    -F "Mensaje=Imagen desde lote automatizado")
  
  # Extraer photoId de la respuesta
  photoId=$(echo $response | jq -r '.photoId')
  
  echo "Imagen subida con ID: $photoId"
  
  # Publicar post con la imagen
  curl -X POST "$API_BASE/post-personalizado" \
    -H "Content-Type: application/json" \
    -d "{
      \"accessTokenPage\": \"$ACCESS_TOKEN\",
      \"pageId\": \"123456789012345\",
      \"mensaje\": \"Nueva imagen en nuestra galería! 📸\",
      \"photoId\": \"$photoId\"
    }"
    
  echo "Post publicado con imagen $image"
  sleep 2  # Pausa entre publicaciones
done
```

### Caso 3: Monitor de Respuestas (Conceptual)

```javascript
// JavaScript example - Monitor para respuestas
class FacebookMonitor {
  constructor(apiBaseUrl, accessToken, pageId) {
    this.apiBaseUrl = apiBaseUrl;
    this.accessToken = accessToken;
    this.pageId = pageId;
  }

  async publicarRespuestaAutomatica(mensaje) {
    const response = await fetch(`${this.apiBaseUrl}/api/Anuncios/fanpage/post-texto`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        accessTokenPage: this.accessToken,
        pageId: this.pageId,
        mensaje: mensaje
      })
    });

    return await response.json();
  }

  async responderAPreguntasFrecuentes() {
    const respuestaAutomatica = 
      "¡Gracias por tu interés! 😊 " +
      "Para más información, visita: https://misite.com/contacto " +
      "O envíanos un mensaje directo.";
    
    return await this.publicarRespuestaAutomatica(respuestaAutomatica);
  }
}

// Uso
const monitor = new FacebookMonitor(
  'https://localhost:7296',
  'EAABwzLixnjYBOZCqh4ZCJmqFY...PAGE_TOKEN',
  '123456789012345'
);

monitor.responderAPreguntasFrecuentes();
```

## 🔍 Testing y Debugging

### Test de Conectividad

```bash
# Verificar que la API está funcionando
curl -X GET "https://localhost:7296/swagger" -I

# Debería retornar HTTP 200 OK
```

### Validar Token de Facebook

```bash
# Verificar token directamente con Facebook Graph API
curl -X GET "https://graph.facebook.com/v23.0/me?access_token=TU_TOKEN_AQUI"
```

### Logs de Debugging

Los logs de la aplicación se muestran en la consola durante el desarrollo:

```bash
# Ejecutar API con logs detallados
cd Api
dotnet run --verbosity detailed
```

## ⚡ Mejores Prácticas

### 1. Gestión de Tokens

```csharp
// Renovar tokens periódicamente
public class TokenManager
{
    private string _longLivedToken;
    private DateTime _tokenExpiry;

    public async Task<string> GetValidTokenAsync()
    {
        if (_tokenExpiry <= DateTime.Now.AddDays(-30))
        {
            // Renovar token si está próximo a vencer
            _longLivedToken = await RenewTokenAsync();
            _tokenExpiry = DateTime.Now.AddDays(60);
        }
        
        return _longLivedToken;
    }
}
```

### 2. Rate Limiting

```csharp
// Implementar delays entre llamadas
public async Task PublicarMultiplesPosts(List<string> mensajes)
{
    foreach (var mensaje in mensajes)
    {
        await PublicarPostAsync(mensaje);
        await Task.Delay(2000); // Esperar 2 segundos entre posts
    }
}
```

### 3. Manejo de Errores

```csharp
public async Task<bool> PublicarConReintentos(string mensaje, int maxIntentos = 3)
{
    for (int intento = 1; intento <= maxIntentos; intento++)
    {
        try
        {
            await _anuncioService.PublicarEnFanpageAsync(token, pageId, mensaje);
            return true;
        }
        catch (HttpRequestException ex) when (intento < maxIntentos)
        {
            Console.WriteLine($"Intento {intento} falló: {ex.Message}");
            await Task.Delay(intento * 1000); // Backoff exponencial
        }
    }
    
    return false;
}
```

### 4. Validación de Contenido

```csharp
public static class ContentValidator
{
    public static bool IsValidMessage(string mensaje)
    {
        if (string.IsNullOrWhiteSpace(mensaje))
            return false;
            
        if (mensaje.Length > 63206) // Límite de Facebook
            return false;
            
        // Verificar contenido prohibido
        var palabrasProhibidas = new[] { "spam", "phishing" };
        return !palabrasProhibidas.Any(p => 
            mensaje.ToLower().Contains(p.ToLower()));
    }
}
```

## 📊 Métricas y Monitoreo

### Ejemplo de Logging Estructurado

```csharp
public class PublicationMetrics
{
    private readonly ILogger<PublicationMetrics> _logger;

    public async Task LogPublicationAsync(string pageId, string type, bool success)
    {
        _logger.LogInformation("Publication attempt: {PageId}, {Type}, {Success}, {Timestamp}",
            pageId, type, success, DateTime.UtcNow);

        // Enviar métricas a sistema de monitoreo
        if (!success)
        {
            _logger.LogError("Failed to publish to page {PageId}", pageId);
        }
    }
}
```

Estos ejemplos proporcionan una base sólida para implementar y usar ApiFacebookMeta en diferentes escenarios reales.