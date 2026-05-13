using Microsoft.EntityFrameworkCore;
using University.Common.Services;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories;
using University.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Основні сервіси
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext + Repository + Service
builder.Services.AddDbContext<UniversityContext>(options =>
    options.UseSqlite("Data Source=university.db"));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ICrudServiceAsync<>), typeof(CrudServiceAsync<>));

var app = builder.Build();

// Налаштування
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
