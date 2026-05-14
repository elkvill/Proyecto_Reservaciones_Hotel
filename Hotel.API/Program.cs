using Hotel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
//using Hotel.Application.Service;
//using Hotel.Infrastructure.Repository;
//using Hotel.Application.Mappings;
using Hotel.Api.Middleware;
//using Hotel.Application.Interface.Repository;
//using Hotel.Application.Interface.Service;

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
    $"Password={password};";

// registrar ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContent>(optios =>
{
    optios.UseNpgsql(connectionString);
});

// registrar repositorios con sus interfaces
//builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
//builder.Services.AddScoped<ITipoHabitacionRepository, TipoHabitacionRepository>();
//builder.Services.AddScoped<IHabitacionRepository, HabitacionRepository>();
//builder.Services.AddScoped<IDetalleReservaRepository, DetalleReservaRepository>();

// registrar servicios con sus interfaces
//builder.Services.AddScoped<IReservaService, ReservaService>();
//builder.Services.AddScoped<ITipoHabitacionService, TipoHabitacionService>();
//builder.Services.AddScoped<IHabitacionService, HabitacionService>();
//builder.Services.AddScoped<IDetalleReservaService, DetalleReservaService>();

// registrar autoMapper
//builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);


// Add services to the container.

builder.Services.AddControllers();

//Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSawe();

builder.Services.AddOpenApi();

var app = builder.Build();

//Registrar Middleware para excepciones globales
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
