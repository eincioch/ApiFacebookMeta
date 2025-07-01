# 📋 Guía de Instalación Completa

Esta guía te llevará paso a paso a través del proceso de instalación y configuración de ApiFacebookMeta.

## 🛠️ Requisitos del Sistema

### Software Requerido

- **[.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)** o superior
- **IDE recomendado**:
  - [Visual Studio 2022](https://visualstudio.microsoft.com/) (Community, Professional, o Enterprise)
  - [Visual Studio Code](https://code.visualstudio.com/) con extensión C#
  - [JetBrains Rider](https://www.jetbrains.com/rider/)

### Cuentas y Accesos Necesarios

1. **Cuenta de Facebook Developer**
   - Crear en: [https://developers.facebook.com/](https://developers.facebook.com/)
   
2. **Aplicación de Facebook configurada**
   - Con permisos específicos para la API

## 📥 Proceso de Instalación

### 1. Clonar el Repositorio

```bash
# Clonar el repositorio
git clone https://github.com/eincioch/ApiFacebookMeta.git

# Navegar al directorio
cd ApiFacebookMeta
```

### 2. Verificar Instalación de .NET

```bash
# Verificar versión de .NET
dotnet --version

# Debería mostrar 9.0.x o superior
```

### 3. Restaurar Dependencias

```bash
# Restaurar paquetes NuGet para toda la solución
dotnet restore
```

### 4. Compilar la Solución

```bash
# Compilar todos los proyectos
dotnet build

# Para compilación en modo Release
dotnet build --configuration Release
```

## 🔑 Configuración de Facebook Developer

### Paso 1: Crear Aplicación en Facebook

1. Ve a [Facebook for Developers](https://developers.facebook.com/)
2. Haz clic en "My Apps" → "Create App"
3. Selecciona "Business" como tipo de aplicación
4. Completa la información básica:
   - App Name: Tu nombre preferido
   - Contact Email: Tu email
   - Business Account: Selecciona o crea uno

### Paso 2: Configurar Productos

#### Facebook Login
1. En el dashboard, agrega "Facebook Login"
2. Configura las URLs válidas:
   - Valid OAuth Redirect URIs: `https://localhost:5001/signin-facebook`
   - Valid OAuth Redirect URIs: `https://localhost:7296/signin-facebook`

#### Marketing API (para anuncios)
1. Agrega "Marketing API" si planeas usar funcionalidades de anuncios
2. Solicita permisos avanzados si es necesario

### Paso 3: Configurar Permisos

En "App Review" → "Permissions and Features", solicita:

- `pages_manage_posts` - Para publicar en páginas
- `pages_read_engagement` - Para leer métricas de páginas
- `ads_management` - Para gestionar anuncios (opcional)
- `business_management` - Para gestión empresarial (opcional)

### Paso 4: Obtener Credenciales

En "Settings" → "Basic":
- **App ID**: Anota este valor
- **App Secret**: Anota este valor (mantén seguro)

## ⚙️ Configuración del Proyecto

### 1. Configurar Variables de Entorno (Recomendado)

Crea un archivo `appsettings.Development.json` en el proyecto `Api`:

```json
{
  "Facebook": {
    "AppId": "TU_APP_ID_AQUI",
    "AppSecret": "TU_APP_SECRET_AQUI",
    "ApiVersion": "v23.0"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 2. Configurar HTTPS (Desarrollo)

```bash
# Generar certificado de desarrollo
dotnet dev-certs https --trust
```

## 🚀 Ejecutar la Aplicación

### Opción 1: Ejecutar Proyectos Individualmente

**Terminal 1 - API:**
```bash
cd Api
dotnet run
```
La API estará disponible en: `https://localhost:7296`

**Terminal 2 - Aplicación Web:**
```bash
cd WebFacebookAuth
dotnet run
```
La web estará disponible en: `https://localhost:5001`

### Opción 2: Ejecutar con Visual Studio

1. Abre `ApiFacebook.sln` en Visual Studio
2. Configura múltiples proyectos de inicio:
   - Click derecho en la solución → "Set Startup Projects"
   - Selecciona "Multiple startup projects"
   - Marca `Api` y `WebFacebookAuth` como "Start"
3. Presiona F5 o "Start Debugging"

### Opción 3: Docker (Opcional)

Si tienes Docker instalado, puedes crear un `Dockerfile` para containerizar la aplicación.

## 🧪 Verificar Instalación

### 1. Verificar API

Navega a: `https://localhost:7296/swagger`

Deberías ver la documentación interactiva de Swagger con todos los endpoints.

### 2. Verificar Aplicación Web

Navega a: `https://localhost:5001`

Deberías ver la página principal de la aplicación Blazor.

### 3. Prueba de Conectividad

Usa el endpoint de prueba (si existe) o intenta renovar un token de prueba.

## 🔧 Solución de Problemas Comunes

### Error: "The current .NET SDK does not support targeting .NET 9.0"

**Solución**: Instala .NET 9.0 SDK desde el sitio oficial de Microsoft.

### Error: "Unable to bind to https://localhost:7296"

**Solución**: 
```bash
# Verificar certificados HTTPS
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

### Error de Conexión con Facebook API

**Posibles causas**:
1. App ID o App Secret incorrectos
2. Permisos no aprobados por Facebook
3. URLs de redirect mal configuradas

**Solución**: Verifica la configuración en Facebook Developer Console.

### Problemas de CORS

Si tienes problemas de CORS entre la API y la aplicación web, verifica la configuración en `Program.cs` del proyecto API.

## 📈 Configuración para Producción

### 1. Variables de Entorno

```bash
export FACEBOOK_APP_ID="tu_app_id"
export FACEBOOK_APP_SECRET="tu_app_secret"
```

### 2. Base de Datos (si aplica)

Configura la cadena de conexión para producción en `appsettings.Production.json`.

### 3. HTTPS en Producción

Asegúrate de configurar certificados SSL válidos para tu dominio.

### 4. Logging

Configura logging apropiado para producción (Application Insights, Serilog, etc.).

## 🔐 Consideraciones de Seguridad

1. **Nunca** hagas commit de secretos en el código fuente
2. Usa User Secrets para desarrollo local
3. Usa Azure Key Vault o similar para producción
4. Implementa rate limiting en la API
5. Valida todas las entradas de usuario
6. Mantén las dependencias actualizadas

## 📞 Soporte

Si encuentras problemas durante la instalación:

1. Revisa los logs de la aplicación
2. Consulta la documentación de Facebook Developer
3. Crea un issue en el repositorio de GitHub
4. Verifica que todos los requisitos están cumplidos