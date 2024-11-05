namespace Bellerphon.EventBus.EfCore.Abstractions.Exceptions.Base;

public class BellerophonEventBusException : Exception
{
    public BellerophonEventBusException(string message) : base(message) { }
}