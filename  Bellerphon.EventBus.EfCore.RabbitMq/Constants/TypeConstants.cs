using Bellerphon.EventBus.EfCore.Abstractions.Exceptions;

namespace Bellerphon.EventBus.EfCore.RabbitMq.Constants;

internal static class TypeConstants
{
    private static Dictionary<string, Type> _eventTypes { get; } = new();

    internal static Type GetEventType(string eventName)
    {
        if (_eventTypes.TryGetValue(eventName, out Type type))
        {
            return type;
        }

        throw new EventIsNotRegistered(eventName);
    }
    internal static void AddEventType<T>(string eventName)
    {
        _eventTypes.Add(eventName, typeof(T));
        TypeConstants<T>.EventName = eventName;
    }

    internal static string GetEventName<T>()
    {
        return TypeConstants<T>.EventName;
    }
    
}

internal static class TypeConstants<T>
{
    internal static string EventName { get; set; }
}