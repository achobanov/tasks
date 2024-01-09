using VL.Challenge.Domain.Entities;

namespace VL.Challenge.Domain.Tests.Helpers;

internal static class TaskHelper
{
    public static VLTask Create(string? subject = null, DateTimeOffset? startTime = null, DateTimeOffset? endTime = null)
    {
        startTime ??= DateTimeOffset.Now;
        endTime ??= DateTimeOffset.Now.AddMinutes(1);
        return new VLTask(default, subject ?? "default", "", startTime.Value, endTime.Value);
    }
}
