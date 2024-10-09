using Application.Mappers;
using Application.ServicesAbstractions;
using CatalogAPI.Validations;
using Domain.RepositoriesAbstraction;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.RepositoriesConcretes;
using Persistence.ServicesConcretes;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog ile logları bir dosyaya yönlendir
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day);
});

// Veritabanı bağlantısı (EnableRetryOnFailure ile)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null);
    })
);

// AutoMapper Entegrasyonu
builder.Services.AddAutoMapper(typeof(MappingProfile));

// FluentValidation Entegrasyonu
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductDTOValidator>());

// Repository'leri DI sistemine ekle
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

// Servis katmanlarını DI sistemine ekle
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Swagger yapılandırması
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS eklenebilir
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll"); // CORS politika kullanımı

app.MapControllers();

app.Run();
