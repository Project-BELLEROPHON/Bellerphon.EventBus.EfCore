using Bellerphon.EventBus.EfCore.Abstractions.Configs;
using Microsoft.EntityFrameworkCore;

namespace Bellerphon.EventBus.EfCore.Abstractions.Constants;

public static class DbContextSpecificConstants<TDbContext> 
    where TDbContext : DbContext
{
    public static EventBusConfigs EventBusConfig { get; private set; }

    public static void SetEventBusConfig(EventBusConfigs eventBusConfig)
    {
        EventBusConfig = eventBusConfig;
    }
}