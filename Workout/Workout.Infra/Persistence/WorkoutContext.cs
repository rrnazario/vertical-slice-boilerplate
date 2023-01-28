using Microsoft.EntityFrameworkCore;
using Workout.Domain.Model;

namespace Workout.Infra.Persistence
{
    public class WorkoutContext
        : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Serie> Series { get; set; }

        public WorkoutContext(DbContextOptions<WorkoutContext> options) : base(options)
        {

        }

    }
}
