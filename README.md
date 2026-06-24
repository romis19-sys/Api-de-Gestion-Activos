# GestionActivos - API de Gestión de Activos y Préstamos

Sistema REST API para la gestión de clientes, préstamos, pagos y multas, desarrollado como proyecto final del curso **Desarrollo de Aplicaciones**.

## Tecnologías

- **Runtime:** .NET 10
- **ORM:** Entity Framework Core 10
- **Base de datos:** PostgreSQL
- **Autenticación:** ASP.NET Core Identity + JWT (Bearer tokens con refresh tokens)
- **Mapeo:** AutoMapper
- **Documentación API:** Swagger / OpenAPI
- **Contenedorización:** Docker

## Estructura del proyecto

```
GestionActivos.Api/           # Capa de presentación (controllers, middleware)
GestionActivos.Application/   # Casos de uso, servicios, DTOs, interfaces
GestionActivos.Domain/        # Entidades del dominio
GestionActivos.Infrastructure/ # Persistencia, repositorios, DbContext
```

### Arquitectura

El proyecto sigue **Arquitectura Limpia (Clean Architecture)** con 4 capas:

| Capa | Responsabilidad
| **Domain** | Entidades puras (Clientes, Prestamos, Pagos, Multas, Usuarios) 
| **Application** | Lógica de negocio, servicios, DTOs, interfaces de repositorio 
| **Infrastructure** | Implementación de repositorios con EF Core, DbContext, migraciones 
| **Api** | Controladores REST, middleware de errores, configuración JWT 

## Requisitos previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL (local o remoto)
- Docker (opcional)

## Configuración

### 1. Clonar el repositorio

```bash
git clone <repo-url>
cd ProyectoFinalGA
```

### 2. Variables de entorno

Copia el archivo de ejemplo y ajusta los valores:

```bash
cp GestionActivos.Api/.env.example GestionActivos.Api/.env
```

Edita `GestionActivos.Api/.env` con tus credenciales reales.

### 3. Migraciones

Genera y aplica la migración inicial:

```bash
dotnet ef migrations add InitialCreate --project GestionActivos.Infrastructure --startup-project GestionActivos.Api
dotnet ef database update --project GestionActivos.Infrastructure --startup-project GestionActivos.Api
```

### 4. Ejecutar

```bash
cd GestionActivos.Api
dotnet run
```

La API estará disponible en `http://localhost:5151` y Swagger en `/swagger`.

## Docker

```bash
docker build -t gestion-activos-api .
docker run -p 8080:8080 `
  -e HOST=tu_host`
  -e PORT=tu_puerto`
  -e DATABASE=tu_bd `
  -e USER=tu_usuario `
  -e PASSWORD=tu_contraseña `
  -e JWT_KEY=tu_clave_secreta_jwt `
  -e JWT_ISSUER=tu_issuer `
  -e JWT_AUDIENCE=tu_audience `
  gestion-activos-api
```

## Endpoints principales

| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/api/Auth/register` | Registrar usuario |
| POST | `/api/Auth/login` | Iniciar sesión |
| POST | `/api/Auth/refresh` | Refrescar token |
| GET/POST | `/api/Cliente` | CRUD de clientes |
| GET/POST | `/api/Prestamo` | CRUD de préstamos |
| GET/POST | `/api/Pago` | CRUD de pagos |
| GET/POST | `/api/Multa` | CRUD de multas |
| GET | `/api/Usuario` | Gestión de usuarios (admin) |

## Licencia

Proyecto educativo, con fines educativos.
