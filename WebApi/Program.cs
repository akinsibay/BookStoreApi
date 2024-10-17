using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Middlewares;
using WebApi.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Controller'ları eklemek için bu satır gerekli
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// In-Memory database için DbContext yapılandırması
builder.Services.AddDbContext<BookStoreDBContext>(options =>
    options.UseInMemoryDatabase(databaseName: "BookStoreDB")); // InMemory veritabanı ismi belirliyoruz

builder.Services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDBContext>());

// Add Auto Mapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Dependency Injection - Logger Service
builder.Services.AddSingleton<ILoggerService, DBLogger>();

var app = builder.Build();

// Uygulama başlarken DataGenerator'ı çağırarak veritabanını başlatıyoruz
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Bizim custom middleware imiz
app.UseCustomExceptionMiddle();

// Controller'ları kullanmak için MapControllers eklenmeli
app.MapControllers();

app.Run();
