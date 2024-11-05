using Bellerphon.EventBus.EfCore.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Bellerphon.EventBus.EfCore.RabbitMq;

public class RabbitMqPublisher<TDbContext> : IPublisher<TDbContext>
    where TDbContext : DbContext
{
    public RabbitMqPublisher()
    {
               
    }
    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class
    {
        throw new NotImplementedException();
    }
}