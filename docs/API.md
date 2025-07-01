# 🔧 Documentación de API

Esta documentación describe todos los endpoints disponibles en la API de ApiFacebookMeta.

## 📋 Información General

- **Base URL**: `https://localhost:7296/api`
- **Formato**: JSON
- **Autenticación**: Tokens de Facebook
- **Documentación Interactiva**: `https://localhost:7296/swagger`

## 🔐 Autenticación

Todos los endpoints requieren tokens de acceso válidos de Facebook. Los tipos de tokens utilizados son:

- **User Access Token**: Token del usuario de Facebook
- **Page Access Token**: Token específico de una página de Facebook

## 📚 Endpoints

### 🔑 FacebookAuth Controller

#### POST `/api/FacebookAuth/renovar-token`

Convierte un token de corta duración en un token de larga duración.

**Request Body:**
```json
{
  "shortLivedToken": "string"
}
```

**Response:**
```json
{
  "access_token": "string"
}
```

**Ejemplo:**
```bash
curl -X POST "https://localhost:7296/api/FacebookAuth/renovar-token" \
     -H "Content-Type: application/json" \
     -d '{
       "shortLivedToken": "EAABwzLixnjYBO..."
     }'
```

---

#### POST `/api/FacebookAuth/obtener-lista-fanpage`

Obtiene la lista de fan pages asociadas a un usuario.

**Request Body:**
```json
"string" // User Access Token
```

**Response:**
```json
{
  "fanpages": [
    {
      "access_token": "string",
      "category": "string",
      "category_list": [
        {
          "id": "string",
          "name": "string"
        }
      ],
      "name": "string",
      "id": "string",
      "tasks": ["string"]
    }
  ]
}
```

**Ejemplo:**
```bash
curl -X POST "https://localhost:7296/api/FacebookAuth/obtener-lista-fanpage" \
     -H "Content-Type: application/json" \
     -d '"EAABwzLixnjYBO..."'
```

---

#### POST `/api/FacebookAuth/obtener-token-page`

Obtiene el token de acceso específico para una página.

**Request Body:**
```json
{
  "userAccessToken": "string",
  "pageId": "string"
}
```

**Response:**
```json
{
  "access_token_page": "string"
}
```

**Ejemplo:**
```bash
curl -X POST "https://localhost:7296/api/FacebookAuth/obtener-token-page" \
     -H "Content-Type: application/json" \
     -d '{
       "userAccessToken": "EAABwzLixnjYBO...",
       "pageId": "123456789"
     }'
```

---

### 📢 Anuncios Controller

#### POST `/api/Anuncios/facebook`

Crea un anuncio en Facebook.

**Request Body:**
```json
{
  "id": "string",
  "nombre": "string",
  "adsetId": 0,
  "creativeId": "string",
  "estado": "string"
}
```

**Response:**
```json
{
  "message": "Anuncio enviado a Facebook correctamente."
}
```

**Ejemplo:**
```bash
curl -X POST "https://localhost:7296/api/Anuncios/facebook" \
     -H "Content-Type: application/json" \
     -d '{
       "id": "anuncio123",
       "nombre": "Mi Anuncio",
       "adsetId": 123456789,
       "creativeId": "creative123",
       "estado": "ACTIVE"
     }'
```

---

#### POST `/api/Anuncios/fanpage/post-texto`

Publica un post de texto en una fan page.

**Request Body:**
```json
{
  "accessTokenPage": "string",
  "pageId": "string",
  "mensaje": "string"
}
```

**Response:**
```json
{
  "message": "Post publicado en la fanpage correctamente."
}
```

**Ejemplo:**
```bash
curl -X POST "https://localhost:7296/api/Anuncios/fanpage/post-texto" \
     -H "Content-Type: application/json" \
     -d '{
       "accessTokenPage": "EAABwzLixnjYBO...",
       "pageId": "123456789",
       "mensaje": "¡Hola desde nuestra API!"
     }'
```

---

#### POST `/api/Anuncios/fanpage/post-personalizado`

Publica un post personalizado con enlaces o imágenes en una fan page.

**Request Body:**
```json
{
  "accessTokenPage": "string",
  "pageId": "string",
  "mensaje": "string",
  "link": "string",
  "photoId": "string"
}
```

**Response:**
```json
{
  "message": "Post publicado en la fanpage correctamente."
}
```

**Ejemplo:**
```bash
curl -X POST "https://localhost:7296/api/Anuncios/fanpage/post-personalizado" \
     -H "Content-Type: application/json" \
     -d '{
       "accessTokenPage": "EAABwzLixnjYBO...",
       "pageId": "123456789",
       "mensaje": "Visita nuestro sitio web",
       "link": "https://misite.com",
       "photoId": "photo123"
     }'
```

