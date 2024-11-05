using Bellerphon.EventBus.EfCore.Abstractions;
using Bellerphon.EventBus.EfCore.RabbitMq.Configs;
using Microsoft.Extensions.DependencyInjection;

namespace Bellerphon.EventBus.EfCore.RabbitMq.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterEfCoreEventBusWithRabbit(
        this IServiceCollection services, 
        Action<EfCoreEventBusRabbitConfig> configAction)
    {

        EfCoreEventBusRabbitConfig config = new EfCoreEventBusRabbitConfig();
        configAction.Invoke(config);
        return services
            .AddScoped(typeof(IPublisher<>),typeof(RabbitMqOutBoxPublisher<>))
            .AddSingleton<EfCoreEventBusRabbitConfig>(config);
    }
}