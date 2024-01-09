using VL.Challenge.Common;
using VL.Challenge.Common.CRUD;
using VL.Challenge.Common.Tasks;
using VL.Challenge.Domain.Entities;
using VL.Challenge.Storage.Repositories;

namespace VL.Challenge.API.Services;

public class TasksService : ITasksService
{
    private readonly IUpdate<VLTask> _taskUpdater;
    private readonly IDelete<VLTask> _taskDeleter;
    private readonly IUserRepository _users;
    private readonly ICache<VLTask> _tasksCache;

    // Display of Interface Segregation
    public TasksService(
        ICache<VLTask> tasksCache,
        IUpdate<VLTask> taskUpdater,
        IDelete<VLTask> taskDeleter,
        IUserRepository users)
    {
        _taskUpdater = taskUpdater;
        _taskDeleter = taskDeleter;
        _users = users;
        _tasksCache = tasksCache;
    }

    public async Task Create(TaskCreateModel model)
    {
        var user = await _users.GetWithTasks(model.UserId);
        if (user == null)
        {
            throw new ApplicationException($"User with id '{model.UserId}' does not exist");
        }
        var task = new VLTask(default, model.Subject, model.Description, model.StartTime, model.EndTime);
        user.Add(task);

        await _users.Update(user);
    }

    // Cache used for tasks 
    public async Task<VLTask> Get(int id)
    {
        return await _tasksCache.Get(id);
    }

    // Cache invalidated on task updates
    public async Task Update(TaskUpdateModel model)
    {
        var task = new VLTask(model.Id, model.Subject, model.Description, model.StartTime, model.EndTime);

        await _taskUpdater.Update(task);
        await _tasksCache.Invalidate(task.Id);
    }

    public async Task Delete(int id)
    {
        var user = await _users.GetByTaskWithTasks(id);
        if (user is not null)
        {
            var task = user.Tasks.First(x => x.Id == id);
            user.Remove(task);

            await _taskDeleter.Delete(id);
            await _tasksCache.Invalidate(id);
        }
    }
}

public interface ITasksService
{
    Task Create(TaskCreateModel model);
    Task<VLTask> Get(int id);
    Task Update(TaskUpdateModel model);
    Task Delete(int id);
}