# Changelog

Todos los cambios notables de este proyecto serán documentados en este archivo.

El formato está basado en [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/),
y este proyecto adhiere a [Semantic Versioning](https://semver.org/lang/es/).

## [1.0.0] - 2024-01-XX

### Añadido
- ✨ Implementación inicial de la API de Facebook Meta
- 🔐 Sistema de autenticación y renovación de tokens de Facebook
- 📱 Gestión completa de fan pages de Facebook
- 📝 Funcionalidades de publicación de posts (texto, enlaces, imágenes)
- 🖼️ Sistema de subida de imágenes (URL y archivos locales)
- 📊 Gestión básica de anuncios de Facebook
- 🖥️ Aplicación web Blazor para interfaz de usuario
- 🏛️ Arquitectura limpia con separación de capas
- 📚 Documentación completa del proyecto
- 🔧 Configuración de Swagger para documentación de API
- ⚙️ Inyección de dependencias configurada
- 🧪 Estructura preparada para testing

### Características Técnicas
- **Framework**: .NET 9.0
- **API**: ASP.NET Core Web API
- **Frontend**: Blazor Server
- **Arquitectura**: Clean Architecture / DDD
- **Documentación**: Swagger/OpenAPI
- **Patrones**: Repository, Service Layer, Dependency Injection

### Endpoints de API
- `POST /api/FacebookAuth/renovar-token` - Renovación de tokens
- `POST /api/FacebookAuth/obtener-lista-fanpage` - Lista de fan pages
- `POST /api/FacebookAuth/obtener-token-page` - Token de página específica
- `POST /api/Anuncios/facebook` - Creación de anuncios
- `POST /api/Anuncios/fanpage/post-texto` - Post de texto simple
- `POST /api/Anuncios/fanpage/post-personalizado` - Post con enlaces/imágenes
- `POST /api/Anuncios/fanpage/upload-image` - Subida de imagen desde URL
- `POST /api/Anuncios/fanpage/upload-local-image` - Subida de imagen local

### Interfaz Web
- 📄 Página principal con información del proyecto
- 📝 Formulario para publicar posts en fan pages
- 🖼️ Interfaz para gestión de imágenes
- ⚙️ Configuración de tokens y páginas

### Documentación
- 📖 README principal con overview del proyecto
- 📋 Guía completa de instalación y configuración
- 🔧 Documentación detallada de la API
- 🏛️ Descripción de la arquitectura del proyecto
- 💡 Ejemplos prácticos de uso
- 📚 Índice de navegación de documentación

---

## [Unreleased]

### Planificado para Futuras Versiones
- 🧪 Suite de testing automatizado
- 📊 Dashboard de métricas y análisis
- 🔄 Programación de publicaciones
- 📱 Soporte para Instagram Business API
- 🔔 Sistema de notificaciones
- 🌐 Soporte multiidioma
- 🐳 Containerización con Docker
- ☁️ Guías de deployment en cloud
- 📈 Optimizaciones de performance
- 🔐 Funcionalidades de seguridad avanzadas

---

## Tipos de Cambios

- `✨ Añadido` para nuevas características
- `🔄 Cambiado` para cambios en funcionalidades existentes
- `⚠️ Deprecado` para funcionalidades que serán removidas
- `🗑️ Removido` para funcionalidades removidas
- `🐛 Corregido` para corrección de bugs
- `🔐 Seguridad` para vulnerabilidades corregidas

## Contribuir

Para contribuir al proyecto:

1. 🍴 Fork el repositorio
2. 🌟 Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. 💾 Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. 🚀 Push a la rama (`git push origin feature/AmazingFeature`)
5. 🔄 Abre un Pull Request

### Notas para Contribuidores

- Actualiza este CHANGELOG cuando agregues nuevas características
- Sigue el formato de [Keep a Changelog](https://keepachangelog.com/)
- Usa [Semantic Versioning](https://semver.org/) para versiones
- Incluye tests para nuevas funcionalidades
- Actualiza la documentación correspondiente