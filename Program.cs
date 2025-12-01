using LernmoduleApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS für statische Seite + API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Statische Dateien (wwwroot)
app.UseDefaultFiles();
app.UseStaticFiles();

// CORS vor den Endpunkten aktivieren
app.UseCors("AllowAll");

// Routing
app.MapControllers();

app.Run();
