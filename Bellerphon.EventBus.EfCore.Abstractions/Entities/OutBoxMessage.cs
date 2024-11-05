namespace Bellerphon.EventBus.EfCore.Abstractions.Entities;

public class OutBoxMessage
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public string EventName { get; set; }
    public string Body { get; set; }
    public string? Headers { get; set; }
    public bool IsSent { get; set; }
    public int TryCount { get; set; }
    public DateTime? ExpireDate { get; set; }
    public DateTime InsertDate { get; set; }
}