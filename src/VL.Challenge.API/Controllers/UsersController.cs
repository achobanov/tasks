using Microsoft.AspNetCore.Mvc;
using VL.Challenge.API.Requests;
using VL.Challenge.API.Services;
using VL.Challenge.Common.Users;

namespace VL.Challenge.API.Controllers;

[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserCreateModel model)
    {
        await _usersService.Create(model);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _usersService.GetList();
        return Ok(result);
    }

    [HttpGet("{id}/tasks")]
    public async Task<IActionResult> Get(IdRequest request)
    {
        var result = await _usersService.GetTasks(request.Id);
        return Ok(result);
    }

    [HttpGet("{id}/agenda")]
    public async Task<IActionResult> GetAgenda(IdRequest request)
    {
        var result = await _usersService.GetAgenda(request.Id);
        return Ok(result.ToArray());
    }

    [HttpPost("{username}/login")]
    public async Task<IActionResult> Post(string username)
    {
        var id = await _usersService.GetLoginId(username);
        var model = new UserLoginResponseModel { Id = id };
        return Ok(model);
    }
}
