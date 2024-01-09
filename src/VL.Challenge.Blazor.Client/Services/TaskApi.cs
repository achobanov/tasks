using VL.Challenge.Common.Tasks;
using VL.Challenge.Domain.Entities;

namespace VL.Challenge.Blazor.Client.Services;

public class TaskApi : ITaskApi
{
    private static Random _random = new Random();
    private readonly IDataService _dataService;

    public TaskApi(IDataService dataService)
    {
        _dataService = dataService;
    }

    public Task<bool> Create(TaskCreateModel model)
    {
        var task = new VLTask(_random.Next(), model.Subject, model.Description, model.StartTime, model.EndTime);
        var result = _dataService.CreateTask(model.UserId, task);
        return Task.FromResult(result);
    }

    public Task<bool> Delete(int id)
    {
        var result = _dataService.RemoveTask(id);
        return Task.FromResult(result);
    }

    public Task<VLTask?> Read(int id)
    {
        var match = _dataService.GetTask(id);
        return Task.FromResult(match);
    }

    public Task<bool> Update(TaskUpdateModel model)
    {
        var match = _dataService.GetTask(model.Id);
        var task = new VLTask(match?.Id ?? _random.Next(), model.Subject, model.Description, model.StartTime, model.EndTime);
        var result = _dataService.UpdateTask(task);
        return Task.FromResult(result);
    }
}

public interface ITaskApi
{
    Task<VLTask?> Read(int id);
    Task<bool> Create(TaskCreateModel model);
    Task<bool> Update(TaskUpdateModel model);
    Task<bool> Delete(int id);
}