---

#### POST `/api/Anuncios/fanpage/upload-image`

Sube una imagen a la fan page desde una URL.

**Request Body:**
```json
{
  "accessTokenPage": "string",
  "imageUrl": "string",
  "mensaje": "string"
}
```

**Response:**
```json
{
  "photoId": "string"
}
```

**Ejemplo:**
```bash
curl -X POST "https://localhost:7296/api/Anuncios/fanpage/upload-image" \
     -H "Content-Type: application/json" \
     -d '{
       "accessTokenPage": "EAABwzLixnjYBO...",
       "imageUrl": "https://example.com/image.jpg",
       "mensaje": "Una imagen increíble"
     }'
```

---

#### POST `/api/Anuncios/fanpage/upload-local-image`

Sube una imagen local a la fan page.

**Request:**
- **Query Parameter**: `accessTokenPage` (string, required)
- **Form Data**:
  - `Archivo` (file, required)
  - `Mensaje` (string, optional)

**Response:**
```json
{
  "photoId": "string"
}
```

**Ejemplo:**
```bash
curl -X POST "https://localhost:7296/api/Anuncios/fanpage/upload-local-image?accessTokenPage=EAABwzLixnjYBO..." \
     -F "Archivo=@/path/to/image.jpg" \
     -F "Mensaje=Una imagen desde mi computadora"
```

---

## 📊 Modelos de Datos

### Anuncio
```json
{
  "id": "string",
  "nombre": "string",
  "adsetId": "number",
  "creativeId": "string",
  "estado": "string"
}
```

### FanPage
```json
{
  "access_token": "string",
  "category": "string",
  "category_list": [
    {
      "id": "string",
      "name": "string"
    }
  ],
  "name": "string",
  "id": "string",
  "tasks": ["string"]
}
```

### Category
```json
{
  "id": "string",
  "name": "string"
}
```

## ⚠️ Códigos de Error

### 400 Bad Request
- Parámetros faltantes o inválidos
- Formato de JSON incorrecto
- Archivo no proporcionado (para upload de imágenes)

### 401 Unauthorized
- Token de acceso inválido o expirado
- Permisos insuficientes

### 500 Internal Server Error
- Error en la API de Facebook
- Error de conexión
- Error interno del servidor

## 📝 Ejemplos de Uso Completo

### Flujo Completo: Publicar una Imagen

1. **Renovar token (si es necesario)**
```bash
curl -X POST "https://localhost:7296/api/FacebookAuth/renovar-token" \
     -H "Content-Type: application/json" \
     -d '{"shortLivedToken": "EAABwzLixnjYBO..."}'
```

2. **Obtener lista de páginas**
```bash
curl -X POST "https://localhost:7296/api/FacebookAuth/obtener-lista-fanpage" \
     -H "Content-Type: application/json" \
     -d '"EAABwzLixnjYBO_LONG_LIVED_TOKEN..."'
```

3. **Obtener token de página específica**
```bash
curl -X POST "https://localhost:7296/api/FacebookAuth/obtener-token-page" \
     -H "Content-Type: application/json" \
     -d '{
       "userAccessToken": "EAABwzLixnjYBO...",
       "pageId": "123456789"
     }'
```

4. **Subir imagen**
```bash
curl -X POST "https://localhost:7296/api/Anuncios/fanpage/upload-image" \
     -H "Content-Type: application/json" \
     -d '{
       "accessTokenPage": "PAGE_ACCESS_TOKEN...",
       "imageUrl": "https://example.com/image.jpg",
       "mensaje": "¡Nueva imagen!"
     }'
```

5. **Publicar post con la imagen**
```bash
curl -X POST "https://localhost:7296/api/Anuncios/fanpage/post-personalizado" \
     -H "Content-Type: application/json" \
     -d '{
       "accessTokenPage": "PAGE_ACCESS_TOKEN...",
       "pageId": "123456789",
       "mensaje": "Mira esta increíble imagen",
       "photoId": "PHOTO_ID_FROM_STEP_4"
     }'
```

## 🔧 Configuración de Desarrollo

Para probar la API localmente:

1. Ejecuta el proyecto API: `cd Api && dotnet run`
2. Navega a: `https://localhost:7296/swagger`
3. Usa la interfaz de Swagger para probar endpoints
4. Los tokens de Facebook deben ser válidos y tener los permisos apropiados

## 📚 Recursos Adicionales

- [Facebook Graph API Documentation](https://developers.facebook.com/docs/graph-api/)
- [Facebook Marketing API](https://developers.facebook.com/docs/marketing-api/)
- [Swagger/OpenAPI Specification](https://swagger.io/specification/)