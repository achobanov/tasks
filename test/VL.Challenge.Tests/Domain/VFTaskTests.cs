using System;
using VL.Challenge.Domain;
using VL.Challenge.Domain.Entities;

namespace VL.Challenge.Tests.Domain;

public class VFTaskTests
{
    [Fact]
    public void WhenStartTimeMoreThanEndTime_ShouldThrowDomainException()
    {
        var endTime = DateTimeOffset.Now;
        var startTime = endTime.AddMinutes(1);

        var action = () => new VLTask(default, "", "", startTime, endTime);

        Assert.Throws<DomainException>(() => action());
    }
}