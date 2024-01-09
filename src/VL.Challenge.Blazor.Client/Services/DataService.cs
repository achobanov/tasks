using VL.Challenge.Domain.Entities;

namespace VL.Challenge.Blazor.Client.Services;

public class DataService : IDataService
{
    private readonly List<User> _users = new() { new User(1, "admin") };
    private readonly IToaster _toaster;

    public DataService(IToaster toaster)
    {
        _toaster = toaster;
    }

    public void CreateUser(User user)
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
        try
        {
            return _users.FirstOrDefault(x => x.Id == id)
                ?? throw new Exception($"User with ID '{id}' does not exist");
        }
        catch (Exception ex)
        {
            HandleError(ex);
            return default;
        }
        
    }
    public User? GetUser(string username)
    {
        try
        {
            return _users.FirstOrDefault(x => x.Username == username)
                ?? throw new Exception($"User '{username}' does not exist");
        }
        catch (Exception ex)
        {
            HandleError(ex);
            return default;
        }
    }
    public List<User> GetAllUsers()
    {
        return _users.ToList();
    }

    public bool CreateTask(int userId, VLTask task)
    {
        try
        {
            var user = _users.FirstOrDefault(x => x.Id == userId)
            ?? throw new Exception($"User with ID '{userId}' does not exist");
            user.Add(task);
            return true;
        }
        catch (Exception ex)
        {
            HandleError(ex);
            return false;
        }
    }
    public bool RemoveTask(int id)
    {
        try
        {
            var user = _users.FirstOrDefault(x => x.Tasks.Any(y => y.Id == id))
                ?? throw new Exception($"Task with id '{id}' does not exist");
            var task = user.Tasks.First(x => x.Id == id);
            user.Remove(task);
            return true;
        }
        catch (Exception ex)
        {
            HandleError(ex);
            return false;
        }
    }
    public bool UpdateTask(VLTask task)
    {
        try
        {
            var user = _users.FirstOrDefault(x => x.Tasks.Any(y => y.Id == task.Id))
            ?? throw new Exception($"Task with id '{task.Id}' does not exist");
            var match = user.Tasks.First(x => x.Id == task.Id);
            user.Remove(match);
            user.Add(match);
            return true;
        }
        catch (Exception ex)
        {
            HandleError(ex);
            return false;
        }
    }
    public VLTask? GetTask(int id)
    {
        return _users.SelectMany(x => x.Tasks).FirstOrDefault(x => x.Id == id);
    }
    public List<VLTask> GetAllTasks()
    {
        return _users.SelectMany(x => x.Tasks).ToList();
    }

    private void HandleError(Exception ex)
    {
        _toaster.Add("validation", ex.Message, UiColor.Warning);
    }
}

public interface IDataService
{
    void CreateUser(User user);
    void RemoveUser(User user);
    void UpdateUser(User user);
    User? GetUser(int id);
    User? GetUser(string username);
    List<User> GetAllUsers();
    bool CreateTask(int userId, VLTask task);
    bool RemoveTask(int id);
    bool UpdateTask(VLTask task);
    VLTask? GetTask(int id);
    List<VLTask> GetAllTasks();
}
