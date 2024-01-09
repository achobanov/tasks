using System.Text.Json.Serialization;

namespace VL.Challenge.Domain.Entities;

public class VLTask : DomainEntity, IAggregateRoot
{
    public VLTask(int id, string subject, string? description, DateTimeOffset startTime, DateTimeOffset endTime) : base(id)
    {
        if (startTime > endTime)
        {
            throw new DomainException($"{nameof(VLTask)} cannot end before it starts. Start '{startTime}', end '{endTime}'");
        }

        Subject = subject;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
    }

    [JsonInclude]
    public string Subject { get; private set; } = default!;
    [JsonInclude]
    public string? Description { get; private set; }
    [JsonInclude]
    public DateTimeOffset StartTime { get; private set; } = default!;
    [JsonInclude]
    public DateTimeOffset EndTime { get; private set; } = default!;
    [JsonInclude]
    public DateTimeOffset Date => StartTime.Date;
}
