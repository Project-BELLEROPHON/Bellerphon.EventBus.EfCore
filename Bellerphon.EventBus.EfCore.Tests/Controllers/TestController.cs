using Bellerphon.EventBus.EfCore.Abstractions;
using Bellerphon.EventBus.EfCore.Tests.Contexts;
using Bellerphon.EventBus.EfCore.Tests.Events;
using Microsoft.AspNetCore.Mvc;

namespace Bellerphon.EventBus.EfCore.Tests.Controllers;

[ApiController]
[Route("api/Test")]
public class TestController : ControllerBase
{
    private readonly IPublisher<TestDbContext> _publisher;

    public TestController(IPublisher<TestDbContext> publisher)
    {
        _publisher = publisher;
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync(CancellationToken token)
    {
        await _publisher.Publish(
            body: new UserCreatedEvent(){UserName = "Test"},
            headers: null, 
            token);
        await _publisher.SaveChangesAsync(token);
        return Ok();
    }
}