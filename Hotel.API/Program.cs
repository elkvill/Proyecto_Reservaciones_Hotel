
using Hotel.Api.Middleware;
using Hotel.Application.Interface.Repositorys;
using Hotel.Application.Interface.Services;
using Hotel.Application.Mappings;
using Hotel.Application.Service;
using Hotel.Domain.Entities;
using Hotel.Infrastructure.Data;
using Hotel.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


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
//var password = Environment.GetEnvironmentVariable("PASSWORD");
var key = Environment.GetEnvironmentVariable("JWT_KEY");
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

// Validar las variables de entorno
//var variablesFaltantes = new List<string>();
//if (string.IsNullOrEmpty(host)) variablesFaltantes.Add("HOST");
//if (string.IsNullOrEmpty(port)) variablesFaltantes.Add("PORT");
//if (string.IsNullOrEmpty(database)) variablesFaltantes.Add("DATABASE");
//if (string.IsNullOrEmpty(user)) variablesFaltantes.Add("USER");
//if (string.IsNullOrEmpty(password)) variablesFaltantes.Add("PASSWORD");

//if (variablesFaltantes.Any())
//{
//    throw new Exception($"Faltan variables de entorno: {string.Join(", ", variablesFaltantes)}");
//}

// construir la cadena de conexion
var connectionString =
    $"Host={host};" +
    $"Port={port};" +
    $"Database={database};" +
    $"Username={user};" +
    $"Password={password};" +
    $"SSL Mode=Prefer;" + //Prefer es para hambiente local y Require para producción, pero en este caso
                         //se usará Prefer para ambos ambientes para evitar problemas de conexión en desarrollo
                         //$"SSL Mode=Require;" +
    $"Trust Server Certificate=true;";



// registrar ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContent>(optios =>
{
    optios.UseNpgsql(connectionString);
});


// Definir las reglas de seguridad
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContent>()
    .AddDefaultTokenProviders();

// registrar repositorios con sus interfaces
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<ITipoHabitacionRepository, TipoHabitacionRepository>();
builder.Services.AddScoped<IHabitacionRepository, HabitacionRepository>();
builder.Services.AddScoped<IdbSeederRepository, DbSeederRepository>(); // <-- Agregado
//builder.Services.AddScoped<IDetalleReservaRepository, DetalleReservaRepository>();

// registrar servicios con sus interfaces
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<ITipoHabitacionService, TipoHabitacionService>();
builder.Services.AddScoped<IHabitacionService, HabitacionService>();
//builder.Services.AddScoped<IDetalleReservaService, DetalleReservaService>();

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
// Swagger / OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Ecommerce API",
        Description = """
        #### **Infraestructura robusta para la gestión de reservas y hospedaje.**
        
        Esta API proporciona un conjunto completo de herramientas para administrar las operaciones del hotel, garantizando la seguridad en el proceso de reserva, control de disponibilidad de habitaciones y una experiencia de usuario optimizada.
        
        ---
        
        #### Módulos del Sistema
        * **Habitaciones:** Gestión del catálogo de habitaciones y estados en tiempo real (Disponible, Ocupada, Mantenimiento).
        * **Reservas:** Administración integral de reservas de huéspedes, asignación de habitaciones y cálculo automático de noches y costos.
        * **Usuarios y Roles:** Control de acceso basado en roles para Clientes, Recepcionistas y Administradores.
        * **Autenticación:** Seguridad integrada con soporte para tokens **JWT** y Refresh Tokens.
        
        #### Características Técnicas
        * **Seguridad:** Autenticación de grado industrial mediante **JWT**.
        * **Integración:** Salidas JSON estandarizadas listas para su consumo en aplicaciones Frontend (Web y Mobile).
        
        ---
        

        """,

        Contact = new OpenApiContact
        {
            Name = "Kevin Villarreal (Soporte Técnico)",
            Email = "mrgmairena@gmail.com",
            Url = new Uri("https://github.com/MGarcia7783/E-commerce")
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

//Construir aplicación
var app = builder.Build();

//Registrar Middleware para excepciones globales
app.UseMiddleware<ExceptionMiddleware>();

// Configuración para entornos de desarrollo y producción
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API v1");
});

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});


app.UseCors("FrontendPolicy");

// Soporte para la autenticación
app.UseAuthentication();
app.UseAuthorization();


//mapear controladores
app.MapControllers();

// Sembrar datos iniciales si no hay usuarios
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var seeder = services.GetRequiredService<IdbSeederRepository>();
        await seeder.SeederAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al ejecutar el seeder de base de datos.");
    }
}



if (app.Environment.IsDevelopment())
{
    app.Run();
}
else
{
    var apiPort = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    app.Run($"http://0.0.0.0:{apiPort}");
}

