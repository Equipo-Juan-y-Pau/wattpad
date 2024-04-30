    using System.Text;
    using BooksContent.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using MongoDB.Driver;

    var builder = WebApplication.CreateBuilder(args);

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
