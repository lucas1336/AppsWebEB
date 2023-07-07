using EB.API.Mapper;
using EB.Domain;
using EB.Domain.ExceptionHandling;
using EB.Domain.Interface;
using EB.Domain.Mapping.Mapper;
using EB.Infrastructure;
using EB.Infrastructure.Context;
using EB.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IProductInfrastructure, ProductInfrastructure>();
builder.Services.AddScoped<ISnapshotInfrastructure, SnapshotInfrastructure>();
builder.Services.AddScoped<IProductDomain, ProductDomain>();
builder.Services.AddScoped<ISnapshotDomain, SnapshotDomain>();
builder.Services.AddAutoMapper(
    typeof(ModelToResponse),
    typeof(InputToModel)
);

// MySQL Connection
var connectionString = builder.Configuration.GetConnectionString("EBConnection");

builder.Services.AddDbContext<EBContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString),
            options => options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: System.TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null)
        );
    });

var app = builder.Build();

// Create database if not exists
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<EBContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();