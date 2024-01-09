using VL.Challenge.Common.Models.Users;
using VL.Challenge.Common.Users;
using VL.Challenge.Domain;
using VL.Challenge.Domain.Entities;
using VL.Challenge.Storage.Repositories;

namespace VL.Challenge.API.Services;

public class UsersService : IUsersService
{
    private readonly IUserRepository _users;

    public UsersService(IUserRepository users)
    {
        _users = users;
    }

    public async Task Create(UserCreateModel model)
    {
        var match = await _users.GetByUsername(model.Username);
        if (match != null)
        {
            throw new ApplicationException($"User with Username '{model.Username}' already exists");
        }

        var user = new User(default, model.Username);
        await _users.Create(user);
    }

    public async Task<IEnumerable<UserListModel>> GetList()
    {
        return await _users.GetList();
    }

    public async Task<int> GetLoginId(string username)
    {
        var loginId =  await _users.GetLoginId(username);
        if (loginId == null)
        {
            throw new ApplicationException($"User '{username}' does not exist");
        }
        return loginId.Value;
    }

    public async Task<Agenda> GetAgenda(int id)
    {
        var user = await _users.GetWithTasks(id);
        if (user == null)
        {
            throw new ApplicationException($"User with id '{id}' does not exist");
        }
        var agenda = new Agenda(user);
        return agenda;
    }

    public async Task<IEnumerable<VLTask>> GetTasks(int id)
    {
        return await _users.GetTasks(id);
    }
}

public interface IUsersService
{
    Task Create(UserCreateModel model);
    Task<IEnumerable<UserListModel>> GetList();
    Task<int> GetLoginId(string username);
    Task<Agenda> GetAgenda(int id);
    Task<IEnumerable<VLTask>> GetTasks(int id);
}
