using Microsoft.AspNetCore.Mvc;

namespace Bellerphon.EventBus.EfCore.Tests.Controllers;

[ApiController]
[Route("api/Test")]
public class TestController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync()
    {
        return Ok();
    }
}