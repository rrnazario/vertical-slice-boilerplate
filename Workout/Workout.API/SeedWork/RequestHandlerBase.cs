using Light.GuardClauses;
using Workout.Infra.Persistence;

namespace Workout.API.SeedWork
{
    public abstract class RequestHandlerBase
    {
        protected readonly UnitOfWork _workoutContext;

        protected RequestHandlerBase(UnitOfWork workoutContext)
        {
            _workoutContext = workoutContext.MustNotBeNull();
        }
    }
}
