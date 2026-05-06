using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connection string: appsettings o variable de entorno (Render)
var connectionString = builder.Configuration.GetConnectionString("Connection") 
    ?? Environment.GetEnvironmentVariable("ConnectionStrings__ConnectionSeguridadyAccesos");

// Validación (opcional pero recomendable)
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string NO configurado");
}

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Servicios
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Puerto dinámico (Render)
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Migraciones automáticas
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
