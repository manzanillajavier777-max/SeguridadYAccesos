using Microsoft.EntityFrameworkCore;
using ConsultaExterna.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Puerto dinámico (Render) → SIEMPRE antes de Build()
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// 🔹 Connection string (Render o appsettings)
var connectionString = builder.Configuration.GetConnectionString("Connection") 
    ?? Environment.GetEnvironmentVariable("ConnectionStrings__ConnectionSeguridadyAccesos");

if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string NO configurado");
}

// 🔹 DbContext
builder.Services.AddDbContext<ConsultaExternaContext>(options =>
    options.UseNpgsql(connectionString));

// 🔹 Servicios
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Migraciones automáticas
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ConsultaExternaContext>();
    context.Database.Migrate();
}

// 🔹 Middleware
app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
