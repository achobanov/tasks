using VL.Challenge.Domain;
using VL.Challenge.Domain.Entities;
using VL.Challenge.Domain.Tests.Helpers;

namespace VL.Challenge.Tests.Domain;

public class AgendaTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(7)]
    public void WhenConstructed_AgendaShouldContainAllUserTasks(int tasks)
    {
        var user = UserHelper.Create();

        for (int i = 0; i < tasks; i++)
        {
            var task = TaskHelper.Create(i.ToString());
            user.Add(task);
        }
        var agenda = new Agenda(user);

        Assert.Equal(user.Tasks.Count, tasks);
        Assert.Equal(agenda.SelectMany(x => x).Count(), tasks);
    }

    [Fact]
    public void WhenAllTasksOverlap_AllTasksShouldBeInSingleGroup()
    {
        var user = UserHelper.Create();
        var tasks = new List<VLTask>
        {
            TaskHelper.Create("1", DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(60)),
            TaskHelper.Create("2", DateTimeOffset.Now.AddSeconds(10), DateTimeOffset.Now.AddSeconds(70)),
            TaskHelper.Create("3", DateTimeOffset.Now.AddSeconds(20), DateTimeOffset.Now.AddSeconds(80))
        };

        foreach (var task in tasks)
        {
            user.Add(task);
        }
        var agenda = new Agenda(user);

        Assert.Single(agenda);
        Assert.Equal(tasks, agenda.First());
    }

    [Fact]
    public void WhenTasksDontOverlap_EachGroupShouldContainSingleTask()
    {
        var user = UserHelper.Create();
        var tasks = new List<VLTask>
        {
            TaskHelper.Create("1", DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(10)),
            TaskHelper.Create("2", DateTimeOffset.Now.AddSeconds(20), DateTimeOffset.Now.AddSeconds(30)),
            TaskHelper.Create("3", DateTimeOffset.Now.AddSeconds(40), DateTimeOffset.Now.AddSeconds(50))
        };

        foreach (var task in tasks)
        {
            user.Add(task);
        }
        var agenda = new Agenda(user);

        Assert.Equal(tasks.Count, agenda.Count);
        for (int i = 0; i < agenda.Count; i++)
        {
            Assert.Single(agenda[i]);
        }
    }
}
