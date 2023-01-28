using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Workout.Domain.SeedWork;
using Workout.Infra.Persistence;

namespace Workout.API.DI
{
    public static class DatabaseInjection
    {
        public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services)
        {
            services.AddDbContext<WorkoutContext>(options =>
            {
                options.UseInMemoryDatabase("workoutdb")
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            services.AddScoped<IUnitOfWork, WorkoutContext>();

            return services;
        }

        public static void SeedInMemoryDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            unitOfWork.Exercises.Add(new(1, "Squats", "Squats for pump the whole body"));
            unitOfWork.Exercises.Add(new(3, "Burpees", "Burpees to burn all undesired fat"));
            unitOfWork.Exercises.Add(new(2, "Dumbbell rows", "Dumbbell rows to stronger backs"));            

            unitOfWork.Series.Add(new(1, 1, 10, 0f));
            unitOfWork.Series.Add(new(2, 3, 10, 20.5f));

            unitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
