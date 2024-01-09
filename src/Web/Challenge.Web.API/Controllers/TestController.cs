using Microsoft.AspNetCore.Mvc;

namespace Challenge.Web.API.Controllers;

[ApiController]
public class TestController : ControllerBase
{

    [HttpGet("test")]
    public Task<IActionResult> Test()
    {
        return Task.FromResult(Ok("1,2") as IActionResult);
    }
}
