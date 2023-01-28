using Light.GuardClauses;
using Workout.Domain.SeedWork;
using Workout.Infra.Persistence;

namespace Workout.API.SeedWork
{
    public abstract class RequestHandlerBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected RequestHandlerBase(IUnitOfWork workoutContext)
        {
            _unitOfWork = workoutContext.MustNotBeNull();
        }
    }
}
