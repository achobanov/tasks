using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VL.Challenge.Storage.EFCore.Configuration;

internal class VLTaskConfiguration : IEntityTypeConfiguration<VLTask>
{
    public void Configure(EntityTypeBuilder<VLTask> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasData(
            new
            {
                Id = 1,
                UserId = 1,
                Subject = "Task 1",
                Description = "Task 1 Description",
                StartTime = DateTimeOffset.Now.AddMinutes(10),
                EndTime = DateTimeOffset.Now.AddMinutes(30)
            },
            new
            {
                Id = 2,
                UserId = 1,
                Subject = "Task 2",
                Description = "Task 2 Description",
                StartTime = DateTimeOffset.Now.AddDays(1).AddMinutes(10),
                EndTime = DateTimeOffset.Now.AddDays(1).AddMinutes(30)
            },
            new
            {
                Id = 3,
                UserId = 1,
                Subject = "Task 3",
                StartTime = DateTimeOffset.Now.AddMinutes(-30),
                EndTime = DateTimeOffset.Now.AddMinutes(-20)
            });
    }
}
