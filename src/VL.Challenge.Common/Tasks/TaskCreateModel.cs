using System.ComponentModel.DataAnnotations;
using VL.Challenge.Domain.Entities;

namespace VL.Challenge.Common.Tasks;

public class TaskCreateModel
{
    public int UserId { get; set; }
    [Required]
    public string Subject { get; set; } = default!;
    public string? Description { get; set; }
    [Required]
    public DateTimeOffset StartTime { get; set; } = DateTimeOffset.Now;
    [Required]
    public DateTimeOffset EndTime { get; set; } = DateTimeOffset.Now;
}
