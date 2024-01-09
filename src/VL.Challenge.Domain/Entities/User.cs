using System.Text.Json.Serialization;

namespace VL.Challenge.Domain.Entities;

public class User : DomainEntity, IAggregateRoot
{
    private List<VLTask> _tasks = new();

    public User(int id, string username) : base(id)
    {
        if (username.Length < 4)
        {
            throw new DomainException("HAHAH! Username needs to be at least 4 symbols long!");
        }
        Username = username;
    }

    [JsonInclude]
    public string Username { get; private set; } = default!;
    [JsonInclude]
    public IReadOnlyCollection<VLTask> Tasks
    {
        get => _tasks.AsReadOnly();
        private set => _tasks = value.ToList();
    }

    public void Add(VLTask task)
    {
        if (_tasks.Any(x => x.Subject == task.Subject))
        {
            throw new DomainException($"You already added '{task.Subject}', remember?");
        }
        _tasks.Add(task);
    }
    public void Remove(VLTask task)
    {
        if (_tasks.Count == 1)
        {
            throw new DomainException("No no, don't remove that last Task. Don't want to be a slacker now, do you?");
        }
        _tasks.Remove(task);
    }
}
