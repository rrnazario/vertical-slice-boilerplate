using Light.GuardClauses;
using MediatR;
using Workout.API.SeedWork;
using Workout.Domain.SeedWork;
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
            public QueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

            public Task<IEnumerable<GetAllExercisesResponse>> Handle(GetAllExercisesQuery request, CancellationToken cancellationToken)
            {
                var result = _unitOfWork.Exercises.AsQueryable()
                    .Select(s => new GetAllExercisesResponse(s.Name, s.Description));

                return Task.FromResult(result.AsEnumerable());
            }
        }
    }
}
