using Scalar.AspNetCore;
using WebApiProject.Data;
using WebApiProject.Repository;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Models;
using WebApiProject.Endpoints;
using Engine;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("Programs"));
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<DishwasherEngine>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Demo API");
    });
    app.MapScalarApiReference();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

   
    if (!dbContext.Programs.Any())
    {
        dbContext.Initialize();  
        dbContext.SaveChanges();
    }
}

app.UseHttpsRedirection();
app.ConfigureProgramEndpoints();

app.Run();

