using MongoDB.Driver;
using BooksContent.Repositories;
using System.Text;
using BooksContent.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configuración de MongoDB
var mongoSettings = builder.Configuration.GetSection("MongoDB");
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    return new MongoClient(mongoSettings["ConnectionString"]);
});

builder.Services.AddScoped(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    return client.GetDatabase(mongoSettings["wattpaddb"]);
});

// Registra tus repositorios aquí
builder.Services.AddScoped<IChapterRepository, ChapterRepository>();
    

    // Configuración de la cadena de conexión MongoDB desde appsettings.json
    builder.Configuration.AddJsonFile("appsettings.json");

    // Configurar la conexión a MongoDB
    var connectionString = builder.Configuration.GetConnectionString("MongoDBConnection");
    var mongoClient = new MongoClient(connectionString);
    var mongoDatabase = mongoClient.GetDatabase("mongodb"); 

    // Configuración del servicio BooksRepository con la conexión a MongoDB
    builder.Services.AddScoped<IBooksRepository>(provider => new BooksRepository(mongoDatabase));

    // Configuración del servicio BooksService
    builder.Services.AddScoped<IBooksService, BooksService>();

    builder.Services.AddControllers().AddNewtonsoftJson();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddHttpContextAccessor();

    // Agrega la autenticación JWT Bearer
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

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
