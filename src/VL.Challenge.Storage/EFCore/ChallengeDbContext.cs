using Microsoft.EntityFrameworkCore;

namespace VL.Challenge.Storage.EFCore;

public class ChallengeDbContext : DbContext
{
    private ChallengeDbContext()
    {
    }
    public ChallengeDbContext(DbContextOptions<ChallengeDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<VLTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
