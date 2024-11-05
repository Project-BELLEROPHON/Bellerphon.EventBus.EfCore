using Bellerophon.EventBus.EfCore.Abstractions.Exceptions.Base;

namespace Bellerophon.EventBus.EfCore.Abstractions.Configs;

public class EventBusConfigs
{
    /// <summary>
    /// After message expiration time passes, if 'RunExpiredMessagesCollector' is true then no matter if message is sent or not,
    /// it will be removed from the table.
    /// If 'RunExpiredMessagesCollector' is false, 'ExpireAfter' must be null because it does not matter and message will be removed only if it was sent.
    /// </summary>
    public TimeSpan? ExpireAfter { get; set; }
    
    /// <summary>
    /// Determines if expired messages collector should be run.
    /// </summary>
    public required bool RunExpiredMessagesCollector { get; set; }

    internal void Validate()
    {
        if (RunExpiredMessagesCollector is true && ExpireAfter is null)
        {
            throw new BellerophonEventBusException("ExpireAfter must be set if RunExpiredMessagesCollector is true");
        }
        else if(RunExpiredMessagesCollector is false && ExpireAfter is not null)
        {
            throw new BellerophonEventBusException("ExpireAfter must be null if RunExpiredMessagesCollector is false");
        }
    }
}