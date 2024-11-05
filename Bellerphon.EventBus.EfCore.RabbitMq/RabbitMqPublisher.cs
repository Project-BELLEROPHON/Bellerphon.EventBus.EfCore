using System.Text.Json;
using Bellerphon.EventBus.EfCore.Abstractions;
using Bellerphon.EventBus.EfCore.Abstractions.Constants;
using Bellerphon.EventBus.EfCore.Abstractions.Entities;
using Bellerphon.EventBus.EfCore.RabbitMq.Constants;
using Microsoft.EntityFrameworkCore;

namespace Bellerphon.EventBus.EfCore.RabbitMq;

public class RabbitMqPublisher<TDbContext> : IPublisher<TDbContext>
    where TDbContext : DbContext
{
    private readonly TDbContext _context;

    public RabbitMqPublisher(TDbContext context)
    {
        _context = context;
    }
    public Task Publish<TEvent>(TEvent body, Dictionary<string,string>? headers = null, CancellationToken cancellationToken = default) where TEvent : class
    {
        if(cancellationToken.IsCancellationRequested) return Task.FromCanceled(cancellationToken);
        
        DateTime currentDateTime = DateTime.UtcNow;
        _context.Set<OutBoxMessage>().Add(new OutBoxMessage()
        {
            EventName = TypeConstants.GetEventName<TEvent>(),
            Body = JsonSerializer.Serialize(body),
            InsertDate = currentDateTime,
            ExpireDate = CalculateExpireDate(currentDateTime, DbContextSpecificConstants<TDbContext>.EventBusConfig.ExpireAfter),
            TryCount = 0,
            IsSent = false,
            Headers = SerializeHeaders(headers)
        });
        
        return Task.CompletedTask;
    }
    private string? SerializeHeaders(Dictionary<string, string>? headers)
    {
        if (headers is null) return null;

        return JsonSerializer.Serialize(headers);
    }
    
    private DateTime? CalculateExpireDate(DateTime currentDate, TimeSpan? expirationTime)
    {
        if (expirationTime is null) return null;
        
        return currentDate.Add(expirationTime.Value);
    }
    
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    
}