using Microsoft.EntityFrameworkCore;
using Workout.Domain.Model;

namespace Workout.Infra.Persistence
{
    public class UnitOfWork
        : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Serie> Series { get; set; }

        public UnitOfWork(DbContextOptions<UnitOfWork> options) : base(options)
        {

        }

    }
}
