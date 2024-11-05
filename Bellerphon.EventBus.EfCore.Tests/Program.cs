using Bellerphon.EventBus.EfCore.Abstractions.Configs;
using Bellerphon.EventBus.EfCore.Tests.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Bellerphon.EventBus.EfCore.Abstractions.Extensions;
using Bellerphon.EventBus.EfCore.RabbitMq.Extensions;
using Bellerphon.EventBus.EfCore.Tests.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.RegisterEfCoreEventBusWithRabbit(config =>
{
    config.ConfigureEvent<UserCreatedEvent>("user_created_event");
});

builder.Services.AddDbContext<TestDbContext>(builder =>
{
    NpgsqlConnectionStringBuilder connectionBuilder = new NpgsqlConnectionStringBuilder()
    {
        Host = "localhost",
        Password = "guest",
        Username = "guest",
        Database = "test_db",
        MaxPoolSize = 100
    };
    builder
        .UseNpgsql(connectionBuilder.ConnectionString)
        .AddEventsBus(new EventBusConfigs()
        {
            RunExpiredMessagesCollector = true,
            ExpireAfter = TimeSpan.FromMinutes(30)
        });
});
var app = builder.Build();

app.UseSwaggerUI();
app.UseSwagger();

app.MapControllers();

app.Run();