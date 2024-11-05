using Bellerophon.EventBus.EfCore.Abstractions.Configs;
using Bellerphon.EventBus.EfCore.Tests.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Bellerophon.EventBus.EfCore.Abstractions.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
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