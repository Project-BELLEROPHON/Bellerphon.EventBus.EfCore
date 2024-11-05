using Bellerphon.EventBus.EfCore.RabbitMq.Constants;

namespace Bellerphon.EventBus.EfCore.RabbitMq.Configs;

public class EfCoreEventBusRabbitConfig
{
    public void ConfigureEvent<T>(string eventName) => TypeConstants.AddEventType<T>(eventName);
}