using Bellerphon.EventBus.EfCore.Abstractions.Exceptions.Base;

namespace Bellerphon.EventBus.EfCore.Abstractions.Exceptions;

public class EventIsNotRegistered : BellerophonEventBusException
{
    public EventIsNotRegistered(string eventName) : base($"event '{eventName}' is not registered") { }
}