using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Obtener connection string desde appsettings o variables de entorno (Render)
var connectionString = builder.Configuration.GetConnectionString("ConnectionStrings__Connection") 
    ?? Environment.GetEnvironmentVariable("Connection");

// Configuración de DbContext con PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Servicios básicos
builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// IMPORTANTE: configurar el puerto dinámico para Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// Ejecutar migraciones automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Swagger siempre activo (Render no es "Development")
app.UseSwagger();
app.UseSwaggerUI();

// Render ya maneja HTTPS, mejor evitar esto si da problemas
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
