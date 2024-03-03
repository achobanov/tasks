using VL.Challenge.Domain.Entities;

namespace VL.Challenge.Domain;

public class Agenda : List<List<VLTask>>
{
    public Agenda()
    {
    }
    public Agenda(User user) : base()
    {
        var group = new List<VLTask>();
        foreach (var task in user.Tasks.OrderBy(x => x.StartTime))
        {
            if (group.Count == 0)
            {
                group.Add(task);
                continue;
            }
            if (AreOverlap(group.Last(), task))
            {
                group.Add(task);
                continue;
            }
            AddConcurentTasks(group);
            group = CreateConcurrentGroup(task);
        }
        AddConcurentTasks(group);
    }

    private static bool AreOverlap(VLTask earlier, VLTask later)
    {
        return later.StartTime < earlier.EndTime;
    }

    public void AddConcurentTasks(IEnumerable<VLTask> tasks)
    {
        this.Add(tasks.ToList());
    }

    private List<VLTask> CreateConcurrentGroup(VLTask task)
    {
        return new() { task };
    }
}
