using System.Reflection;
using Common.Logging;
using Discount.Extentions;
using Discount.Handlers;
using Discount.Repositories;
using Discount.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog configure
builder.Host.UseSerilog(Logging.ConfigureLogger);

// Add services to the container.

var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),typeof(CreateDiscountCommandHandler).Assembly
};

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Migrate Database
app.MigrateDatabase<Program>();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>(); 
});

app.Run();
