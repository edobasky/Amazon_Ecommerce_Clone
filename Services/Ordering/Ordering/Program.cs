using EventBus.Messages.Common;
using MassTransit;
using Ordering.Data;
using Ordering.Dispatcher;
using Ordering.EventBusConsumer;
using Ordering.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Application Service
builder.Services.AddApplicationServices();

// Add Infra service
builder.Services.AddInfraServices(builder.Configuration);

// Register outbox message dispatcher
builder.Services.AddHostedService<OutboxMessageDispatcher>();

// Add Mass Transit
builder.Services.AddMassTransit(config =>
{
    // Mark as consumer
    config.AddConsumer<BasketOrderringConsumer>();
    config.AddConsumer<PaymentCompletedConsumer>();
    config.AddConsumer<PaymentFailedConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        // provide the queu name with consumer settings
        cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<BasketOrderringConsumer>(ctx); 
        });
        // Payment completed Event
        cfg.ReceiveEndpoint(EventBusConstant.PaymentCompletedQueue, e =>
        {
            e.ConfigureConsumer<PaymentCompletedConsumer>(ctx); 
        });
        // Payment failed Event
        cfg.ReceiveEndpoint(EventBusConstant.PaymentFailedQueue, e =>
        {
            e.ConfigureConsumer<PaymentFailedConsumer>(ctx);
        });
    });
});

var app = builder.Build();

app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context,logger).Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
