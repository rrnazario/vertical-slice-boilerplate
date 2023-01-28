using Light.GuardClauses;
using MediatR;
using Workout.API.SeedWork;
using Workout.Infra.Persistence;

namespace Workout.API.Features.Exercise
{
    public class GetAllExercises
    {
        public record GetAllExercisesQuery
            : IRequest<IEnumerable<GetAllExercisesResponse>>;

        public record GetAllExercisesResponse(
            string Name,
            string Description
        );

        public class QueryHandler
            : RequestHandlerBase, IRequestHandler<GetAllExercisesQuery, IEnumerable<GetAllExercisesResponse>>
        {
            public QueryHandler(UnitOfWork workoutContext) : base(workoutContext) { }

            public Task<IEnumerable<GetAllExercisesResponse>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
            {
                var result = _workoutContext.Exercises.AsQueryable()
                    .Select(s => new GetAllExercisesResponse(s.Name, s.Description));

                return Task.FromResult(result.AsEnumerable());
            }
        }
    }
}
