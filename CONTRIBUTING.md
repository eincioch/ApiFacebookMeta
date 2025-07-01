# 🤝 Guía de Contribución

¡Gracias por tu interés en contribuir a ApiFacebookMeta! Esta guía te ayudará a hacer contribuciones efectivas al proyecto.

## 📋 Tabla de Contenidos

- [Código de Conducta](#código-de-conducta)
- [Cómo Contribuir](#cómo-contribuir)
- [Proceso de Desarrollo](#proceso-de-desarrollo)
- [Estándares de Código](#estándares-de-código)
- [Testing](#testing)
- [Documentación](#documentación)
- [Reporte de Issues](#reporte-de-issues)

## 🤗 Código de Conducta

Este proyecto adhiere a un código de conducta. Al participar, se espera que mantengas este código. Por favor reporta comportamiento inaceptable a través de los issues de GitHub.

### Nuestros Estándares

**Comportamientos que contribuyen a crear un ambiente positivo:**
- 🌟 Uso de lenguaje acogedor e inclusivo
- 🤝 Respeto a diferentes puntos de vista y experiencias
- 💡 Aceptación constructiva de críticas
- 🎯 Enfoque en lo que es mejor para la comunidad
- 😊 Mostrar empatía hacia otros miembros de la comunidad

**Comportamientos inaceptables:**
- ❌ Uso de lenguaje o imágenes sexualizadas
- 🚫 Trolling, comentarios insultantes o ataques personales
- 🔒 Acoso público o privado
- 📧 Publicar información privada de otros sin permiso
- ⚖️ Otras conductas que podrían considerarse inapropiadas en un entorno profesional

## 🚀 Cómo Contribuir

### Reportar Bugs
1. **Verifica** que el bug no haya sido reportado previamente
2. **Usa** la plantilla de issue para bugs
3. **Incluye** información detallada sobre el entorno
4. **Proporciona** pasos para reproducir el problema

### Sugerir Mejoras
1. **Revisa** las issues existentes por duplicados
2. **Usa** la plantilla de feature request
3. **Explica** claramente el beneficio de la mejora
4. **Proporciona** ejemplos de uso si es posible

### Pull Requests
1. **Fork** el repositorio
2. **Crea** una rama desde `master`
3. **Implementa** tus cambios
4. **Añade** tests si es aplicable
5. **Actualiza** la documentación
6. **Envía** el pull request

## 🔄 Proceso de Desarrollo

### 1. Setup del Entorno

```bash
# 1. Fork y clonar el repositorio
git clone https://github.com/TU_USERNAME/ApiFacebookMeta.git
cd ApiFacebookMeta

# 2. Añadir el repositorio original como upstream
git remote add upstream https://github.com/eincioch/ApiFacebookMeta.git

# 3. Instalar dependencias
dotnet restore

# 4. Verificar que compila
dotnet build
```

### 2. Crear una Rama

```bash
# Crear rama para nueva feature
git checkout -b feature/nombre-descriptivo

# Crear rama para bug fix
git checkout -b fix/descripcion-del-bug

# Crear rama para documentación
git checkout -b docs/mejora-documentacion
```

### 3. Realizar Cambios

- ✅ Mantén los commits atómicos y con mensajes descriptivos
- ✅ Sigue las convenciones de código del proyecto
- ✅ Añade tests para nueva funcionalidad
- ✅ Actualiza documentación relevante

### 4. Testing Local

```bash
# Ejecutar tests (cuando estén implementados)
dotnet test

# Verificar que la API funciona
cd Api
dotnet run

# Verificar que la aplicación web funciona
cd WebFacebookAuth
dotnet run
```

### 5. Enviar Pull Request

```bash
# Actualizar desde upstream
git fetch upstream
git rebase upstream/master

# Push de tu rama
git push origin tu-rama

# Crear PR en GitHub
```

## 📏 Estándares de Código

### Convenciones de C#

#### Nomenclatura
```csharp
// ✅ Correcto
public class AnuncioService
{
    private readonly IAnuncioRepository _anuncioRepository;
    
    public async Task<string> PublicarEnFanpageAsync(string mensaje)
    {
        var resultado = await _anuncioRepository.PublicarAsync(mensaje);
        return resultado;
    }
}

// ❌ Incorrecto
public class anuncioservice
{
    private IAnuncioRepository anuncioRepository;
    
    public async Task<string> publicar_en_fanpage(string Mensaje)
    {
        var Resultado = await anuncioRepository.publicar(Mensaje);
        return Resultado;
    }
}
```

#### Estructura de Archivos
- **Controllers**: `NombreController.cs`
- **Services**: `NombreService.cs`
- **Repositories**: `NombreRepository.cs`
- **Entities**: `NombreEntidad.cs`
- **DTOs**: `NombreDto.cs`

### Arquitectura

#### Respeta las Capas
```csharp
// ✅ Correcto - Controller usa Service
[ApiController]
public class AnunciosController : ControllerBase
{
    private readonly AnuncioService _anuncioService;
    
    public AnunciosController(AnuncioService anuncioService)
    {
        _anuncioService = anuncioService;
    }
}

// ❌ Incorrecto - Controller usa Repository directamente
[ApiController]
public class AnunciosController : ControllerBase
{
    private readonly IAnuncioRepository _anuncioRepository; // ❌
}
```

#### Inyección de Dependencias
```csharp
// ✅ Correcto - Usar interfaces
public class AnuncioService
{
    private readonly IAnuncioRepository _repository;
    
    public AnuncioService(IAnuncioRepository repository)
    {
        _repository = repository;
    }
}

// ❌ Incorrecto - Dependencia concreta
public class AnuncioService
{
    private readonly AnuncioRepository _repository; // ❌
}
```

## 🧪 Testing

### Estructura de Tests
```
Tests/
├── Unit/
│   ├── Services/
│   ├── Controllers/
│   └── Repositories/
├── Integration/
│   ├── Api/
│   └── Web/
└── E2E/
    └── Scenarios/
```

### Ejemplo de Test Unitario
```csharp
[Test]
public async Task PublicarEnFanpage_ConMensajeValido_DeberiaRetornarExito()
{
    // Arrange
    var mockRepository = new Mock<IAnuncioRepository>();
    var service = new AnuncioService(mockRepository.Object);
    var mensaje = "Test mensaje";
    
    mockRepository
        .Setup(r => r.PublicarAsync(It.IsAny<string>()))
        .ReturnsAsync("success");
    
    // Act
    var resultado = await service.PublicarEnFanpageAsync(mensaje);
    
    // Assert
    Assert.AreEqual("success", resultado);
    mockRepository.Verify(r => r.PublicarAsync(mensaje), Times.Once);
}
```

## 📚 Documentación

### Actualizar Documentación

Cuando hagas cambios, actualiza:
- **README.md** si cambias funcionalidades principales
- **docs/API.md** si agregas/modificas endpoints
- **docs/ARCHITECTURE.md** si cambias la estructura
- **docs/EXAMPLES.md** si agregas nuevos ejemplos
- **CHANGELOG.md** para registrar todos los cambios

### Estilo de Documentación

#### Formato de Títulos
```markdown
# 📚 Título Principal (H1)
## 🔧 Subtítulo de Sección (H2)
### ⚙️ Subtítulo de Subsección (H3)
```

#### Ejemplos de Código
```markdown
\`\`\`csharp
// Siempre incluir comentarios explicativos
public async Task<string> EjemploMetodo()
{
    // Explicar qué hace este bloque
    return "resultado";
}
\`\`\`
```

#### Enlaces
- Usa enlaces relativos para documentación interna
- Usa enlaces absolutos para recursos externos
- Verifica que todos los enlaces funcionen

## 🐛 Reporte de Issues

### Antes de Reportar
1. ✅ Busca issues similares existentes
2. ✅ Verifica que tienes la versión más reciente
3. ✅ Revisa la documentación relevante

### Template de Bug Report

```markdown
**Describe el bug**
Una descripción clara y concisa del problema.

**Para Reproducir**
Pasos para reproducir el comportamiento:
1. Ve a '...'
2. Haz clic en '....'
3. Desplázate hacia abajo hasta '....'
4. Ve el error

**Comportamiento Esperado**
Una descripción clara de lo que esperabas que pasara.

**Screenshots**
Si aplica, añade screenshots para ayudar a explicar el problema.

**Entorno:**
- OS: [ej. Windows 10]
- .NET Version: [ej. 9.0.1]
- Browser: [ej. Chrome 91.0]

**Contexto Adicional**
Cualquier otro contexto sobre el problema aquí.
```

### Template de Feature Request

```markdown
**¿Tu feature request está relacionado con un problema?**
Una descripción clara del problema. Ej. Siempre me frustra cuando [...]

**Describe la solución que te gustaría**
Una descripción clara y concisa de lo que quieres que pase.

**Describe alternativas que hayas considerado**
Una descripción clara de cualquier solución o feature alternativa que hayas considerado.

**Contexto adicional**
Añade cualquier otro contexto o screenshots sobre el feature request aquí.
```

## 🏷️ Convenciones de Commits

### Formato de Mensajes
```
tipo(ámbito): descripción corta

Descripción más detallada si es necesaria.

Fixes #123
```

### Tipos de Commit
- `feat`: Nueva característica
- `fix`: Corrección de bug
- `docs`: Solo cambios en documentación
- `style`: Cambios que no afectan el significado del código
- `refactor`: Cambio de código que no corrige un bug ni añade una característica
- `test`: Añadir tests faltantes
- `chore`: Cambios en el proceso de build o herramientas auxiliares

### Ejemplos
```bash
feat(api): añadir endpoint para eliminar posts

fix(auth): corregir validación de tokens expirados

docs(readme): actualizar guía de instalación

style(controllers): formatear código según estándares

refactor(services): simplificar lógica de publicación

test(integration): añadir tests para upload de imágenes

chore(deps): actualizar dependencias de seguridad
```

## 🌟 Reconocimiento

Los contribuidores son reconocidos en:
- 📋 README principal
- 🏆 Releases de GitHub
- 📊 Contributors graph

¡Gracias por hacer ApiFacebookMeta mejor para todos! 🚀