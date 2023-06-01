using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FeatureFlagAPI.Data;
using FeatureFlagAPI.Controllers;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FeatureFlagAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FeatureFlagAPIContext") ?? throw new InvalidOperationException("Connection string 'FeatureFlagAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.MapFeatureFlagEndpoints();

app.Run();