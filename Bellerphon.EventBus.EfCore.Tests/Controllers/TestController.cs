using Bellerphon.EventBus.EfCore.Abstractions;
using Bellerphon.EventBus.EfCore.Tests.Contexts;
using Bellerphon.EventBus.EfCore.Tests.Entities;
using Bellerphon.EventBus.EfCore.Tests.Events;
using Microsoft.AspNetCore.Mvc;

namespace Bellerphon.EventBus.EfCore.Tests.Controllers;

[ApiController]
[Route("api/Test")]
public class TestController : ControllerBase
{
    private readonly IPublisher<TestDbContext> _publisher;
    private readonly TestDbContext _context;

    public TestController(IPublisher<TestDbContext> publisher, TestDbContext context)
    {
        _publisher = publisher;
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync(CancellationToken token)
    {
        _context.Users.Add(new User()
        {
            Name = "Sandro",
            Email = "sandrika@gmail.com"
        });
        await _publisher.Publish(
            body: new UserCreatedEvent(){UserName = "Test"},
            headers: null, 
            token);
        await _publisher.Publish(
            body: new UserCreatedEvent(){UserName = "Test2"},
            headers: null, 
            token);
        await _context.SaveChangesAsync(token);
        return Ok();
    }
}