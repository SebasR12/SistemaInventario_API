using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SistemaInventario_API.Data;
using SistemaInventario_API.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =========================
// CORS
// =========================
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy
            .WithOrigins("https://localhost:7136")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// =========================
// JWT Authentication
// =========================
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();

// =========================
// Database
// =========================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"))
);

// =========================
// Repositories
// =========================
builder.Services.AddScoped<categoria_Repositorio, categoria_Repositorio>();
builder.Services.AddScoped<Cliente_Repositorio, Cliente_Repositorio>();
builder.Services.AddScoped<Proveedor_Repositorio, Proveedor_Repositorio>();
builder.Services.AddScoped<Producto_Repositorio, Producto_Repositorio>();
builder.Services.AddScoped<Usuario_Repositorio, Usuario_Repositorio>();
builder.Services.AddScoped<factura_Repositorio, factura_Repositorio>();
builder.Services.AddScoped<detalleFactura_Repositorio, detalleFactura_Repositorio>();
builder.Services.AddScoped<MovimientoInventario_Repositorio, MovimientoInventario_Repositorio>();
builder.Services.AddScoped<HistorialPrecios_Repositorio, HistorialPrecios_Repositorio>();
builder.Services.AddScoped<Rol_Repositorio, Rol_Repositorio>();
builder.Services.AddScoped<Login_Repositorio, Login_Repositorio>();

// =========================
// Controllers + Authorization Global
// =========================
builder.Services.AddControllers(options =>
{
    // TODOS los endpoints requieren token
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter());
});

// =========================
// Swagger + Bearer Token
// =========================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sistema Inventario API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingrese el token JWT.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// =========================
// Pipeline
// =========================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PermitirFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
