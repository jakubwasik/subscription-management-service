using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SubscriptionManagement.Domain;
using SubscriptionManagement.Infrastructure.EntityConfiguration;
using SubscriptionManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining(typeof(Program)));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var cs = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(cs, optionsBuilder => optionsBuilder.EnableRetryOnFailure(10, TimeSpan.FromSeconds(10), null)));

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
}
app.Run();