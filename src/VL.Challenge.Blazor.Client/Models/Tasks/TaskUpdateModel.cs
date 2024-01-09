using System.ComponentModel.DataAnnotations;
using VL.Challenge.Domain.Entities;

namespace VL.Challenge.Common.Tasks;

public class TaskUpdateModel
{
    public TaskUpdateModel()
    {
    }
    public TaskUpdateModel(VLTask task)
    {
        Id = task.Id;
        Subject = task.Subject;
        Description = task.Description;
        StartTime = task.StartTime;
        EndTime = task.EndTime;
    }

    public int Id { get; set; }
    [Required]
    public string Subject { get; set; } = default!;
    public string? Description { get; set; }
    [Required]
    public DateTimeOffset StartTime { get; set; } = default!;
    [Required]
    public DateTimeOffset EndTime { get; set; } = default!;
}
