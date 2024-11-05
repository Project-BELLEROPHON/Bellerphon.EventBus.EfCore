using Microsoft.EntityFrameworkCore;

namespace Bellerphon.EventBus.EfCore.Abstractions;

public interface IPublisher<TDbContext>
    where TDbContext : DbContext
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class;
}