using GestionActivos.Api.Middleware;
using GestionActivos.Infrastructure.Repository;
using GestionActivos.Application.Interface.Repository;
using GestionActivos.Application.Interface.Service;
using GestionActivos.Application.Mappings;
using GestionActivos.Application.Service;
using GestionActivos.Domain.Entities;
using GestionActivos.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Ecommerce.Infrastructure.Repostory;


var builder = WebApplication.CreateBuilder(args);

// Cargar las variables de entorno
DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();

// Leer las varibales de entorno
var host = Environment.GetEnvironmentVariable("HOST");
var port = Environment.GetEnvironmentVariable("PORT");
var database = Environment.GetEnvironmentVariable("DATABASE");
var user = Environment.GetEnvironmentVariable("USER");
var password = Environment.GetEnvironmentVariable("PASSWORD");
var key = Environment.GetEnvironmentVariable("JWT_KEY");
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

// Validar las variables de entorno
var variablesFaltantes = new List<string>();
if (string.IsNullOrEmpty(host)) variablesFaltantes.Add("HOST");
if (string.IsNullOrEmpty(port)) variablesFaltantes.Add("PORT");
if (string.IsNullOrEmpty(database)) variablesFaltantes.Add("DATABASE");
if (string.IsNullOrEmpty(user)) variablesFaltantes.Add("USER");
if (string.IsNullOrEmpty(password)) variablesFaltantes.Add("PASSWORD");

if (variablesFaltantes.Any())
{
    throw new Exception($"Faltan variables de entorno: {string.Join(", ", variablesFaltantes)}");
}

// construir la cadena de conexion
var connectionString =
    $"Host={host};" +
    $"Port={port};" +
    $"Database={database};" +
    $"Username={user};" +
    $"Password={password};" +
    $"SSL Mode=Require;" +
    $"Trust Server Certificate=true;";

// registrar ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(optios =>
{
    optios.UseNpgsql(connectionString);
});

// registrar Identity
builder.Services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// registrar repositorios con sus interface
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPrestamoRepository, PrestamoRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<IMultaRepository, MultaRepository>();


// registrar servicios con sus interfaces
builder.Services.AddScoped<IUsuarioServices, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IClienteServices, ClienteService>();
builder.Services.AddScoped<IPrestamoServices, PrestamoService>();
builder.Services.AddScoped<IPagoServices, PagoService>();
builder.Services.AddScoped<IMultaServices, MultaService>();
// Configurar la autenticación
builder.Services.AddAuthentication
    (
        options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    ).AddJwtBearer(options =>
    {
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            RoleClaimType = ClaimTypes.Role,
            ValidIssuer = issuer,
            ValidAudience = audience
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    status = 401,
                    detail = "No autenticado. El token es inválido o no fue enviado."
                }));
            },

            OnForbidden = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    status = 403,
                    detail = "Acceso denegado. No tiene permisos para acceder a este recurso."
                }));
            }
        };
    });

// registrar autoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

// Add services to the container.

builder.Services.AddControllers();

//Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Gestión Activos API",
        Description = """
        #### **Infraestructura escalable para la Gestión de Activos.**

        Esta API proporciona una solución integral para la administración y control de activos, permitiendo gestionar préstamos, devoluciones, multas y seguimiento de usuarios de forma segura, eficiente y centralizada.

        ---

        #### Módulos del Sistema
        * **Activos:** Registro y control de activos con disponibilidad y estado en tiempo real.
        * **Préstamos:** Administración completa de préstamos, renovaciones y devoluciones.
        * **Multas y Pagos:** Gestión de sanciones, historial de pagos y control financiero.

        #### Características Técnicas
        * **Seguridad:** Autenticación y autorización mediante **JWT** y control de roles.
        * **Eficiencia:** Implementación de **paginación, filtrado y búsquedas optimizadas**.
        * **Integración:** API REST con respuestas JSON estandarizadas para aplicaciones Web y Mobile.
        * **Escalabilidad:** Arquitectura preparada para crecimiento modular y alta disponibilidad.

        ---

        """,

        Contact = new OpenApiContact
        {
            Name = "Rodolfo Kraudy (Soporte Técnico)",
            Email = "rodolfocraudy@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Configuración de seguridad para Swagger (JWT)

    // 1. Definir el esquema de seguridad que Swagger usará para UI
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT. Ejemplo: eyJhbGciOiJIUzI1NiIsInR5..."
    });

    // 2. Aplicar el esquema de seguridad a toso los endpoint protegidos de la API
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference(referenceId: "Bearer", hostDocument: document),
            new List<string>()
        }
    });
});

// Configuración de CORS
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy.WithOrigins(
                "http://localhost:4200",    // Angular
                "http://localhost:3000"    // React
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
        else
        {
            // Solo para desarrollo si no hay configuración
            policy.AllowAnyHeader()
                .AllowAnyMethod();
        }
    });
});

builder.Services.AddOpenApi();

// Construir la aplicacion
var app = builder.Build();

//Registrar Middleware para excepciones globales
app.UseMiddleware<ExceptionMiddleware>();

// Configuracion para entornos de desarrollo y produccion
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "GestionActivos API v1");
});

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});


app.UseCors("FrontendPolicy");

// Soporte para la autenticacion
app.UseAuthorization();
app.UseAuthorization();


// Mappear controladores
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    await DbInitializer.SeedAsync(scope.ServiceProvider);
}

if(app.Environment.IsDevelopment())
{
    app.Run();
}
else
{
    var apiPort = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    app.Run($"http://0.0.0.0:{apiPort}");
}