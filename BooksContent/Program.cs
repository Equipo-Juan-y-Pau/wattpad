using MongoDB.Driver;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BooksContent.Repositories;
using BooksContent.Services;

var builder = WebApplication.CreateBuilder(args);

// Leer la configuración de MongoDB desde el archivo JSON
var mongoSettings = builder.Configuration.GetSection("MongoDB");
var connectionString = mongoSettings["ConnectionString"];
var databaseName = mongoSettings["DatabaseName"];

if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(databaseName))
{
    throw new ArgumentNullException("ConnectionString o DatabaseName no pueden ser nulos.");
}

// Configuración de MongoDB
builder.Services.AddSingleton<IMongoClient>(serviceProvider => new MongoClient(connectionString));
builder.Services.AddScoped(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    return client.GetDatabase(databaseName);
});

// Registra tus repositorios aquí
builder.Services.AddScoped<IChapterRepository, ChapterRepository>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();

// Configura el servicio BooksService
builder.Services.AddScoped<IBooksService, BooksService>();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

// Agrega autenticación JWT Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var jwtKey = builder.Configuration["Jwt:Key"];
    if (string.IsNullOrEmpty(jwtKey))
    {
        throw new InvalidOperationException("La clave secreta JWT no está configurada.");
    }
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
