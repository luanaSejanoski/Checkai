using Checkai.Data;
using Checkai.Repositories;
using Checkai.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

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