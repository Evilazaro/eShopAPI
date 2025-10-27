using eShopAPI.Data;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<StoreDBContext>(OptionsBuilderConfigurationExtensions =>
    OptionsBuilderConfigurationExtensions.UseInMemoryDatabase("storedb"));
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "eShopAPI V1");
});

app.MapOpenApi(pattern: "/openapi/v1.json");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
