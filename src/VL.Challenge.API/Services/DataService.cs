using VL.Challenge.Domain.Entities;

namespace VL.Challenge.API.Services;

public class DataService : IDataService
{
    private readonly List<User>_users = new();

    public void AddUser(User user)
    {
        _users.Add(user);
    }
    public void RemoveUser(User user)
    {
        _users.Remove(user);
    }
    public void UpdateUser(User user)
    {
        var match = _users.FirstOrDefault(x => x.Id == user.Id);
        if (match != null)
        {
            _users.Remove(user);
        }
        _users.Add(user);
    }
    public User? GetUser(int id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }
    public List<User> GetAllUsers()
    {
        return _users.ToList();
    }

    public void AddTask(int userId, VLTask task)
    {
        var user = _users.FirstOrDefault(x => x.Id == userId)
            ?? throw new Exception($"User with ID '{userId}' does not exist");
        user.Add(task);
    }
    public void RemoveTask(VLTask task)
    {
        var user = _users.FirstOrDefault(x => x.Tasks.Any(y => y.Id == task.Id))
            ?? throw new Exception($"Task with id '{task.Id}' does not exist");
        user.Remove(task);
    }
    public void UpdateTask(VLTask task)
    {
        var user = _users.FirstOrDefault(x => x.Tasks.Any(y => y.Id == task.Id))
            ?? throw new Exception($"Task with id '{task.Id}' does not exist");
        var match = user.Tasks.First(x => x.Id == task.Id);
        user.Remove(match);
        user.Add(match);
    }
    public VLTask? GetTask(int id)
    {
        return _users.SelectMany(x => x.Tasks).FirstOrDefault(x => x.Id == id);
    }
    public List<VLTask> GetAllTasks()
    {
        return _users.SelectMany(x => x.Tasks).ToList();
    }

}

public interface IDataService
{
    void AddUser(User user);
    void RemoveUser(User user);
    void UpdateUser(User user);
    User? GetUser(int id);
    List<User> GetAllUsers();
    void AddTask(int userId, VLTask task);
    void RemoveTask(VLTask task);
    void UpdateTask(VLTask task);
    VLTask? GetTask(int id);
    List<VLTask> GetAllTasks();
}
