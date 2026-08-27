using Checkai.Data;
using Checkai.Repositories;
using Checkai.Services;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, //como verificar a assinatura
            ValidateIssuer = true, //quem pode emitir
            ValidateAudience = true, //para quem o token deve ser
            ValidateLifetime = true, //se ainda está dentro da validade


            IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
),

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"]
        };

    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=checkai.db");
});

builder.Services.AddScoped<HabitoService>();
builder.Services.AddScoped<HabitoLogService>();

builder.Services.AddScoped<HabitoRepository>();
builder.Services.AddScoped<HabitoLogRepository>();

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<UsuarioRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();