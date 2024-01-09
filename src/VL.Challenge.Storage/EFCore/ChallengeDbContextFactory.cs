using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VL.Challenge.Storage.EFCore.Configuration;

public class ChallengeDbContextFactory : IDesignTimeDbContextFactory<ChallengeDbContext>
{
    public ChallengeDbContext CreateDbContext(string[] args)
    {
        if (args.Length < 1)
        {
            throw new Exception("EF Core migrations require hostname for the SQL server connection");
        }

        var options = new DbContextOptionsBuilder<ChallengeDbContext>();
        options.UseSqlServer($"Data Source={args[0]}; Database=VLChallenge; User Id=sa; Password=password12_; Encrypt=true; TrustServerCertificate=true ");
        return new ChallengeDbContext(options.Options);
    }
}
