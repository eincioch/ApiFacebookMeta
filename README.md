# ApiFacebookMeta

Una aplicación integral para la integración con la API de Facebook Meta, que permite gestionar autenticación, publicaciones en fanpages y anuncios de Facebook.

## 🚀 Características Principales

- **Autenticación de Facebook**: Manejo completo de tokens de acceso (renovación, verificación de permisos)
- **Gestión de Fan Pages**: Listado y gestión de páginas de Facebook
- **Publicación de Contenido**: 
  - Posts de texto
  - Posts con enlaces
  - Publicaciones con imágenes (desde URL o archivos locales)
- **Gestión de Anuncios**: Creación y administración de anuncios de Facebook
- **Interfaz Web**: Aplicación Blazor para gestión visual de contenido
- **API RESTful**: Endpoints documentados para integración

## 🏗️ Arquitectura

El proyecto sigue un patrón de **Arquitectura Limpia** con las siguientes capas:

```
📦 ApiFacebookMeta
├── 🔌 Api/                    # Capa de API - Controladores REST
├── 🖥️ WebFacebookAuth/        # Aplicación Blazor - Interfaz de usuario
├── ⚙️ Aplicacion/             # Capa de Aplicación - Servicios de negocio
├── 🏛️ Dominio/               # Capa de Dominio - Entidades y contratos
└── 💾 Repository/            # Capa de Datos - Implementación de repositorios
```

### Componentes Principales

- **API Controllers**: Exposición de endpoints REST
- **Blazor Web App**: Interfaz de usuario interactiva
- **Application Services**: Lógica de negocio
- **Domain Entities**: Modelos de datos y contratos
- **Repositories**: Acceso a datos y APIs externas

## 🛠️ Tecnologías

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Blazor Server**
- **Swagger/OpenAPI**
- **HttpClient** para integración con Facebook API
- **Dependency Injection**

## 📋 Requisitos Previos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- Cuenta de desarrollador de Facebook Meta
- App de Facebook configurada con permisos apropiados

## ⚡ Instalación Rápida

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/eincioch/ApiFacebookMeta.git
   cd ApiFacebookMeta
   ```

2. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

3. **Compilar la solución**
   ```bash
   dotnet build
   ```

4. **Ejecutar la API**
   ```bash
   cd Api
   dotnet run
   ```

5. **Ejecutar la aplicación web** (en otra terminal)
   ```bash
   cd WebFacebookAuth
   dotnet run
   ```

## 🔑 Configuración de Facebook

Para usar esta aplicación, necesitarás configurar una aplicación en Facebook Developer:

1. Ve a [Facebook for Developers](https://developers.facebook.com/)
2. Crea una nueva aplicación
3. Configura los permisos necesarios:
   - `pages_manage_posts`
   - `pages_read_engagement`
   - `ads_management` (para anuncios)

## 📚 Documentación Detallada

### 📊 Informes Ejecutivos
- [📋 Resumen Ejecutivo](RESUMEN_EJECUTIVO.md) - Vista rápida para directivos
- [📊 Informe Ejecutivo Completo](INFORME_EJECUTIVO.md) - Análisis detallado del proyecto
- [🎯 Presentación Ejecutiva](PRESENTACION_EJECUTIVA.md) - Para stakeholders
- [🔧 Detalles Técnicos](DETALLES_TECNICOS.md) - Implementación técnica

### 🛠️ Documentación Técnica
- [📖 Guía de Instalación Completa](docs/INSTALLATION.md)
- [🔧 Documentación de API](docs/API.md)
- [🏛️ Arquitectura del Proyecto](docs/ARCHITECTURE.md)
- [💡 Ejemplos de Uso](docs/EXAMPLES.md)

## 🚦 Endpoints Principales

### Autenticación
- `POST /api/FacebookAuth/renovar-token` - Renovar token de corta duración
- `POST /api/FacebookAuth/obtener-lista-fanpage` - Obtener lista de fan pages
- `POST /api/FacebookAuth/obtener-token-page` - Obtener token de página específica

### Publicaciones
- `POST /api/Anuncios/fanpage/post-texto` - Publicar texto en fan page
- `POST /api/Anuncios/fanpage/post-personalizado` - Publicar con enlaces/imágenes
- `POST /api/Anuncios/fanpage/upload-image` - Subir imagen desde URL
- `POST /api/Anuncios/fanpage/upload-local-image` - Subir imagen local

### Anuncios
- `POST /api/Anuncios/facebook` - Crear anuncio en Facebook

## 🖥️ Interfaz Web

La aplicación incluye una interfaz web Blazor accesible en `https://localhost:5001` que proporciona:

- Formularios para publicar en fan pages
- Gestión de imágenes
- Visualización de resultados
- Interfaz intuitiva para todas las funcionalidades

## 🤝 Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo [LICENSE](LICENSE) para más detalles.

## 📞 Contacto

- **Autor**: eincioch
- **Repositorio**: [https://github.com/eincioch/ApiFacebookMeta](https://github.com/eincioch/ApiFacebookMeta)

## 🙏 Agradecimientos

- Facebook Meta por proporcionar la API
- Comunidad .NET por las herramientas y frameworks utilizados