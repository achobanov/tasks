using VL.Challenge.Common.Models.Users;
using VL.Challenge.Common.Tasks;
using VL.Challenge.Common.Users;
using VL.Challenge.Domain;

namespace VL.Challenge.Blazor.Client.Services;

public class UserApi : IUserApi
{
    private readonly IHttpService _httpService;
    private readonly UserContext _userContext;

    public UserApi(IHttpService httpService, UserContext userContext)
    {
        _httpService = httpService;
        _userContext = userContext;
    }

    public async Task<AgendaModel?> GetAgenda(int id)
    {
        var agenda = await _httpService.Get<Agenda>($"users/{id}/agenda");
        if (agenda == null)
        {
            return null;
        }
        var model = new AgendaModel();
        model.AddRange(agenda);
        return model;
    }

    public async Task<IEnumerable<UserListModel>> GetList()
    {
        return await _httpService.Get<List<UserListModel>>("users") ?? new();
    }

    public async Task Login(string username)
    {
        var payload = await _httpService.Post<UserLoginResponseModel>($"users/{username}/login");
        _userContext.LoggingId = payload?.Id;
    }

    public void Logout()
    {
        _userContext.LoggingId = null;
    }

    public async Task<bool> Register(UserCreateModel model)
    {
        return await _httpService.Post($"users", model);
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
