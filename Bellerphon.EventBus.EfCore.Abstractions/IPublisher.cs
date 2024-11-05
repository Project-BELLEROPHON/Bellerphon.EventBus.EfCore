using Microsoft.EntityFrameworkCore;

namespace Bellerphon.EventBus.EfCore.Abstractions;

public interface IPublisher<TDbContext>
    where TDbContext : DbContext
{
    Task Publish<TEvent>(TEvent body, Dictionary<string,string>? headers, CancellationToken cancellationToken = default)
        where TEvent : class;
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}