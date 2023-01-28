using Light.GuardClauses;
using Workout.Infra.Persistence;

namespace Workout.API.SeedWork
{
    public abstract class RequestHandlerBase
    {
        protected readonly WorkoutContext _workoutContext;

        protected RequestHandlerBase(WorkoutContext workoutContext)
        {
            _workoutContext = workoutContext.MustNotBeNull();
        }
    }
}
