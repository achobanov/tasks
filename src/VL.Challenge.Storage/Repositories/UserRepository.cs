using Microsoft.EntityFrameworkCore;
using VL.Challenge.Common.CRUD;
using VL.Challenge.Common.Models.Users;
using VL.Challenge.Storage.EFCore;

namespace VL.Challenge.Storage.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ChallengeDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<UserListModel>> GetList()
    {
        return await Set
            .Select(x => new UserListModel { Username = x.Username, Tasks = x.Tasks.Count() })
            .ToListAsync();
    }

    public async Task<int?> GetLoginId(string username)
    {
        return await Set
            .Where(x => x.Username == username)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<VLTask>> GetTasks(int id)
    {
        return await Set
            .Where(x => x.Id == id)
            .SelectMany(x => x.Tasks)
            .ToListAsync();
    }

    public async Task<User?> GetByTaskWithTasks(int taskId)
    {
        return await Set
            .Include(x => x.Tasks)
            .FirstOrDefaultAsync(x => x.Tasks.Any(y => y.Id == taskId));
    }

    public async Task<User?> GetWithTasks(int id)
    {
        return await Set
            .Include(x => x.Tasks)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await Set.FirstOrDefaultAsync(x => x.Username == username);
    }
}

public interface IUserRepository : ICreate<User>, IUpdate<User>
{
    Task<IEnumerable<UserListModel>> GetList();
    Task<User?> GetByUsername(string username);
    Task<int?> GetLoginId(string username);
    Task<IEnumerable<VLTask>> GetTasks(int id);
    Task<User?> GetByTaskWithTasks(int taskId);
    Task<User?> GetWithTasks(int id);
}
