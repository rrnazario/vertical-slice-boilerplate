using Microsoft.EntityFrameworkCore;
using Workout.Domain.Model;
using Workout.Domain.SeedWork;

namespace Workout.Infra.Persistence
{
    public class WorkoutContext
        : DbContext, IUnitOfWork
    {
        public DbSet<Exercise> Exercises { get; private set; }
        public DbSet<Serie> Series { get; private set; }

        public WorkoutContext(DbContextOptions<WorkoutContext> options)
            : base(options) { }
    }
}
