using VL.Challenge.Common.Tasks;
using VL.Challenge.Domain.Entities;

namespace VL.Challenge.Blazor.Client.Services;

public class TaskApi : ITaskApi
{
    private readonly IHttpService _httpService;

    public TaskApi(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<bool> Create(TaskCreateModel model)
    {
        return await _httpService.Post("tasks", model);
    }

    public async Task<bool> Delete(int id)
    {
        return await _httpService.Delete($"tasks/{id}");
    }

    public async Task<VLTask?> Read(int id)
    {
        return await _httpService.Get<VLTask>($"tasks/{id}");
    }

    public async Task<bool> Update(TaskUpdateModel model)
    {
        return await _httpService.Put("tasks", model);
    }
}

public interface ITaskApi
{
    Task<VLTask?> Read(int id);
    Task<bool> Create(TaskCreateModel model);
    Task<bool> Update(TaskUpdateModel model);
    Task<bool> Delete(int id);
}
