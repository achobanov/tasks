using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VL.Challenge.Common.CRUD;
using VL.Challenge.Storage.EFCore;

namespace VL.Challenge.Storage.Repositories;

public class GenericRepository<T> : IRepository<T>
    where T : DomainEntity, IAggregateRoot
{
    protected DbContext DbContext { get; }
    protected DbSet<T> Set { get; }

    public GenericRepository(ChallengeDbContext dbContext)
    {
        DbContext = dbContext;
        Set = dbContext.Set<T>();
    }

    public async Task<T> Create(T entity)
    {
        Set.Add(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(T entity)
    {
        Set.Remove(entity);
        await DbContext.SaveChangesAsync(true);
    }

    public async Task Delete(int id)
    {
        var match = Set.FirstOrDefault(x => x.Id == id);
        if (match != null)
        {
            Set.Remove(match);
            await DbContext.SaveChangesAsync();
        }
    }
        
    public async Task<T> Update(T entity)
    {
        Set.Update(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> Read(int id)
    {
        return await Set.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<T>> Read()
    {
        return await Set.ToListAsync();
    }

    public async Task<IEnumerable<T>> Read(Expression<Func<T, bool>> predicate)
    {
        return await Set
            .Where(predicate)
            .ToListAsync();
    }
}
