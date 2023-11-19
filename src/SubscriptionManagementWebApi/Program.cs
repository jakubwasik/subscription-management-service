using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.Domain;
using SubscriptionManagement.Infrastructure;
using SubscriptionManagement.Infrastructure.EntityConfiguration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var cs = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(cs, optionsBuilder => optionsBuilder.EnableRetryOnFailure(10, TimeSpan.FromSeconds(10), null)));
builder.Services.RegisterDomainTypes();
builder.Services.RegisterInfrastructureTypes();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
    if (app.Environment.IsDevelopment())
    {
        await new SeedData().SeedAsync(context);
    }
}

app.Run();
