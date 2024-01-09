using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VL.Challenge.Storage.EFCore.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Tasks)
            .WithOne();

        builder.HasData(new { Id = 1, Username = "admin" });
    }
}
