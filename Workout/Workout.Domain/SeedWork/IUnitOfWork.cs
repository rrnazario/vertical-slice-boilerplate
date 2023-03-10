using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Workout.Domain.Model;

namespace Workout.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        public DbSet<Exercise> Exercises { get; }
        public DbSet<Serie> Series { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
    }
}
