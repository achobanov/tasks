using VL.Challenge.Common.Models.Users;
using VL.Challenge.Common.Tasks;
using VL.Challenge.Common.Users;
using VL.Challenge.Domain;
using VL.Challenge.Domain.Entities;

namespace VL.Challenge.Blazor.Client.Services;

public class UserApi : IUserApi
{
    private static Random _random = new Random();
    private readonly UserContext _userContext;
    private readonly IDataService _dataService;

    public UserApi(UserContext userContext, IDataService dataService)
    {
        _userContext = userContext;
        _dataService = dataService;
    }

    public Task<AgendaModel?> GetAgenda(int id)
    {
        var user = _dataService.GetUser(id);
        if (user == null)
        {
            return Task.FromResult((AgendaModel?)null);
        }
        var agenda = new Agenda(user);
        var model = new AgendaModel();
        model.AddRange(agenda);
        return Task.FromResult((AgendaModel?)model);
    }

    public Task<IEnumerable<UserListModel>> GetList()
    {
        var users = _dataService.GetAllUsers().Select(x => new UserListModel { Username = x.Username, Tasks = x.Tasks.Count });
        return Task.FromResult(users);
    }

    public Task Login(string username)
    {
        var user = _dataService.GetUser(username);
        if (user == null)
        {
            return Task.CompletedTask;
        }
        _userContext.LoggingId = user.Id;
        return Task.CompletedTask;
    }

    public void Logout()
    {
        _userContext.LoggingId = null;
    }

    public Task<bool> Register(UserCreateModel model)
    {
        var user = new User(_random.Next(), model.Username);
        _dataService.CreateUser(user);
        return Task.FromResult(true);
    }
}

public interface IUserApi
{
    Task<AgendaModel?> GetAgenda(int id);
    Task<IEnumerable<UserListModel>> GetList();
    Task<bool> Register(UserCreateModel model);
    Task Login(string username);
    void Logout();
}
