using System.Threading.Tasks;
using VL.Challenge.Domain;
using VL.Challenge.Domain.Entities;
using VL.Challenge.Domain.Tests.Helpers;

namespace VL.Challenge.Tests.Domain;

public class UserTests
{
    [Fact]
    public void WhenUsernameLengthLessThan4_ShouldThrowDomainException()
    {
        var one = () => new User(default, "a");
        var two = () => new User(default, "aaa");
        var three = () => new User(default, "aaa");

        Assert.Throws<DomainException>(() => one());
        Assert.Throws<DomainException>(() => two());
        Assert.Throws<DomainException>(() => three());
    }

    [Fact]
    public void WheAddTaskWithDuplicateSubject_ShouldThrowDomainException()
    {
        var user = UserHelper.Create();
        var task1 = TaskHelper.Create();
        var task2 = TaskHelper.Create();

        user.Add(task1);

        Assert.Throws<DomainException>(() => user.Add(task2));
    }

    [Fact]
    public void WhenAttemptToRemoveLastTask_ShouldThrowDomainException()
    {
        var user = UserHelper.Create();
        var task1 = TaskHelper.Create();

        user.Add(task1);

        Assert.Throws<DomainException>(() => user.Remove(task1));
    }
}
