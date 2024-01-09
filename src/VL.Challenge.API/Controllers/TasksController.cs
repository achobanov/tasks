using Microsoft.AspNetCore.Mvc;
using VL.Challenge.API.Requests;
using VL.Challenge.API.Services;
using VL.Challenge.Common.Tasks;

namespace VL.Challenge.API.Controllers;

[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly ITasksService _tasksService;

    public TasksController(ITasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TaskCreateModel model)
    {
        await _tasksService.Create(model);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(IdRequest request)
    {
        var result = await _tasksService.Get(request.Id);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] TaskUpdateModel model)
    {
        await _tasksService.Update(model);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(IdRequest request)
    {
        await _tasksService.Delete(request.Id);
        return Ok();
    }
}
