using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.DBOperations;
using WebApi.Middlewares;
using WebApi.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

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

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

// Bizim custom middleware imiz
app.UseCustomExceptionMiddle();

// Controller'ları kullanmak için MapControllers eklenmeli
app.MapControllers();

app.Run();
